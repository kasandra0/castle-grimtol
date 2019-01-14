using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Player : IPlayer
  {
    public string PlayerName { get; set; }
    public List<Item> Inventory { get; set; }

    public Player(string name)
    {
      PlayerName = name;
      Inventory = new List<Item>();
    }
    public bool HasItem(string itemName)
    {
      if (Inventory.Exists(item => itemName.ToLower() == item.Name.ToLower()))
      {
        return true;
      }
      return false;
    }
    public void Use(string itemName)
    {
      Item item = Inventory.Find(i => i.Name.ToLower() == itemName.ToLower());

    }
  }
}