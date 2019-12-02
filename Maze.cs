using System;
using System.Collections;
using System.Drawing;
using System.IO;

/*
DFS method summary: create a CellStack (LIFO) to hold a list of cell locations  
set TotalCells = number of cells in grid  
choose a cell at random and call it CurrentCell  
set VisitedCells = 1  
   
while VisitedCells < TotalCells  
find all neighbors of CurrentCell with all walls intact   
if one or more found  
choose one at random  
knock down the wall between it and CurrentCell  
push CurrentCell location on the CellStack  
make the new cell CurrentCell  
add 1 to VisitedCells 
else  
pop the most recent cell entry off the CellStack  
make it CurrentCell 
endIf 
endWhile  
*/

namespace Labyrinth
{
	/// <summary>
	/// Summary description for Maze.
	/// </summary>
	public class Maze
	{
		public static int kDimension = 35;
		public Cell[, ] Cells = null;
        public int vx, vy, sx, sy;
		Stack CellStack = new Stack();
		int VisitedCells = 1;
		int TotalCells = kDimension * kDimension;
		Cell CurrentCell = null;
        public static bool EnterMarker = false;

		public Maze()
		{
			//
			// TODO: Add constructor logic here
			//
			Initialize();
		}

		private ArrayList GetNeighborsWithWalls(Cell aCell)
		{
			ArrayList Neighbors = new ArrayList();
			for (int countRow = -1; countRow <= 1; countRow++)
				for (int countCol = -1; countCol <= 1; countCol++)
				{
					if ( (aCell.Row + countRow < kDimension) &&  
						 (aCell.Column+countCol < kDimension) &&
						 (aCell.Row+countRow >= 0) &&
						 (aCell.Column+countCol >= 0) &&
						 ((countCol == 0) || (countRow == 0)) &&
						 (countRow != countCol)
						)
					{
						if (Cells[aCell.Row+countRow, aCell.Column+countCol].HasAllWalls())
						{
							Neighbors.Add( Cells[aCell.Row+countRow, aCell.Column+countCol]);
						}
					}
				}
			return Neighbors;
		}

		public void Initialize()
		{
			Cells = new Cell[kDimension, kDimension];
			TotalCells = kDimension * kDimension;
			for (int i = 0; i < kDimension; i++)
				for (int j = 0; j < kDimension; j++)
				{
					Cells[i, j] =  new Cell();
					Cells[i, j].Row = i;
					Cells[i, j].Column = j;
                    Cells[i, j].Property = 0;
				}

            // get random entrance position
            Random r = new Random();
            int exitCell = r.Next(0, kDimension);
            if (r.Next(0, 2) == 1)
            {
                if (r.Next(0, 2) == 1)
                {
                    vx = 0;
                    vy = exitCell;
                    Cells[0, exitCell].Exit = true;
                }
                else 
                {
                    vx = kDimension - 1;
                    vy = exitCell;
                    Cells[kDimension - 1, exitCell].Exit = true;
                } 
            }
            else
            {
                if (r.Next(0, 2) == 1)
                {
                    Cells[exitCell, 0].Exit = true;
                    vx = exitCell;
                    vy = 0;
                }
                else
                {
                    vx = exitCell;
                    vy = kDimension - 1;
                    Cells[exitCell, kDimension - 1].Exit = true;
                } 
            }

			CurrentCell = Cells[0,0];
			VisitedCells = 1;
			CellStack.Clear();
		}

		public void  Generate()
		{
			while (VisitedCells < TotalCells)
			{
				// get all neighbors cells with untouched walls
				ArrayList AdjacentCells = GetNeighborsWithWalls(CurrentCell);
				// check if cells exists
				if (AdjacentCells.Count > 0)
				{
					// yes, choose one of those and remove the wall between current and chosen
					int randomCell = Cell.TheRandom.Next(0, AdjacentCells.Count);
					Cell theCell = ((Cell)AdjacentCells[randomCell]);
					CurrentCell.KnockDownWall(theCell);
					CellStack.Push(CurrentCell); // current cell to stack
					CurrentCell = theCell; // do new cell as current
					VisitedCells++;
				}
				else
				{
					// if cell with untouched walls does not exist, need to remove current cell from stack and make cell from stack as current
					CurrentCell = (Cell)CellStack.Pop();
				}
			}
		}

