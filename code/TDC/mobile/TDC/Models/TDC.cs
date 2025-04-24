namespace TDC.Models;
public class TDC
{
    private readonly Account? currentUser;

    #region constructors
    public TDC() { 
    
    }

    public TDC(Account currentUser) { 
        this.currentUser = currentUser;
    }
    #endregion

    #region getters & setters
    public Account? GetCurrentUser()
    {
        return currentUser;
    }
    #endregion
}
