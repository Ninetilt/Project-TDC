using TDC.Models;
namespace TDC;

public partial class ListItemView 
{
    public event EventHandler? NewItemOnEnter;
    public event EventHandler? EffortChanged;
    public bool IsInitialized;
    private readonly ListItem item;

    #region constructors
    public ListItemView(ListItem item)
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
        item.SetDescription(this.FindByName<Entry>("TaskEntry").Text);
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
            item.ToggleDone();
        }
    }

    #endregion

    #region privates

    private void SetComponentProperties()
    {
        this.FindByName<Entry>("TaskEntry").Text = item.GetDescription();
        this.FindByName<CheckBox>("TaskCheckBox").IsChecked = item.IsDone();
        this.FindByName<Picker>("TaskPicker").SelectedIndex = item.GetEffort() - 1;
    }

    private void SetEventHandlers()
    {
        LayoutChanged += (_, _) =>
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
        item.SetEffort(selectedIndex + 1);
        EffortChanged?.Invoke(this, e);
    }

    #endregion

    #region publics
    public ListItem GetItem()
    {
        return item;
    }
    #endregion
}