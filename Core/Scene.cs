using System.Text;

namespace Minesweeper.Core
{
    public class Scene
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int MineCount { get; set; }
        public int MinesRemain { get; set; }
        public int[,] Area { get; set; }
        public bool HasMine(int x, int y)
        {
            return Area[x, y] == -1;
        }

        public Scene(int width, int height, int mineCount)
        {
            Width = width;
            Height = height;
            MineCount = mineCount;
            MinesRemain = mineCount;
            Area = new int[width, height];
            AreaInit(mineCount);
        }

        private void AreaInit(int mineCount)
        {
            Area.Initialize();  //fill with 0

            //TODO: random place mines into area
            LayingMines(mineCount);
            MineNumber( );            
        }

        //TODO: random place mines into area
        // -1 means the target has a mine, otherwise it is safe
        // refer to function HasMine(int x, int y)
        private void LayingMines(int mineCount)
        {
            Random rad = new Random();
            int x;
            int y;            

            for (int i = 0; i < mineCount; i++)
            {
                x = rad.Next(Width);
                y = rad.Next(Height);
                while (Area[x, y] == -1)
                {
                    x = rad.Next(Width);
                    y = rad.Next(Height);
                }

                Area[x, y] = -1;               
            }
        }

        //TODO: generate corresponding mine number for each coordinate
        private void MineNumber( )
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {

                    if (HasMine(i, j))
                    {
                        int pStart = ((i - 1) < 0 ? i : i - 1);
                        int pEnd = ((i + 1) >= Width ? i : i + 1);

                        int qStart = ((j - 1) < 0 ? j : j - 1);
                        int qEnd = ((j + 1) >= Height ? j : j + 1);

                        for (int p = pStart; p <= pEnd; p++)
                        {
                            for (int q = qStart; q <= qEnd; q++)
                            {
                                if (HasMine(p, q))
                                {
                                    continue;
                                }

                                Area[p, q] += 1;
                            }
                        }
                    }
                }
            }
        }

        //throw new NotImplementedException();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            AppendLineSplitter(ref sb, Width);
            for (int i = 0; i < Height; i++)
            {
                sb.Append('|');
                for (int j = 0; j < Width; j++)
                {
                    sb.Append(' ');
                    if (HasMine(j, i))
                        sb.Append('*');
                    else if (Area[j, i] != 0)
                        sb.Append(Area[j, i]);
                    else
                    {
                        sb.Append(' ');
                    }
                    sb.Append(' ');
                    sb.Append('|');
                }
                sb.Append('\n');
                AppendLineSplitter(ref sb, Width);
            }

            return sb.ToString();
        }

        private static void AppendLineSplitter(ref StringBuilder sb, int elementCount)
        {
            sb.Append('+');
            for (int i = 0; i < elementCount; i++)
            {
                sb.Append("---+");
            }
            sb.Append('\n');
        }
    }
}
