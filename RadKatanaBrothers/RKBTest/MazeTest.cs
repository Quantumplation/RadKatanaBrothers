using RadKatanaBrothers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RKBTest
{
    
    
    /// <summary>
    ///This is a test class for MazeTest and is intended
    ///to contain all MazeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MazeTest
    {
        /// <summary>
        ///A test for CreateMaze
        ///</summary>
        [TestMethod()]
        public void CreateMazeTest()
        {
            Maze target = new Maze(); // TODO: Initialize to an appropriate value
            int seed = 9001; // TODO: Initialize to an appropriate value
            List<GameParams> expected = target.CreateMaze(seed); // TODO: Initialize to an appropriate value
            List<GameParams> actual;
            actual = target.CreateMaze(seed);
            for (int i = 0; i < expected.Count; ++i)
            {
                List<Vector2> exp = (List<Vector2>)(expected[i]["polygonVertices"]);
                List<Vector2> act = (List<Vector2>)(actual[i]["polygonVertices"]);
                for (int j = 0; j < exp.Count; ++j)
                    Assert.AreEqual(exp[j], act[j]);
            }
        }

        /// <summary>
        ///A test for GenerateMaze
        ///</summary>
        [TestMethod()]
        public void GenerateMazeTest()
        {
            Maze target = new Maze(); // TODO: Initialize to an appropriate value
            Random rand = new Random(1337); // TODO: Initialize to an appropriate value
            int x = 1; // TODO: Initialize to an appropriate value
            int y = 1; // TODO: Initialize to an appropriate value
            target.GenerateMaze(rand, x, y);
        }

        /// <summary>
        ///A test for ResetMaze
        ///</summary>
        [TestMethod()]
        public void ResetMazeTest()
        {
            Maze target = new Maze(); // TODO: Initialize to an appropriate value
            target.ResetMaze();
            for (int y = 0; y < Maze.GRID_DIMENSIONS; ++y)
                for (int x = 0; x < Maze.GRID_DIMENSIONS; ++x)
                    Assert.IsTrue(target.PositionIsOccupied(new Tuple<int, int>(x, y)));
        }
    }
}
