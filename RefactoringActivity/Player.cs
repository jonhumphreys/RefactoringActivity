namespace RefactoringActivity;

public class Player
{
    public int Health;
    public string CurrentLocation;
    public List<string> Inventory { get; private set; }

    public Player(int health)
    {
        Health = health;
        CurrentLocation = "Start";
        Inventory = new List<string>();
    }

    public void ShowInventory()
    {
        if (Inventory.Count == 0)
        {
            PrintInventoryEmpty();
        }
        else
        {
            PrintInventoryFull();
        }
    }

    private void PrintInventoryFull()
    {
        Console.WriteLine("You are carrying:");
        foreach (string item in Inventory)
        {
            Console.WriteLine($"- {item}");
        }
    }

    private static void PrintInventoryEmpty()
    {
        Console.WriteLine("Your inventory is empty.");
    }
}