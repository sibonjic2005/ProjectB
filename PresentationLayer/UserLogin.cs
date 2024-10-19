using Spectre.Console;
static class UserLogin
{
    public static void Login()
    {
        var email = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your email address: "));

        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your password: ")
                .Secret());




        Console.WriteLine($"email: {email}, password: {password}");

        //call logic method and see if user exists
        //if exsist go to user menu
        //else no account exists
    }
}