using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Lab2
{
    enum Status
    {
        Winner,
        Loser,
        Playing,
        NotInGame
    }
    internal class Player
    {
        public string Name { get; }
        public int Location { get; private set; }
        public Status Stat { get; set; }
        public int Distance { get; private set; }

        public Player(string name)
        {
            this.Name = name;
            this.Stat = Status.NotInGame;
        }

        public void Move(int size, int arg)
        {
            if (Stat == Status.NotInGame) { Location = Math.Abs(arg); Stat = Status.Playing; } else { Location = (Location + arg % size + size) % size; Distance += Math.Abs(arg); }
        }

    }
}
