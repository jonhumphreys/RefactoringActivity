namespace RefactoringActivity;

public class World
{
    public Dictionary<string, Location> Locations;
    private Location start;
    private Location forest;
    private Location cave;

    public World()
    {
        Locations = new Dictionary<string, Location>();
        InitializeWorld();
    }

    private void InitializeWorld()
    {
        InitializeLocations();
        AddExits();
        AddItems();

        start.Puzzles.Add(new Puzzle("riddle",
            "What's tall as a house, round as a cup, and all the king's horses can't draw it up?", "well"));

        AddLocations();
    }

    private void InitializeLocations()
    {
        start = new("You are at the starting point of your adventure.");
        forest = new("You are in a dense, dark forest.");
        cave = new("You see a dark, ominous cave.");
    }

    private void AddExits()
    {
        start.Exits.Add("north", "Forest");
        forest.Exits.Add("south", "Start");
        forest.Exits.Add("east", "Cave");
        cave.Exits.Add("west", "Forest");
    }

    private void AddItems()
    {
        start.Items.Add("map");
        forest.Items.Add("key");
        forest.Items.Add("potion");
        cave.Items.Add("sword");
    }

    private void AddLocations()
    {
        Locations.Add("Start", start);
        Locations.Add("Forest", forest);
        Locations.Add("Cave", cave);
    } 

    public bool MovePlayer(Player player, string direction)
    {
        if (Locations[player.CurrentLocation].Exits.ContainsKey(direction))
        {
            player.CurrentLocation = Locations[player.CurrentLocation].Exits[direction];
            return true;
        }

        return false;
    }

    public string GetLocationDescription(string locationName)
    {
        if (Locations.ContainsKey(locationName)) 
            return Locations[locationName].Description;
        return "Unknown location.";
    }

    public string GetLocationDetails(string locationName)
    {
        if (!Locations.ContainsKey(locationName)) 
            return "Unknown location.";

        Location location = Locations[locationName];
        string details = location.Description;
        
        details = Details(location, details);

        return details;
    }

    private static string Details(Location location, string details)
    {
        details = AddExitDetails(location, details);
        details = AddItemsDetails(location, details);
        details = AddPuzzleDetails(location, details);

        return details;
    }

    private static string AddPuzzleDetails(Location location, string details)
    {
        if (location.Puzzles.Count > 0)
        {
            details += "\nYou see the following puzzles:";
            foreach (Puzzle puzzle in location.Puzzles) 
                details += $"\n- {puzzle.Name}";
        }

        return details;
    }

    private static string AddItemsDetails(Location location, string details)
    {
        if (location.Items.Count > 0)
        {
            details += "\nYou see the following items:";
            foreach (string item in location.Items) 
                details += $"\n- {item}";
        }

        return details;
    }

    private static string AddExitDetails(Location location, string details)
    {
        if (location.Exits.Count > 0)
        {
            details += " Exits lead: ";
            foreach (string exit in location.Exits.Keys)
                details += exit + ", ";
            details = details.Substring(0, details.Length - 2);
        }

        return details;
    }

    public bool TakeItem(Player player, string itemName)
    {
        Location location = Locations[player.CurrentLocation];
        if (location.Items.Contains(itemName))
        {
            location.Items.Remove(itemName);
            player.Inventory.Add(itemName);
            Console.WriteLine($"You take the {itemName}.");
            return true;
        }

        return false;
    }

    public bool UseItem(Player player, string itemName)
    {
        if (player.Inventory.Contains(itemName))
        {
            if (itemName == "potion")
            {
                Console.WriteLine("Ouch! That tasted like poison!");
                player.Health -= 10;
                Console.WriteLine($"Your health is now {player.Health}.");
            }
            else
            {
                Console.WriteLine($"The {itemName} disappears in a puff of smoke!");
            }
            player.Inventory.Remove(itemName);
            return true;
        }

        return false;
    }

    public bool SolvePuzzle(Player player, string puzzleName)
    {
        Location location = Locations[player.CurrentLocation];
        Puzzle? puzzle = location.Puzzles.Find(p => p.Name == puzzleName);

        if (puzzle != null && puzzle.Solve())
        {
            location.Puzzles.Remove(puzzle);
            return true;
        }

        return false;
    }
}