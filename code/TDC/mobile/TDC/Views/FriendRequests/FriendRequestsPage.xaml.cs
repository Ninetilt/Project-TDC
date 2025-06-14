using TDC.IService;
using TDC.Models;
using TDC.Services;

namespace TDC.Views.FriendRequests;

public partial class FriendRequestsPage : ContentPage
{
    private readonly IFriendService _friendService;
    private readonly IService.ICharacterService _characterService;
    private readonly UserService _userService;

    private string? _defaultImageUrl;

    public FriendRequestsPage(IFriendService friendService, IService.ICharacterService characterService, UserService userService)
    {
        InitializeComponent();

        _friendService = friendService;
        _characterService = characterService;
        _userService = userService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var username = _userService.CurrentUser?.Username;
        if (string.IsNullOrWhiteSpace(username)) return;

        if (string.IsNullOrWhiteSpace(_defaultImageUrl))
        {
            _defaultImageUrl = await _characterService.GetDefaultCharacterImage();
        }

        var incomingUsernames = await _friendService.GetFriendRequestsForUser(username);
        var incomingRequests = incomingUsernames
            .Select(u => new FriendRequestItem(u)
            {
                ProfileImage = ImageSource.FromUri(new Uri(_defaultImageUrl!))
            })
            .ToList();

        IncomingRequestsView.ItemsSource = incomingRequests;

        var sentUsernames = await _friendService.GetSentFriendRequestsForUser(username);
        var sentRequests = sentUsernames
            .Select(u => new FriendRequestItem(u)
            {
                ProfileImage = ImageSource.FromUri(new Uri(_defaultImageUrl!))
            })
            .ToList();

        SentRequestsView.ItemsSource = sentRequests;
    }

    private async void Accept_Clicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is FriendRequestItem friendRequestItem)
        {
            await _friendService.AcceptFriendRequest(_userService.CurrentUser!.Username, friendRequestItem.Username);
            await DisplayAlert("Success", "Friend request accepted.", "OK");
            OnAppearing();
        }
    }

    private async void Decline_Clicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is FriendRequestItem friendRequestItem)
        {
            await _friendService.DenyFriendRequest(_userService.CurrentUser!.Username, friendRequestItem.Username);
            await DisplayAlert("Success", "Friend request declined.", "OK");
            OnAppearing();
        }
    }

    private async void SendRequest_Clicked(object sender, EventArgs e)
    {
        try
        {
            var receiver = UsernameEntry.Text?.Trim();
            var senderUsername = _userService.CurrentUser!.Username;

            if (string.IsNullOrWhiteSpace(receiver))
            {
                await DisplayAlert("Error", "Please enter a valid username.", "OK");
                return;
            }

            if (receiver.Equals(senderUsername, StringComparison.OrdinalIgnoreCase))
            {
                await DisplayAlert("Error", "You cannot send a request to yourself.", "OK");
                return;
            }

            var exists = await _friendService.AccountExists(receiver);
            if (!exists)
            {
                await DisplayAlert("Error", $"User '{receiver}' does not exist.", "OK");
                return;
            }

            var friends = await _friendService.GetFriendsForUser(senderUsername);
            if (friends.Contains(receiver, StringComparer.OrdinalIgnoreCase))
            {
                await DisplayAlert("Info", $"You are already friends with User {receiver}.", "OK");
                return;
            }

            var alreadySentRequests = await _friendService.GetSentFriendRequestsForUser(senderUsername);
            if (alreadySentRequests.Contains(receiver, StringComparer.OrdinalIgnoreCase))
            {
                await DisplayAlert("Info", $"You already sent a request to User {receiver}.", "OK");
                return;
            }

            var incomingRequests = await _friendService.GetFriendRequestsForUser(senderUsername);
            if (incomingRequests.Contains(receiver, StringComparer.OrdinalIgnoreCase))
            {
                await DisplayAlert("Info", $"User {receiver} already sent you a friend request. Accept or Decline it.", "OK");
                return;
            }

            await _friendService.SendFriendRequest(receiver, senderUsername);
            await DisplayAlert("Request Sent", $"You sent a request to {receiver}.", "OK");
            UsernameEntry.Text = string.Empty;

            OnAppearing();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Could not send friend request.", "OK");
            Console.WriteLine("Error sending request: " + ex);
        }
    }

    private async void CancelSentRequest_Clicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is FriendRequestItem friendRequestItem)
        {
            try
            {
                var senderUsername = _userService.CurrentUser!.Username; 

                await _friendService.CancelFriendRequest(friendRequestItem.Username, senderUsername);
                await DisplayAlert("Success", "Request cancelled.", "OK");

                OnAppearing();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Could not cancel request.", "OK");
                Console.WriteLine("Cancel error: " + ex);
            }
        }
    }
}