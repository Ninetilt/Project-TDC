using TDC.Models;
namespace TDC;

public partial class ListReadOnlyView
{
	public ListReadOnlyView(ToDoList list)
    {
        InitializeComponent();
        this.FindByName<Label>("TitleLabel").Text = list.Name;
        this.FindByName<Label>("PointsLabel").Text = GetListPoints(list).ToString();
        this.FindByName<Label>("AllPointsLabel").Text = GetAllListPoints(list).ToString();
        InitListItems(list);
	}

    #region privates

    private void InitListItems(ToDoList list)
    {
        foreach (var listItemView in list.GetItems().Select(listItem => new ListItemReadOnlyView(listItem)
                 {
                     MaximumHeightRequest = 42,
                 }))
        {
            ItemsContainer.Children.Add(listItemView);
        }
    }

    private static int GetListPoints(ToDoList list)
    {
        return list
            .GetItems()
            .Where(item => item.IsDone())        // nur erledigte Items
            .Sum(item => item.GetEffort() * 5);
    }

    private static int GetAllListPoints(ToDoList list)
    {
        return list
            .GetItems()
            .Sum(item => item.GetEffort() * 5);
    }
    #endregion
}