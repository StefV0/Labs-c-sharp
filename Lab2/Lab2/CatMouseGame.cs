using System.Diagnostics.Metrics;
using System.Text;

namespace Lab2
{
    public enum GameStatus
    {
        Playing,
        Ended
    }

    struct Command
    {
        public char command;
        public int argument;

        public Command(char c, int turn)
        {
            command = c;
            argument = turn;
        }
        public Command(string[] ar)
        {
            command = ar[0][0];
            if (ar.Length == 2) argument = Convert.ToInt32(ar[1]);
        }
    }

    internal class CatMouseGame
    {
        private GameStatus st;
        private int FieldSize;
        private Player cat;
        private Player mouse;
        private int GameOverTurn;


        public CatMouseGame(int fieldSize)
        {
            FieldSize = fieldSize;
            cat = new Player("Cat"); ;
            mouse = new Player("Mouse");
            st = GameStatus.Playing;
        }

        public void move(Command c)
        {
            switch (c.command)
            {
                case 'C':
                    cat.Move(FieldSize, c.argument);
                    break;
                case 'M':
                    mouse.Move(FieldSize, c.argument);
                    break;
            }
            if (Distance() == 0)
            {
                GameOverTurn = cat.Location;
                cat.Stat = Status.Winner;
                mouse.Stat = Status.Loser;
                st = GameStatus.Ended;
            }
        }

        public int? Distance()
        {
            if ((cat.Stat == Status.Playing) && (mouse.Stat == Status.Playing))
            {
                return Math.Abs(cat.Location - mouse.Location);
            }
            else
            {
                return null;
            }
        }



        public string State()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{(cat.Stat == Status.NotInGame ? "??" : cat.Location),-3} {(mouse.Stat == Status.NotInGame ? "??" : mouse.Location),-5} {((Distance() != null) ? Distance() : " "),-8}");
            sb.Append('\n');
            return sb.ToString();
        }

        public string FinalState()
        {
            return cat.Stat == Status.Winner ? $"Mouse catched at cell: {this.GameOverTurn}" : "Mouse evaded cat";

        }

        public string GetCatMouseDistance()
        {
            StringBuilder b = new StringBuilder();
            b.Append("Distance traveled:\tMouse\tCat\n");
            b.Append($"                  \t{mouse.Distance,-5}\t{mouse.Distance,-3}\n");
            return b.ToString();
        }

        public StringBuilder RunGame(List<Command> commands)
        {
            StringBuilder file_text = new StringBuilder();
            file_text.Append("Cat Mouse Game\n");
            file_text.Append("\nCat Mouse Distance\n");
            file_text.Append(new string('-', 15) + "\n");
            foreach (Command c in commands)
            {
                if (st == GameStatus.Playing)
                {
                    switch (c.command)
                    {
                        case 'P':
                            file_text.Append(State());
                            break;
                        default:
                            move(c);
                            break;
                    }
                }
                else break;

            }

            if (st != GameStatus.Ended)
            {
                mouse.Stat = Status.Winner;
                cat.Stat = Status.Loser;
                st = GameStatus.Ended;
            }


            file_text.Append(new string('-', 15));
            file_text.Append("\n\n");
            file_text.Append(GetCatMouseDistance());
            file_text.Append("\n");
            file_text.Append(FinalState());
            return file_text;
        }

        public int? getCatPos()
        {
            return cat.Location;
        }
        public int? getMousePos()
        {
            return mouse.Location;
        }
        public int getCatDistance() { return cat.Distance; }
        public int getMouseDistance() { return mouse.Distance; }
    }
}
