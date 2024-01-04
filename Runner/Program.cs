// See https://aka.ms/new-console-template for more information
Console.WriteLine("Minesweeper game!");

int totalMines = 50;
int sceneWidth = 20;
int sceneHeight = 20;

Minesweeper.Core.Scene scene = new(sceneWidth, sceneHeight, totalMines);


for (int i = 0; i < scene.Width; i++)
{
    for (int j = 0; j < scene.Height; j++)
    {
        if (scene.HasMine(i, j))
        {
            totalMines--;
            continue;
        }

        int mineCount = 0;
        for (int p = i == 0 ? 0 : i - 1;
            p <= (i == scene.Width - 1 ? i : i + 1);
            p++)
        {
            for (int q = j == 0 ? 0 : j - 1;
                q <= (j == scene.Height - 1 ? j : j + 1);
                q++)
            {
                if (scene.HasMine(p, q))
                {
                    mineCount++;
                }
            }
        }

        if (scene.Area[i, j] != mineCount)
        {
            throw new ArgumentException($"incorrect mine marker in [{i},{j}]! expected:{mineCount}, actual:{scene.Area[i, j]}");
        }
    }
}

if (totalMines > 0)
    throw new ArgumentException($"{totalMines} mines are not included!");

Console.Write(scene.ToString());