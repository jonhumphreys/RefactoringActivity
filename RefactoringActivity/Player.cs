namespace RefactoringActivity;

public class Player
{
    public int Health;
    public string CurrentLocation;
    public List<string> Inventory;

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
            Console.WriteLine("Your inventory is empty.");
        }
        else
        {
            Console.WriteLine("You are carrying:");
            foreach (string item in Inventory)
            {
                Console.WriteLine($"- {item}");
            }
        }
    }
    
    public bool UseItem(string itemName)
    {
        if (Inventory.Contains(itemName))
        {
            if (itemName == "potion")
            {
                UsePotion();
            }
            else
            {
                Console.WriteLine($"The {itemName} disappears in a puff of smoke!");
            }
            Inventory.Remove(itemName);
            return true;
        }

        return false;
    }
    
    public bool TakeItem(string itemName, Location location)
    {
        if (location.GetItems().Contains(itemName))
        {
            location.GetItems().Remove(itemName);
            Inventory.Add(itemName);
            Console.WriteLine($"You take the {itemName}.");
            return true;
        }
        return false;
    }

    private void UsePotion()
    {
        Console.WriteLine("Ouch! That tasted like poison!");
        Health -= 10;
        Console.WriteLine($"Your health is now {Health}.");
    }
}