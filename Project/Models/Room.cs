using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; } = "this is a room";
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public string EventWithWand { get; set; }
    public string EventNoWand { get; set; } = "Oh no! This room is full of Death Eaters and don't have a wand to protect yourself. You see green sparks flying and the last words you hear are Avada Kedrava. You are dead.";

    public bool WandRequired { get; set; } = true;
    public bool isWinningRoom { get; set; } = false;

    public Room(string name)
    {
      Name = name;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
    }
    public void AddConnectingRoom(Room newNeighbor, string direction)
    {
      Exits.Add(direction, newNeighbor);
    }
    public bool IsWandRequired()
    {
      return WandRequired;
    }
    public string TriggerEvent(bool hasWand)
    {
      if (!hasWand)
      {
        return EventNoWand;
      }
      else
      {
        return EventWithWand;
      }
    }
  }

}