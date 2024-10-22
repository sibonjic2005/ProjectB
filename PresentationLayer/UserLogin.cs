using Spectre.Console;
static class UserLogin
{
    public static void Login()
    {
        Log_in_Checker login = new Log_in_Checker();

        // var email = login.ValidateEmail(() => 
        //     AnsiConsole.Prompt(new TextPrompt<string>("Enter your email address:"))
        // );
 
        // var password = login.PasswordRules(() => 
        //     AnsiConsole.Prompt(new TextPrompt<string>("Enter your password: ").Secret())
        // );
 
 
 
 
        // Console.WriteLine($"email: {email}, password: {password}");
 
        //call logic method and see if user exists
        //if exsist go to user menu
        //else no account exists
    }
}