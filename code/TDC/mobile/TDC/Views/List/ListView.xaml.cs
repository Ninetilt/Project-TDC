using TDC.Models;
using TDC.Constants;
using TDC.Services;
using TDC.IService;
using TDC.Models.DTOs;
using TDC.Views.ListItem;
using System.Collections.ObjectModel;


#if ANDROID
using Android.Views;
#endif

namespace TDC;

[QueryProperty(nameof(IdString), "id")]
public partial class ListView : IOnPageKeyDown
{
    private readonly IListService _listService;
    private readonly IListItemService _listItemService;
    private readonly UserService _userService;
    private readonly IFriendService _friendService;

    public long? ListId { get; set; }
    public string? IdString { get; set; }
    public ToDoList List { get; set; }
    public List<ListItem> ExistingItems { get; set; }
    public List<long> DeletedItems = [];
    public List<ListItem> NewItems { get; set; } = [];
    public bool IsCreator { get; set; }
    public ObservableCollection<MemberWithPoints> Members { get; set; } = new();
    public ObservableCollection<string> FriendUsernames { get; set; } = new();

    #region constructors
    public ListView(IListService listService, IListItemService listItemService, UserService userService, IFriendService friendService)
    {
        InitializeComponent();
        _userService = userService;
        _listService = listService;
        _listItemService = listItemService;
        _friendService = friendService;
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        if (long.TryParse(IdString, out var parsed))
        {
            ListId = parsed;
        }
        else
        {
            ListId = null;
        }

        base.OnAppearing();
        _ = SetUpAsync();
    }

    #endregion

    #region listeners
    private void OnNewItemClicked(object sender, EventArgs e)
    {
        var item = new ListItem("", 5);
        NewItems.Add(item);
        AddItemToView(item);
        UpdateFinishListButton();
    }

    private void TitleEntryChanged(object sender, EventArgs e)
    {
        var newTitle = this.FindByName<Entry>("TitleEntry")?.Text;
        if (!string.IsNullOrEmpty(this.FindByName<Entry>("TitleEntry").Text))
        {
            List.Name = newTitle!;
            this.FindByName<Label>("ErrorLabel").IsVisible = false;
        }
    }

    private void OnEffortUpdated(object sender, EventArgs e)
    {
        UpdatePointLabels();
    }

    private async void OnSaveListClicked(object sender, EventArgs e)
    {
        await SaveList(true);
    }

    private async Task<long> SaveList(bool redirect)
    {
        var listName = TitleEntry.Text?.Trim();
        if (string.IsNullOrWhiteSpace(listName) || HasInvalidTitleCharacters(listName))
        {
            this.FindByName<Label>("ErrorLabel").IsVisible = true;
            return -1;
        }

        List.Name = listName;

        

        if (HasListId(ListId))
        {
            await UpdateExistingList();
            if(redirect) { await Shell.Current.GoToAsync("///MainPage");}
            return (long) ListId;
        }

        var newId = await CreateNewList();
        if(redirect) { await Shell.Current.GoToAsync("///MainPage");}
        return newId;
    }

    private async void OnFinishListClicked(object sender, EventArgs e)
    {
        await SaveList(false);
        var currentUser = _userService.CurrentUser!.Username;
        await _listService.FinishList(List.ListID, currentUser);

        await Shell.Current.GoToAsync("///MainPage");
    }

    private async void OnDeleteListClicked(object sender, EventArgs e)
    {
        if (ListId == null) { return; }
        var currentUser = _userService.CurrentUser!.Username;
        if (!IsCreator)
        {
            var leaveAnswer = await DisplayAlert("Leave list", "Would you like to leave this list?\nThis action can't be undone.", "Yes", "No");
            if (!leaveAnswer) return;

            await _listService.RemoveUserFromList(currentUser, (long) ListId);
            await Shell.Current.GoToAsync("///MainPage");
        }
        var deleteAnswer = await DisplayAlert("Delete list", "Would you like to delete this list?\nThis action can't be undone.", "Yes", "No");
        if (!deleteAnswer) return;

        await _listService.DeleteList((long)ListId!, currentUser);
        await Shell.Current.GoToAsync("///MainPage");
    }

