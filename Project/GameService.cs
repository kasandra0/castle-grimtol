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

    }

    public void EndGame(bool winner)
    {
      if (winner)
      {
        System.Console.WriteLine("You destroyed the horcrux! Harry Potter defeated the Dark Lord. The Wizarding World is safe now.");
      }
      else
      {
        System.Console.WriteLine("You Died. Enter reset to play again");
      }
      StillPlaying = false;
    }
    //Setup and Starts the Game loop
    public void StartGame()
    {
      while (StillPlaying)
      {
        GetUserInput();
      }
    }
    public void GetUserInput()
    {
      Console.Write($"You're in the {CurrentRoom.Name} \n\t What do you want to do? ");
      string[] input = Console.ReadLine().ToLower().Split(' ');
      Console.Clear();
      switch (input[0])
      {
        case "look":
          Console.WriteLine("action: LOOK");
          Look();
          break;
        case "quit":
          Console.WriteLine("action: QUIT");
          Quit();
          break;
        case "help":
          Console.WriteLine("action: HELP");
          break;
        case "go":
          Console.WriteLine("action: GO");
          if (input.Length > 1)
          {
            Go(input[1]);
          }
          break;
        case "take":
          Console.WriteLine("action: TAKE");
          if (input.Length > 1)
          {
            TakeItem(input[1]);
          }
          break;
        case "use":
          Console.WriteLine("action: USE");
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
        if (CurrentRoom.WandRequired && !CurrentPlayer.HasItem("wand"))
        {
          Console.WriteLine($"{CurrentRoom.EventNoWand}");
        }
        Console.WriteLine(CurrentRoom.Description);
      }

    }

    public void Help()
    {

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
      Console.WriteLine("\tYou can go: ");
      foreach (var keypair in CurrentRoom.Exits)
      {
        Console.WriteLine($"\t{keypair.Key} into {keypair.Value.Name}");
      }
      if (CurrentRoom.Items.Count > 0)
      {
        Console.Write("\tItems in this room:\t");
        CurrentRoom.Items.ForEach(i => Console.Write($" {i.Name}"));
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
      if (item == null)
      {
        System.Console.WriteLine($"no match: {itemName}");
      }
      if (h == null)
      {
        System.Console.WriteLine($"no match: horcux");
      }
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