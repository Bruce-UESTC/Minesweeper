namespace CoreTester
{
    [TestClass]
    public class SceneTester
    {
        [TestMethod]
        public void TestCoherentMineIndicator()
        {
            Minesweeper.Core.Scene scene = new(10, 10, 10);

            for (int i = 0; i < scene.Width; i++)
            {
                for (int j = 0; j < scene.Height; j++)
                {
                    if (scene.HasMine(i, j))
                    {
                        continue;
                    }

                    int mineCount = 0;
                    for (int p = i == 0 ? 0 : i - 1;
                        p < (i == scene.Width - 1 ? i : i + 1);
                        p++)
                    {
                        for (int q = j == 0 ? 0 : j - 1;
                            q < (j == scene.Height - 1 ? j : j + 1);
                            q++)
                        {
                            if (scene.HasMine(p, q))
                            {
                                mineCount++;
                            }
                        }
                    }

                    Assert.AreEqual(scene.Area[i, j], mineCount);
                }
            }
        }
    }
}