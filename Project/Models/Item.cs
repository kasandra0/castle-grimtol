using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Item : IItem
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public string UseText { get; set; } = "";
    public Item(string name, string desc)
    {
      Name = name;
      Description = desc;
    }
    public void Use()
    {
      Console.WriteLine($"{UseText}");
    }
  }
}