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
        private bool IsMouseCatched = false;
        private int FieldSize;
        private int? CPos, MPos;
        private int GameOverTurn;
        private int CatDistance;
        private int MouseDistance;

        public CatMouseGame(int fieldSize, int cPos, int mPos)
        {
            FieldSize = fieldSize;
            CPos = cPos;
            MPos = mPos;
            CatDistance = 0;
            MouseDistance = 0;
        }

        public CatMouseGame(int fieldSize)
        {
            FieldSize = fieldSize;
            CPos = null;
            MPos = null;
            CatDistance = 0;
            MouseDistance = 0;
        }

        public void iterate(Mover mover, int t)
        {
            switch (mover)
            {
                case Mover.Cat:
                    CatDistance += (CPos == null ? 0 : Math.Abs(t));
                    CPos = (CPos == null ? Math.Abs(t) : (CPos + t % FieldSize + FieldSize)%FieldSize);
                    break;
                case Mover.Mouse:
                    MouseDistance += (MPos == null ? 0 : Math.Abs(t));
                    MPos = (MPos == null ? Math.Abs(t) : (MPos + t % FieldSize + FieldSize) % FieldSize);
                    break;
            }
            if (Distance() == 0 && CPos != null)
            {
                GameOverTurn = (int) CPos;
                IsMouseCatched = true;
            }
        }

        public int? Distance() {
            if (CPos != null && MPos != null)
            {
                return Math.Abs((CPos ?? 0) - (MPos ?? 0));
            } else
            {
                return null;
            }
        } 

        public string State()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{(CPos == null ? "??" : CPos),-3} {(MPos == null ? "??" : MPos),-5} {((Distance()!=null) ? Distance() : " "),-8}");
            sb.Append('\n');
            return sb.ToString();
        }

        public string FinalState()
        {
            return this.IsMouseCatched ? $"Mouse catched at cell: {this.GameOverTurn}" : "Mouse evaded cat";
           
        }
       
        public string GetCatMouseDistance()
        {
            StringBuilder b = new StringBuilder();
            b.Append("Distance traveled:\tMouse\tCat\n");
            b.Append($"                  \t{MouseDistance,-5}\t{CatDistance,-3}\n");
            return b.ToString();
        }
    }
}
