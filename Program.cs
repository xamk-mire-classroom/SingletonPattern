using System;
using System.Collections.Generic;

// NPC Class
public class NPC
{
    public string Name { get; set; }
    public string Role { get; set; }

    public NPC(string name, string role)
    {
        Name = name;
        Role = role;
    }

    public void Speak()
    {
        Console.WriteLine($"{Name}, the {Role}, says: Hello, adventurer!");
    }
}

// GameWorld Class
public class GameWorld
{
    // Static instance for the Singleton
    private static GameWorld _instance;

    // Lock object for thread safety
    private static readonly object _lock = new object();

    // World Map represented as a grid
    public string[,] WorldMap { get; private set; }

    // List of NPCs
    public List<NPC> NPCs { get; private set; }

    // Game state variables
    public string TimeOfDay { get; set; }
    public string WeatherCondition { get; set; }

    // Private constructor to prevent instantiation
    private GameWorld()
    {
        // Initialize the world map (5x5 grid for example)
        WorldMap = new string[5, 5]
        {
            { "Forest", "Lake", "Mountain", "Cave", "Village" },
            { "Desert", "Ruins", "Castle", "Swamp", "Town" },
            { "Field", "Farm", "Road", "Hill", "Bridge" },
            { "Ocean", "Island", "Reef", "Shipwreck", "Port" },
            { "Plain", "Valley", "Cliff", "River", "Meadow" }
        };

        // Initialize the NPC list
        NPCs = new List<NPC>();

        // Set default game state variables
        TimeOfDay = "Day";
        WeatherCondition = "Clear";
    }

    // Static method to access the Singleton instance
    public static GameWorld Instance
    {
        get
        {
            // Double-checked locking for thread safety
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new GameWorld();
                    }
                }
            }
            return _instance;
        }
    }

    // Method to add an NPC to the game world
    public void AddNPC(NPC npc)
    {
        NPCs.Add(npc);
    }

    // Method to display the current game state
    public void DisplayGameState()
    {
        Console.WriteLine($"Time of Day: {TimeOfDay}");
        Console.WriteLine($"Weather Condition: {WeatherCondition}");
        Console.WriteLine("Current NPCs:");
        foreach (var npc in NPCs)
        {
            Console.WriteLine($"- {npc.Name} ({npc.Role})");
        }
    }
}

// Sample Usage
public class Program
{
    public static void Main(string[] args)
    {
        // Accessing the Singleton instance and modifying the game state
        GameWorld gameWorld = GameWorld.Instance;
        gameWorld.AddNPC(new NPC("Eldrin", "Wizard"));
        gameWorld.AddNPC(new NPC("Thalia", "Warrior"));

        // Display the game state
        gameWorld.DisplayGameState();

        // Example of NPC speaking
        foreach (var npc in gameWorld.NPCs)
        {
            npc.Speak();
        }
    }
}
