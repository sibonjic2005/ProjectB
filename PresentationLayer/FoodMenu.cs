using Spectre.Console;

static class Foodmenu
{
    public static void ShowMenu()
    {
        var panel = new Panel("Menu");
        panel.Header = new PanelHeader("Food Menu");
        Console.WriteLine();
    }
}