namespace RefactoringActivity;

public class Player
{
    private int _health;
    private string _currentLocation;
    private List<string> _inventory;

    public Player(int health)
    {
        _health = health;
        _currentLocation = "Start";
        _inventory = new List<string>();
    }

    public void ShowInventory()
    {
        if (InventoryIsEmpty())
        {
            Console.WriteLine("Your inventory is empty.");
        }
        else
        {
            Console.WriteLine("You are carrying:");
            foreach (string item in _inventory)
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

    public int GetHealth()
    {
        return _health;
    }

    public void SetHealth(int health)
    {
        _health = health;
    }

    public string GetCurrentLocation()
    {
        return _currentLocation;
    }

    public void SetCurrentLocation(string location)
    {
        _currentLocation = location;
    }

    public List<string> GetInventory()
    {
        return _inventory;
    }

    public void AddInventoryItem(string item)
    {
        _inventory.Add(item);
    }

    public void RemoveInventoryItem(string item)
    {
        _inventory.Remove(item);
    }

    public bool HasItem(string item)
    {
        return _inventory.Contains(item);
    }

    private bool InventoryIsEmpty()
    {
        return _inventory.Count == 0;
    }
}
