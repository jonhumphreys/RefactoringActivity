namespace RefactoringActivity;

public class GameManager
{
    private const int DefaultPlayerHealth = 100;
    private const string CommandPrompt = "> ";
    public bool IsRunning;
    public Player Player;
    public World World;
    
    public void RunGame()
    {
        InitializeGame();

        while (IsRunning)
        {
            Console.WriteLine();
            Console.WriteLine(World.GetLocationDetails(Player.CurrentLocation));
            Console.Write(CommandPrompt);
            ProcessCommand(Console.ReadLine()?.ToLower());
        }
    }
    private void InitializeGame()
    {
        IsRunning = true;
        Player = new Player(DefaultPlayerHealth);
        World = new World();
        
        Console.WriteLine("Welcome to the Text Adventure Game!");
        Console.WriteLine("Type 'help' for a list of commands.");
    }

    private void ProcessCommand(string input)
    {
        if (string.IsNullOrEmpty(input))
            return;

        (string command, string argument) = ParseInput(input);

        switch (command)
        {
            case "help":
                DisplayHelp();
                break;
            
            case "go":
                MovePlayer(argument);
                break;
            
            case "take":
                TakeItem(argument);
                break;
            
            case "use":
                UserItem(argument);
                break;
            
            case "inventory":
                Player.ShowInventory();
                break;
            
            case "solve":
                SolvePuzzle(argument);
                break;
            
            case "quit":
                ExitGame();
                break;
            default:
                Console.WriteLine("Unknown command. Try 'help'.");
                break;
        }
    }

    private (string command, string argument) ParseInput(string input)
    {
        string[] parts = input.Split(' ', 2);
        string command = parts[0];
        string argument = parts.Length > 1 ? parts[1] : string.Empty;
        return (command, argument);
    }

    private void DisplayHelp()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("- go [direction]: Move in a direction (north, south, east, west).");
        Console.WriteLine("- take [item]: Take an item from your current location.");
        Console.WriteLine("- use [item]: Use an item in your inventory.");
        Console.WriteLine("- solve [puzzle]: Solve a puzzle in your current location.");
        Console.WriteLine("- inventory: View the items in your inventory.");
        Console.WriteLine("- quit: Exit the game.");
    }

    private void MovePlayer(string direction)
    {
        if (string.IsNullOrEmpty(direction))
        {
            Console.WriteLine("Move where? (north, south, east, west)");
            return;
        }
        if (World.MovePlayer(Player, direction))
            Console.WriteLine($"You move {direction}.");
        else
            Console.WriteLine("You can't go that way.");
    }

    private void TakeItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            Console.WriteLine("Take what?");
            return;
        }
        
        if (!World.TakeItem(Player, itemName))
            Console.WriteLine($"There is no {itemName} here.");
    }

    private void UserItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            Console.WriteLine("Use what?");
            return;
        }

        if (!World.UseItem(Player, itemName))
            Console.WriteLine($"You can't use the {itemName} here.");
    }

    private void SolvePuzzle(string puzzleName)
    {
        if (string.IsNullOrEmpty(puzzleName))
        {
            Console.WriteLine("Solve what?");
            return;
        }

        if (World.SolvePuzzle(Player, puzzleName))
            Console.WriteLine($"You solved the {puzzleName} puzzle!");
        else
            Console.WriteLine($"That's not the right solution for the {puzzleName} puzzle.");
    }

    private void ExitGame()
    {
        IsRunning = false;
        Console.WriteLine("Thanks for playing!");
    }
}