         public void  WaveTracingSolve(Graphics g) 
         {
             while (true)
             {
                 for (int i = 0; i < kDimension; i++)
                     for (int j = 0; j < kDimension; j++)
                     {
                         if (Cells[i, j].Property == 0) continue;
                         else
                         {
                             if ((Cells[i, j].Walls[0] == 0) && ((j - 1) >= 0) && (Cells[i, j - 1].Property == 0))
                             {
                                 Cells[i, j - 1].Property = Cells[i, j].Property + 1;
                             }

                             if ((Cells[i, j].Walls[1] == 0) && ((i - 1) >= 0) && (Cells[i - 1, j].Property == 0))
                             {
                                 Cells[i - 1, j].Property = Cells[i, j].Property + 1;
                             }

                             if ((Cells[i, j].Walls[2] == 0) && ((j + 1) < kDimension) && (Cells[i, j + 1].Property == 0))
                             {
                                 Cells[i, j + 1].Property = Cells[i, j].Property + 1;
                             }

                             if ((Cells[i, j].Walls[3] == 0) && ((i + 1) < kDimension) && (Cells[i + 1, j].Property == 0))
                             {
                                 Cells[i + 1, j].Property = Cells[i, j].Property + 1;
                             }
                         }
                     }
                 if (Cells[vx, vy].Property != 0) break;
             }

             int m = Cells[vx, vy].Property;
             int xx = vx;
             int yy = vy;
             while (true)
             {
                 g.FillRectangle(Brushes.Pink, new Rectangle(xx * Cell.kCellSize + Cell.kPadding, yy * Cell.kCellSize + Cell.kPadding, Cell.kCellSize, Cell.kCellSize));
                 if ((Cells[xx, yy].Walls[0] == 0) && ((yy - 1) >= 0) && (Cells[xx, yy - 1].Property == (m - 1)))
                 {
                     g.FillRectangle(Brushes.Pink, new Rectangle(xx * Cell.kCellSize + Cell.kPadding, (yy - 1) * Cell.kCellSize + Cell.kPadding, Cell.kCellSize, Cell.kCellSize));
                     yy = yy - 1;
                     m--;
                 }
                 if ((Cells[xx, yy].Walls[1] == 0) && ((xx - 1) >= 0) && (Cells[xx - 1, yy].Property == (m - 1)))
                 {
                     g.FillRectangle(Brushes.Pink, new Rectangle((xx - 1) * Cell.kCellSize + Cell.kPadding, yy * Cell.kCellSize + Cell.kPadding, Cell.kCellSize, Cell.kCellSize));
                     xx = xx - 1;
                     m--;
                 }
                 if ((Cells[xx, yy].Walls[2] == 0) && ((yy + 1) < kDimension) && (Cells[xx, yy + 1].Property == (m - 1)))
                 {
                     g.FillRectangle(Brushes.Pink, new Rectangle(xx * Cell.kCellSize + Cell.kPadding, (yy + 1) * Cell.kCellSize + Cell.kPadding, Cell.kCellSize, Cell.kCellSize));
                     yy = yy + 1;
                     m--;
                 }
                 if ((Cells[xx, yy].Walls[3] == 0) && ((xx + 1) < kDimension) && (Cells[xx + 1, yy].Property == (m - 1)))
                 {
                     g.FillRectangle(Brushes.Pink, new Rectangle((xx + 1) * Cell.kCellSize + Cell.kPadding, yy * Cell.kCellSize + Cell.kPadding, Cell.kCellSize, Cell.kCellSize));
                     xx = xx + 1;
                     m--;
                 }
                 if (m == 1) break;
             }
             g.FillEllipse(Brushes.Green, sx * Cell.kCellSize + Cell.kPadding, sy * Cell.kCellSize + Cell.kPadding, Cell.kCellSize, Cell.kCellSize);
             Draw(g);
         }

		public void Draw(Graphics g)
		{
			for (int i = 0; i < kDimension; i++)
				for (int j = 0; j < kDimension; j++)
				{
					Cells[i,j].Draw(g);
				}
		}

        public void SetStartPoint(int x, int y, Graphics g)
        {
            sx = (x - Cell.kPadding) / Cell.kCellSize;
            sy = (y - Cell.kPadding) / Cell.kCellSize;
            if ((sx > kDimension - 1)) sx = kDimension - 1;
            if ((sy > kDimension - 1)) sy = kDimension - 1;
            Cells[sx, sy].Property = 1;
            g.FillEllipse(Brushes.Green, sx * Cell.kCellSize + Cell.kPadding, sy * Cell.kCellSize + Cell.kPadding, Cell.kCellSize, Cell.kCellSize);
            EnterMarker = true;
        }

        public void ClearStartPoint()
        {
            Cells[sx, sy].Property = 0;
            EnterMarker = false;
            sx = 0;
            sy = 0;
            for (int i = 0; i < kDimension; i++)
                for (int j = 0; j < kDimension; j++)
                    Cells[i, j].Property = 0;
        }  

        public void SaveMaze(String FileName) 
        {
            // create WriteFile stream to write bytes to a file
            BinaryWriter WriteFile = new BinaryWriter(
                                File.Open(FileName + ".maze", FileMode.Create));
            try
            {
                WriteFile.Write(kDimension);
                for (int i = 0; i < kDimension; i++)
                    for (int j = 0; j < kDimension; j++)
                        for (int k = 0; k < 4; k++)
                            WriteFile.Write(Cells[i, j].Walls[k]);
                WriteFile.Write(vx);
                WriteFile.Write(vy);
            }
            finally { WriteFile.Close(); }
        }

        public void LoadMaze(String FileName)
        {
            // create ReadFile stream
            BinaryReader ReadFile = new BinaryReader(File.
                                          OpenRead(FileName));
            try
            {
                kDimension = ReadFile.ReadInt32();;
                Cells = new Cell[kDimension, kDimension];
                for (int i = 0; i < kDimension; i++)
                    for (int j = 0; j < kDimension; j++)
                    {
                        Cells[i, j] = new Cell();
                        Cells[i, j].Row = i;
                        Cells[i, j].Column = j;
                        Cells[i, j].Property = 0;
                        for (int k = 0; k < 4; k++)
                            Cells[i, j].Walls[k] = ReadFile.ReadInt32();
                     }
                EnterMarker = false;
                vx = ReadFile.ReadInt32();
                vy = ReadFile.ReadInt32(); 
                Cells[vx, vy].Exit = true;
             }
             finally { ReadFile.Close(); }
        }
    }	
}