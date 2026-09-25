using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    internal class CatMouseGame
    {
        int FieldSize;
        int? CPos, MPos;

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
    }
}
