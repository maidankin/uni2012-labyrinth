using System;
using System.Drawing;

namespace Labyrinth
{
	/// <summary>
	/// Summary description for Cell.
	/// </summary>
	public class Cell
	{
        public static int kCellSize = 20;
		public static int kPadding = 5;
		public int[] Walls  = new int[4]{1, 1, 1, 1};
		public int Row;
		public int Column;
        public bool Exit = false;
        public int Property;
        
		private static long Seed = 	DateTime.Now.Ticks;
		static public Random TheRandom = new Random((int)Cell.Seed);
		
		
		public Cell()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public bool HasAllWalls()
		{
			for (int i = 0; i < 4; i++)
			{
				 if (Walls[i] == 0)
					 return false;
			}

			return true;
		}

		public void KnockDownWall(Cell theCell)
		{
			// ищем смежную стену
			int theWallToKnockDown = FindAdjacentWall(theCell);
			Walls[theWallToKnockDown] = 0;
			int oppositeWall = (theWallToKnockDown + 2) % 4;
			theCell.Walls[oppositeWall] = 0;
		}

		public int FindAdjacentWall(Cell theCell)
		{
			if (theCell.Row == Row) 
			{
				if (theCell.Column < Column)
					return 0;
				else
					return 2;
			}
			else // столбцы одни и те же
			{
				if (theCell.Row < Row)
					return 1;
				else
					return 3;
			}
		}

        public void Draw(Graphics g)
        {
            if (Exit)
            {
                g.FillEllipse(Brushes.Red, Row * kCellSize + kPadding, Column * kCellSize + kPadding, kCellSize, kCellSize);
                if (Row == 0) Walls[1] = 0; else 
                    if (Column == 0) Walls[0] = 0; else
                        if (Row == Maze.kDimension - 1) Walls[3] = 0; else
                            if (Column == Maze.kDimension - 1) Walls[2] = 0; 
            }
            if (Walls[0] == 1)
            {
                g.DrawLine(Pens.Blue, Row * kCellSize + kPadding, Column * kCellSize + kPadding, (Row + 1) * kCellSize + kPadding, Column * kCellSize + +kPadding);
            }
            if (Walls[1] == 1)
            {
                g.DrawLine(Pens.Blue, Row * kCellSize + kPadding, Column * kCellSize + kPadding, Row * kCellSize + kPadding, (Column + 1) * kCellSize + kPadding);
            }
            if (Walls[2] == 1)
            {
                g.DrawLine(Pens.Blue, Row * kCellSize + kPadding, (Column + 1) * kCellSize + kPadding, (Row + 1) * kCellSize + kPadding, (Column + 1) * kCellSize + kPadding);
            }
            if (Walls[3] == 1)
            {
                g.DrawLine(Pens.Blue, (Row + 1) * kCellSize + kPadding, Column * kCellSize + kPadding, (Row + 1) * kCellSize + kPadding, (Column + 1) * kCellSize + kPadding);
            }
        }
	}
}
