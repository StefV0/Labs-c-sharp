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
        public Status Stat { get; private set; }
        public int Distance { get; private set; }

        public Player(string name)
        {
            this.Name = name;
        }

    }
}
