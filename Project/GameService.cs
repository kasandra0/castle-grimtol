using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    private bool StillPlaying { get; set; } = true;
    private bool NewGame { get; set; } = true;

    #region Game Control
    //Initializes the game, creates rooms, their exits, and add items to rooms
    public void Setup()
    {
      Game game = new Game(CurrentPlayer.PlayerName);
      CurrentRoom = game.CurrentRoom;
      StartGame();
    }
    //Stops the application
    public void Quit()
    {
      StillPlaying = false;
    }
    //Restarts the game
    public void Reset()
    {
      Player newPlayer = new Player(CurrentPlayer.PlayerName);
      CurrentPlayer = newPlayer;
      Game game = new Game(CurrentPlayer.PlayerName);
      CurrentRoom = game.CurrentRoom;
      StillPlaying = true;
    }

    public void EndGame(bool winner)
    {
      if (winner)
      {
        System.Console.WriteLine("You destroyed the horcrux! Harry Potter defeated the Dark Lord. The Wizarding World is safe now.");
      }
      else
      {
        System.Console.WriteLine("You Died.");
      }
      StillPlaying = false;
    }
    //Setup and Starts the Game loop
    public void StartGame()
    {
      while (NewGame)
      {
        while (StillPlaying)
        {
          GetUserInput();
        }
        System.Console.WriteLine("Do you want to play again? (y/n)");
        string answer = Console.ReadLine();
        if (answer == "n")
        {
          break;
        }
        if (answer == "y")
        {
          Reset();
        }
      }
    }
    public void GetUserInput()
    {
      Console.Write($"\n\t What do you want to do? ");
      string[] input = Console.ReadLine().ToLower().Split(' ');
      Console.Clear();
      switch (input[0])
      {
        case "look":
          Console.WriteLine("\tinput: LOOK");
          Look();
          break;
        case "quit":
          Console.WriteLine("\tinput: QUIT");
          Quit();
          break;
        case "help":
          Console.WriteLine("\tinput: HELP");
          Help();
          break;
        case "go":
          Console.WriteLine("\tinput: GO");
          if (input.Length > 1)
          {
            Go(input[1]);
          }
          break;
        case "take":
          Console.WriteLine("\tinput: TAKE");
          if (input.Length > 1)
          {
            TakeItem(input[1]);
          }
          break;
        case "use":
          Console.WriteLine("\tinput: USE");
          UseItem(input[1]);
          break;
        case "inventory":
          Inventory();
          break;
        default:
          Console.WriteLine("Invalid input");
          break;
      }
    }
    #endregion

    #region Console Commands
    public void Go(string direction)
    {
      if (!CurrentRoom.Exits.ContainsKey(direction))
      {
        Console.WriteLine("Cannot GO in that direction");
      }
      else
      {
        CurrentRoom = CurrentRoom.Exits[direction];
        Console.WriteLine(CurrentRoom.Description);
        if (CurrentRoom.WandRequired && !CurrentPlayer.HasItem("wand"))
        {
          Console.WriteLine($"{CurrentRoom.EventNoWand}");
          EndGame(false);
        }
      }

    }

    public void Help()
    {
      System.Console.WriteLine($"LOOK: Prints the description of the room and any items that may be in the room");
      System.Console.WriteLine($"GO <direction>: move in one of four directions(north, south, east, west)");
      System.Console.WriteLine($"USE <item name>: Uses the item from your inventory");
      System.Console.WriteLine($"TAKE <item name>: Places the item into your inventory and removes it from the room");
      System.Console.WriteLine($"INVENTORY: displays all the items currently in your inventory");
      System.Console.WriteLine($"QUIT: ends the current game");
    }

    public void Inventory()
    {
      Console.Write($"\tItems in your Inventory:\t");
      CurrentPlayer.Inventory.ForEach(i => Console.Write($"  {i.Name}"));
      Console.WriteLine("");
    }

    public void Look()
    {
      Console.WriteLine(CurrentRoom.Description);
      if (CurrentRoom.Items.Count > 0)
      {
        Console.Write("\tItems in this room:\n");
        CurrentRoom.Items.ForEach(i => Console.Write($" \t {i.Name}: {i.Description}\n"));
        Console.WriteLine("");
      }
    }

    public void TakeItem(string itemName)
    {
      Item item = CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName.ToLower());
      if (item != null)
      {
        CurrentPlayer.Inventory.Add(item);
        CurrentRoom.Items.Remove(item);
      }
      else
      {
        Console.WriteLine($"\tNo {itemName} in this room");
      }
    }

    public void UseItem(string itemName)
    {
      Item item = CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName.ToLower());
      Item h = CurrentRoom.Items.Find(i =>
      {
        System.Console.WriteLine($"item {i.Name}");
        return i.Name.ToLower() == "horcrux";
      });
      if (item != null && h != null)
      {
        CurrentRoom.Items.Remove(h);
        if (CurrentRoom.isWinningRoom)
        {
          EndGame(true);
          Console.WriteLine("You Destroyed the horcrux");
        }
      }
      else
      {
        Console.WriteLine($"Cannot find {itemName}");
      }
    }
    #endregion

    public GameService()
    {
      Console.Write("Enter player username: ");
      string name = Console.ReadLine();
      CurrentPlayer = new Player(name);
      Setup();
    }
  }
}