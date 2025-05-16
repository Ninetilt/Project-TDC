namespace TDC.Views.ListItem;

public partial class ListItemView 
{
    public event EventHandler? NewItemOnEnter;
    public event EventHandler? EffortChanged;
    public event EventHandler? DeletePressed;
    public event EventHandler? CheckBoxChanged;
    public bool IsInitialized;
    private readonly Models.ListItem item;

    #region constructors
    public ListItemView(Models.ListItem item)
    {
        IsInitialized = false;
        this.item = item;

        InitializeComponent();
        SetComponentProperties();
        SetEventHandlers();
    }

    #endregion

    #region event listeners

    private void DescriptionChanged(object sender, EventArgs e)
    {
        item.Description = this.FindByName<Entry>("TaskEntry").Text;
    }

    private void EnterPressed(object sender, EventArgs e)
    {
        DescriptionChanged(sender, e);
        NewItemOnEnter?.Invoke(this, e);
    }

    private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (IsInitialized)
        {
            item.IsDone = !item.IsDone;
            CheckBoxChanged?.Invoke(this, e);
        }
    }

    //TODO: ONLY FOR TESTING ON WINDOWS, CAN BE DELETED LATER ON
    private void OnDeletePressed(object sender, EventArgs e)
    {
        DeletePressed?.Invoke(this,e);
    }

    #endregion

    #region privates

    private void SetComponentProperties()
    {
        this.FindByName<Entry>("TaskEntry").Text = item.Description;
        this.FindByName<CheckBox>("TaskCheckBox").IsChecked = item.IsDone;
        this.FindByName<Picker>("TaskPicker").SelectedIndex = item.Effort - 1;
    }

    private void SetEventHandlers()
    {
        SizeChanged += (_, _) =>
        {
            this.FindByName<Entry>("TaskEntry").Focus();
        };
    }
    #endregion

    #region Picker
    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedIndex = picker.SelectedIndex;

        if (selectedIndex == -1 || !IsInitialized) return;
        item.Effort = selectedIndex + 1;
        EffortChanged?.Invoke(this, e);
    }

    #endregion

    #region publics
    public Models.ListItem GetItem()
    {
        return item;
    }
    #endregion
}