using System.Collections.Generic;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project.Interfaces
{
  public interface IRoom
  {
    string Name { get; set; }
    string Description { get; set; }
    List<Item> Items { get; set; }
    Dictionary<string, IRoom> Exits { get; set; }
    bool WandRequired { get; set; }
    string EventNoWand { get; set; }
    bool isWinningRoom { get; set; }
  }
}