    private void BackspaceEmitted()
    {
        foreach (var item in ItemsContainer.Children) {
            var view = (ListItemView)item;
            
            if(view.FindByName<Entry>("TaskEntry").IsFocused)
            {
                RemoveItem(view);
                return;
            }
        }
    }

    #if ANDROID
    public bool OnPageKeyDown(Keycode keyCode, KeyEvent e) {
        if(keyCode == Keycode.Del) {
            BackspaceEmitted();
            return true;
        }
        return false;
    }
#endif
    #endregion

    #region privates
    public void UpdatePointLabels()
    {
        var allItems = ExistingItems.Union(NewItems).ToList();
        this.FindByName<Label>("AllPointsLabel").Text = GetTotalPoints(allItems).ToString();
        this.FindByName<Label>("PointsLabel").Text = GetCompletedPoints(allItems).ToString();
        UpdateFinishListButton();
    }

    public int GetCompletedPoints(List<ListItem> items)
    {
        return items
            .Where(item => item.IsDone)
            .Sum(item => item.Effort * 5);
    }

    public int GetTotalPoints(List<ListItem> items)
    {
        return items.Sum(item => item.Effort * 5);
    }

    private async Task UpdateExistingList()
    {

        await _listService.UpdateListTitle(List.Name, (long) ListId!);

        var currentUser = _userService.CurrentUser!.Username;
        foreach (var existingItem in ExistingItems)
        {
            await _listItemService.UpdateItemDescription(existingItem.ItemId, existingItem.Description);
            await _listItemService.UpdateItemEffort(existingItem.ItemId, existingItem.Effort);
            await _listItemService.SetItemStatus(existingItem.ItemId, currentUser, existingItem.IsDone);
        }

        foreach (var item in NewItems) {
            var itemDto = new ListItemSavingDto(item.Description, item.Effort);
            var itemId = await _listItemService.AddItemToList((long)ListId!, itemDto);
            await _listItemService.SetItemStatus(itemId, currentUser, item.IsDone);
        }

        foreach (var id in DeletedItems)
        {
            await _listItemService.DeleteItem(id);
        }
    }

    private async Task<long> CreateNewList()
    {
        var currentUser = _userService.CurrentUser!.Username;
        var newId = await _listService.CreateList(List.Name, List.IsCollaborative, currentUser); //TODO: Add toggle option for collabs

        ListId = newId;

        foreach (var item in NewItems)
        {
            var itemDto = new ListItemSavingDto(item.Description, item.Effort);
            var itemId = await _listItemService.AddItemToList(newId, itemDto);
            await _listItemService.SetItemStatus(itemId, currentUser, item.IsDone);
        }

        return newId;
    }

    private async Task SetUpAsync()
    {
        if (HasListId(ListId))
        {
            var currentUser = _userService.CurrentUser!.Username;
            List = await _listService.GetListById((long)ListId!)!;
            ExistingItems = await _listItemService.GetItemsForList((long)ListId!, currentUser);
            this.FindByName<Entry>("TitleEntry").Text = List.Name;
            AddItemsForExistingList();

            UpdatePointLabels();

            await LoadMembers();
            await LoadFriends();

            IsCreator = await _listService.IsUserCreator(currentUser, (long) ListId);

            if (!IsCreator)
            {
                this.FindByName<Button>("DeleteListBtn").Text = "Leave List";
                this.FindByName<Button>("RemoveMemberBtn").IsVisible = false;
                this.FindByName<CollectionView>("MemberListView").SelectionMode = SelectionMode.None;
            }

            return;
        }
        List = new ToDoList();
        ExistingItems = [];
        await LoadFriends();
    }

    private void AddItemsForExistingList()
    {
        foreach (var item in ExistingItems) {
            AddItemToView(item);
        }
    }

    private async Task LoadMembers()
    {
        var result = await _listService.GetMembersForList(ListId.Value);
        var currentUser = _userService.CurrentUser!.Username;

        Members.Clear();
        foreach (var username in result.Members)
        {
            if (username == currentUser)
                continue;

            var points = await _listService.GetPointsForMember(username, ListId.Value);
            Members.Add(new MemberWithPoints
            {
                Username = username,
                Points = points
            });
        }
    }

