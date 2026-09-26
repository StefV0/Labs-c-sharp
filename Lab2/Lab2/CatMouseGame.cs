using System.Text;

namespace Lab2
{
    public enum Mover
    {
        Cat,
        Mouse
    }
    internal class CatMouseGame
    {
        private bool IsMouseCatched;
        private int FieldSize;
        private int? CPos, MPos;
        private int GameOverTurn;

        public CatMouseGame(int fieldSize, int cPos, int mPos)
        {
            FieldSize = fieldSize;
            CPos = cPos;
            MPos = mPos;
        }

        public CatMouseGame(int fieldSize)
        {
            FieldSize = fieldSize;
            CPos = null;
            MPos = null;
        }

        public void iterate(Mover mover, int t)
        {
            switch (mover)
            {
                case Mover.Cat:
                    CPos = (CPos == null ? t : ((CPos + t) % FieldSize));
                    break;
                case Mover.Mouse:
                    MPos = (MPos == null ? t : ((MPos + t) % FieldSize));
                    break;
            }
            if (Distance() == 0 && CPos != null)
            {
                GameOverTurn =(int) CPos;
            }
        }

        public int? Distance() {
            if (CPos != null && MPos != null)
            {
                return Math.Abs(CPos ?? 0 - MPos ?? 0);
            } else
            {
                return null;
            }
        } 

        public string State()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{(CPos == null ? "??" : CPos):-3} {(MPos == null ? "??" : MPos):-5} {(Distance()!=null ? Distance() : ' '):-8}");
            sb.Append('\n');
            return sb.ToString();
        }

        public string FinalState()
        {
            return this.IsMouseCatched ? $"Mouse catched at turn {this.GameOverTurn}" : "Mouse evaded cat";
           
        }
       
    }
}
