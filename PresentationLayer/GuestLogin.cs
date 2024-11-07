using Spectre.Console;
class GuestLogin
{
    public static void LoginGuest()
    {      
        //call logic method and see if user exists
        //if exsist go to user menu
        //else no account exists
        CreateAccount.CreateGuestAcc();
    }
}