    private async Task LoadFriends()
    {
        var allFriends = await _friendService.GetFriendsForUser(_userService.CurrentUser!.Username);

        if (!HasListId(ListId))
        {
            FriendUsernames.Clear();
            foreach (var friend in allFriends)
            {
                FriendUsernames.Add(friend);
            }
            return;
        }

        var currentMembers = (await _listService.GetMembersForList(ListId.Value)).Members;

        if (currentMembers == null)
        {
            FriendUsernames.Clear();
            foreach (var friend in allFriends)
            {
                FriendUsernames.Add(friend);
            }
        }
        else
        {
            var filteredFriends = allFriends.Except(currentMembers).ToList();

            FriendUsernames.Clear();
            foreach (var name in filteredFriends)
            {
                FriendUsernames.Add(name);
            }
        }
    }
    private void AddItemToView(ListItem item) {
        var listItemView = new ListItemView(item);
        ItemsContainer.Children.Add(listItemView);
        listItemView.NewItemOnEnter += OnNewItemClicked!;
        listItemView.EffortChanged += OnEffortUpdated!;
        listItemView.DeletePressed += (s, e) => RemoveItem(listItemView); //TODO: for testing onl windows only, can be deleted later
        listItemView.CheckBoxChanged += (s, e) => UpdatePointLabels();
        listItemView.IsInitialized = true;
        OnEffortUpdated(this, EventArgs.Empty);
    }

    private void RemoveItem(ListItemView view)
    {
        DeletedItems.Add(view.GetItem().ItemId);
        ExistingItems.Remove(view.GetItem());
        NewItems.Remove(view.GetItem());
        ItemsContainer.Children.Remove(view);
        UpdateFinishListButton();
    }

    private static bool HasListId(long? listId)
    {
        return listId != null;
    }

    private int GetListPoints()
    {
        return ExistingItems.Sum(listItem => listItem.Effort * 5) + NewItems.Sum(listItem => listItem.Effort * 5);
    }

    private static bool HasInvalidTitleCharacters(string title)
    {
        return InvalidCharacters.InvalidTitle.Any(title.Contains);
    }

    private void UpdateFinishListButton()
    {
        var canBeFinished = CanBeFinished();
        this.FindByName<Button>("FinishListBtn").IsEnabled = canBeFinished;  
    }

    private bool CanBeFinished()
    {
        foreach (var item in NewItems)
        {
            if (!item.IsDone)
            {
                return false;
            }
        }

        foreach (var item in ExistingItems)
        {
            if (!item.IsDone)
            {
                return false;
            }
        }

        return true;
    }

    private async void OnAddFriendClicked(object sender, EventArgs e)
    {

        if (FriendListView.SelectedItems == null || !FriendListView.SelectedItems.Any())
        {
            await DisplayAlert("Error", "Please select friends to add", "OK");
            return;
        }

        var selectedFriends = FriendListView.SelectedItems.Cast<string>().ToList();

        var id = await SaveList(false);
        if (id == -1)
            return;

        this.List.ListID = id;
        this.ListId = id;

        foreach (var friend in selectedFriends)
        {
            await _listService.AddUserToList(friend, ListId.Value);
            FriendUsernames.Remove(friend);
        }

        await LoadMembers();
    }

    private async void OnRemoveMemberClicked(object sender, EventArgs e)
    {

        if (MemberListView.SelectedItems == null || !MemberListView.SelectedItems.Any())
        {
            await DisplayAlert("Error", "Please select members to remove", "OK");
            return;
        }

        var selectedMembers = MemberListView.SelectedItems.Cast<MemberWithPoints>().ToList();

        var id = await SaveList(redirect: false);
        if (id == -1)
        {
            return;
        }

        this.List.ListID = id;
        this.ListId = id;

        foreach (var member in selectedMembers)
        {
            if (ListId.HasValue)
            {
                await _listService.RemoveUserFromList(member.Username, ListId.Value);
                Members.Remove(member);
            }
        }

        await LoadMembers();
        await LoadFriends();
    }

    #endregion
}