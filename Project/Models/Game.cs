using System.Collections.Generic;
using System;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  class Game
  {
    public IRoom CurrentRoom { get; set; }
    public bool UsingCloak = false;
    public Game(string name)
    {
      Console.Clear();
      Console.WriteLine($"\n\t{name}, Welcome to Hogwarts Castle. The evil dark wizard\n\t Lord Voldemort has sent every Death Eater and Dark creature\n\t to launch an attack on the castle. Dumbledore's Army, the Order\n\t of the Phoenix, and Harry Potter fight to save the fate of the\n\t wizarding world, but they need your help. Inside the castle is\n\t one of Lord Voldemort's final horcruxes. Find the horcrux and\n\t destroy it so Harry can defeat Lord Voldemort once and for all.");
      Room center = new Room("Entrance Hall");
      Room r1 = new Room("Courtyard");
      Room r2 = new Room("Staffroom");
      Room r3 = new Room("Great Hall");
      Room r4 = new Room("Second Floor corridor");
      Room r6 = new Room("r6");
      center.WandRequired = false;
      r2.WandRequired = false;
      Item wand = new Item("Wand", "13inches, Cypress wood, unicorn hair core");
      wand.UseText = "STUPEFY! You've stunned the Death Eaters. But it won't last long. You should move quickly.";
      Item sword = new Item("SwordOfGryffindor", "Originally owned by Godric Gryffindor, this sword is Goblin made and so powerful it can destroy a Horcrux.");
      Item horcrux = new Item("Horcrux", "Orginally Rowena Ravenclaw's Diadem, Voldemort defiled this tiara with dark and powerful magic. Destroy it and the Dark Lord with be mortal again.");
      Item boggart = new Item("Boggart", "A shape-shifting creature that assumes the form of the viewer's worst fear.");
      r2.Items.Add(wand);
      r2.Items.Add(boggart);
      r3.Items.Add(sword);
      r6.Items.Add(horcrux);
      r1.Description = "You've walked outside into the courtyard. Be careful, you are exposed out here.";
      r2.Description = "The staffroom has two stone gargoyles guarding the front. There is a large wardrobe in the corner and you can hear something rattling inside it. Laying on one of the tables is wand.";
      r3.Description = "You've walked into the Great Hall. Normally filled with four giant long tables, there are now pieces of rubble everywhere. Among the rubble you notice there is a sword sticking out.";
      r4.Description = "You've walked up the stairwell that leads to the fourth floor corridor. As you are walking the staircase moves. It stops on the second floor. You see a Death Eater turn the corner spots you. Grab your wand.";
      center.Description = "This is the entrance to the castle. It's surrounded by giant staircases and stone walls. You can go in any direction, but be careful. There are Death Eaters all over the castle";
      r6.Description = "You've reached Ravenclaw Tower. In the corner you see something sparkle as there is a flash outside. You walk closer and you see it's a tiara. Could it be the Lost Diadem of Ravenclaw? Take a closer look.";


      center.AddConnectingRoom(r1, "north");
      center.AddConnectingRoom(r2, "east");
      center.AddConnectingRoom(r3, "south");
      center.AddConnectingRoom(r4, "west");
      r1.AddConnectingRoom(center, "south");
      r2.AddConnectingRoom(center, "west");
      r3.AddConnectingRoom(r6, "south");
      r3.AddConnectingRoom(center, "north");
      r4.AddConnectingRoom(center, "east");
      r6.AddConnectingRoom(r3, "north");
      r6.isWinningRoom = true;
      CurrentRoom = center;
    }
  }
}