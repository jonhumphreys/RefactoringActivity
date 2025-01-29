namespace RefactoringActivity;

public class World
{
    private Dictionary<string, Location> Locations;

    public World()
    {
        Locations = new Dictionary<string, Location>();
        InitializeWorld();
    }

    private void InitializeWorld()
    {
        Location start = new("Start", "You are at the starting point of your adventure.");
        Location forest = new("Forest", "You are in a dense, dark forest.");
        Location cave = new("Cave", "You see a dark, ominous cave.");

        InitializeExits(start, forest, cave);

        InitializeItems(start, forest, cave);

        start.GetPuzzles().Add(new Puzzle("riddle",
            "What's tall as a house, round as a cup, and all the king's horses can't draw it up?", "well"));

        InitializeLocations(start, forest, cave);
    }

    private void InitializeLocations(Location start, Location forest, Location cave)
    {
        Locations.Add("Start", start);
        Locations.Add("Forest", forest);
        Locations.Add("Cave", cave);
    }

    private static void InitializeItems(Location start, Location forest, Location cave)
    {
        start.GetItems().Add("map");
        forest.GetItems().Add("key");
        forest.GetItems().Add("potion");
        cave.GetItems().Add("sword");
    }

    private static void InitializeExits(Location start, Location forest, Location cave)
    {
        start.GetExits().Add("north", "Forest");
        forest.GetExits().Add("south", "Start");
        forest.GetExits().Add("east", "Cave");
        cave.GetExits().Add("west", "Forest");
    }

    public bool MovePlayer(Player player, string direction)
    {
        if (Locations[player.CurrentLocation].GetExits().ContainsKey(direction))
        {
            player.CurrentLocation = Locations[player.CurrentLocation].GetExits()[direction];
            return true;
        }

        return false;
    }

    public string GetLocationDescription(string locationName)
    {
        if (Locations.ContainsKey(locationName)) 
            return Locations[locationName].GetDescription();
        return "Unknown location.";
    }

    public string GetLocationDetails(string locationName)
    {
        if (!Locations.ContainsKey(locationName)) 
            return "Unknown location.";

        Location location = Locations[locationName];
        string details = location.GetDescription();
        
        if (location.GetExits().Count > 0)
        {
            details += " Exits lead: ";
            foreach (string exit in location.GetExits().Keys)
                details += exit + ", ";
            details = details.Substring(0, details.Length - 2);
        }

        if (location.GetItems().Count > 0)
        {
            details += "\nYou see the following items:";
            foreach (string item in location.GetItems()) 
                details += $"\n- {item}";
        }

        if (location.GetPuzzles().Count > 0)
        {
            details += "\nYou see the following puzzles:";
            foreach (Puzzle puzzle in location.GetPuzzles()) 
                details += $"\n- {puzzle.Name}";
        }

        return details;
    }

    public bool TakeItem(Player player, string itemName)
    {
        Location location = Locations[player.CurrentLocation];
        if (location.GetItems().Contains(itemName))
        {
            location.GetItems().Remove(itemName);
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
        Puzzle? puzzle = location.GetPuzzles().Find(p => p.Name == puzzleName);

        if (puzzle != null && puzzle.Solve())
        {
            location.GetPuzzles().Remove(puzzle);
            return true;
        }

        return false;
    }
}