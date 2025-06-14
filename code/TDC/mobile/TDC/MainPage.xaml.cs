using TDC.IService;
using TDC.Models;
using TDC.Services;

namespace TDC
{
    public partial class MainPage
    {
        private IListService _listService;
        private IListItemService _listItemService;
        private readonly UserService _userService;
        private List<ToDoList> availableLists;
        private int shownListIndex;

        #region constructors
        public MainPage()
        {
            InitializeComponent();

            _userService = App.Services.GetService<UserService>()!;
            _listService = App.Services.GetService<IListService>()!;
            _listItemService = App.Services.GetService<IListItemService>()!;


            shownListIndex = 0;
            availableLists = [];
        }
        #endregion

        #region navigation
        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            shownListIndex = 0;
            availableLists = [];
            _ = LoadAvailableLists();
        }

        private async void OnNewListClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"ToDoListPage");
        }

        private async void OnListPreviewTapped(object sender, EventArgs e)
        {
            if (availableLists.Count == 0)
            {
                return;
            }
            var id = availableLists[shownListIndex].ListID;
            await Shell.Current.GoToAsync($"ToDoListPage?id={(long?)id}");
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            shownListIndex++;
            if (shownListIndex >= availableLists.Count)
            {
                shownListIndex = 0;
            }
            _ = UpdateShownList();
        }

        private void OnPrevClicked(object sender, EventArgs e)
        {
            shownListIndex--;
            if (shownListIndex < 0)
            {
                shownListIndex = availableLists.Count - 1;
            }
            _ = UpdateShownList();
        }

        #endregion

        #region privates

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = LoadAvailableLists();
            _ = CheckAndShowRewardsAsync();
        }

        private async Task CheckAndShowRewardsAsync()
        {
            if (_userService.CurrentUser == null)
                return;

            var rewards = await _listService.GetOpenRewardsForUser(_userService.CurrentUser.Username);

            foreach (var reward in rewards)
            {
                var prettyMessage = FormatRewardingMessage(reward.Message);
                var list = await _listService.GetListById(reward.ListId);
                string rewardListName = list.Name;
                await DisplayAlert("List " + rewardListName + " finished", prettyMessage, "OK");
                await _listService.RemoveSeenReward(_userService.CurrentUser.Username, reward.ListId);
            }
        }

        private async Task LoadAvailableLists()
        {
            if (_userService.CurrentUser == null)
            {
                availableLists = [];

                _ = UpdateShownList();
                return;
            }

            var username = _userService.CurrentUser.Username;
            availableLists = await _listService.GetAllListsForUser(username);
            _ = UpdateShownList();
        }

        private string FormatRewardingMessage(string rawMessage)
        {
            var lines = rawMessage.Split(Environment.NewLine);
            var formattedLines = new List<string>();

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length != 3) continue;

                var username = parts[0];
                var points = parts[1];
                var place = int.TryParse(parts[2], out int p) ? p : -1;

                formattedLines.Add($"{username} – {points} Points (Place {place})");
            }

            return string.Join(Environment.NewLine, formattedLines);
        }


        private async Task UpdateShownList()
        {
            if (availableLists.Count == 0)
            {
                var emptyListEntry = new Label
                {
                    Text = "No Lists available"
                };
                ListPreview.Children.Clear();
                ListPreview.Children.Add(emptyListEntry);
                return;
            }
            var list = availableLists[shownListIndex];
            var currentUser = _userService.CurrentUser!.Username;
            var listItems = await _listItemService.GetItemsForList(list.ListID, currentUser);
            ListPreview.Children.Clear();
            ListPreview.Children.Add(new ListReadOnlyView(list, listItems));
        }
        #endregion
    }

}