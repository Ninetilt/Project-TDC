using System.Diagnostics;
using TDC.Models;
using TDC.Repositories;
using TDC.Services;

namespace TDC
{
    public partial class MainPage
    {
        private ListRepository listRepository;
        private readonly UserService _userService;
        private List<ToDoList> availableLists;
        private int shownListIndex;

        #region constructors
        public MainPage()
        {
            InitializeComponent();
            _userService = App.Services.GetService<UserService>();


            shownListIndex = 0;
            availableLists = new List<ToDoList>();
            listRepository = new ListRepository();

            LoadAvailableLists();
        }
        #endregion

        #region navigation
        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            shownListIndex = 0;
            availableLists = [];
            LoadAvailableLists();
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
            await Shell.Current.GoToAsync($"ToDoListPage?id={id}");
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            shownListIndex++;
            if (shownListIndex >= availableLists.Count)
            {
                shownListIndex = 0;
            }
            UpdateShownList();
        }

        private void OnPrevClicked(object sender, EventArgs e)
        {
            shownListIndex--;
            if (shownListIndex < 0)
            {
                shownListIndex = availableLists.Count - 1;
            }
            UpdateShownList();
        }

        #endregion

        #region privates

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadAvailableLists(); // Laden der Listen beim Anzeigen der Seite
        }
        private void LoadAvailableLists()
        {
            if (_userService.CurrentUser == null)
            {
                // Kein Benutzer eingeloggt – keine Listen anzeigen
                availableLists = new List<ToDoList>();

                // UI trotzdem updaten, damit z.B. "No Lists available" angezeigt wird
                UpdateShownList();
                return;
            }

            long userId = _userService.CurrentUser.UserId;
            availableLists = listRepository.GetAllListsForUser(userId);
            UpdateShownList();
        }

        private void UpdateShownList()
        {
            ListPreview.Children.Clear();
            if (availableLists.Count == 0)
            {
                var emptyListEntry = new Label
                {
                    Text = "No Lists available"
                };
                ListPreview.Children.Add(emptyListEntry);
                return;
            }
            var list = availableLists[shownListIndex];
            var listView = new ListReadOnlyView(list);
            ListPreview.Children.Add(listView);
        }
        #endregion
    }

}