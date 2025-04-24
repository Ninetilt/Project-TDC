namespace TDC.Models;
public class ListItem(string description, bool done, List<Profile> finishedMembers, int effort)
{
    #region constructors
    public ListItem() : this("", false, [], 0) {}

    public ListItem(string description, List<Profile> finishedMembers, int effort) : this(description, false, finishedMembers, effort) {}

    #endregion

    #region getters & setters
    public void SetDescription(string newDescription)
    {
        description = newDescription;
    }

    public string GetDescription()
    {
        return description;
    }

    public void ToggleDone()
    {
        done = !done;
    }

    public bool IsDone()
    {
        return done;
    }

    public void AddFinishedMember(Profile member)
    {
        finishedMembers.Add(member);
    }

    public void RemoveFinishedMember(Profile member)
    {
        finishedMembers.Remove(member);
    }

    public List<Profile> GetFinishedMembers()
    {
        return finishedMembers;
    }

    public int GetEffort() {
        return effort;
    }

    public void SetEffort(int newEffort) { 
        effort = newEffort;
    }
    #endregion
}
