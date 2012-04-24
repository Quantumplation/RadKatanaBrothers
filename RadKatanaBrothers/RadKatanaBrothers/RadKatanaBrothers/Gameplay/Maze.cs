using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RadKatanaBrothers
{
    //A procedurally generated maze
    class Maze
    {
        public const int CELL_SIZE = 48; //The size of each cell
        public const int GRID_DIMENSIONS = 15; //The number of cells (must be odd and > 1)
        //Rectangle LOCATION = new Rectangle(0, 0, CELL_SIZE * GRID_DIMENSIONS, CELL_SIZE * GRID_DIMENSIONS);
        bool[,] cellsOccupied; //The locations of the walls

        //Recursively generate a random maze
        public void GenerateMaze(Random rand, int x, int y)
        {
            cellsOccupied[x, y] = false; //Clear the current point
            bool triedLeft = false, triedRight = false;
            bool triedUp = false, triedDown = false;
            //Iterate through the four cardinal directions
            while (!(triedLeft && triedRight && triedUp && triedDown))
            {
                int wayToTryNext = rand.Next(4) - 1;
                bool keepSearching = true;
                do
                {
                    //Choose a direction at random
                    wayToTryNext = (wayToTryNext > 3 ? 0 : wayToTryNext + 1);
                    switch (wayToTryNext)
                    {
                        case 0:
                            if (!triedLeft)
                            {
                                if (x > 2 && cellsOccupied[x - 2, y]) //If a wall exists two cells away in this direction...
                                {
                                    cellsOccupied[x - 1, y] = false; //Clear the cell in that direction,
                                    GenerateMaze(rand, x - 2, y); //and generate the maze starting from there
                                    keepSearching = false;
                                }
                                triedLeft = true;
                            }
                            break;
                        case 1:
                            if (!triedRight)
                            {
                                if (x < GRID_DIMENSIONS - 3 && cellsOccupied[x + 2, y])
                                {
                                    cellsOccupied[x + 1, y] = false;
                                    GenerateMaze(rand, x + 2, y);
                                    keepSearching = false;
                                }
                                triedRight = true;
                            }
                            break;
                        case 2:
                        default:
                            if (!triedUp)
                            {
                                if (y > 2 && cellsOccupied[x, y - 2])
                                {
                                    cellsOccupied[x, y - 1] = false;
                                    GenerateMaze(rand, x, y - 2);
                                    keepSearching = false;
                                }
                                triedUp = true;
                            }
                            break;
                        case 3:
                            if (!triedDown)
                            {
                                if (y < GRID_DIMENSIONS - 3 && cellsOccupied[x, y + 2])
                                {
                                    cellsOccupied[x, y + 1] = false;
                                    GenerateMaze(rand, x, y + 2);
                                    keepSearching = false;
                                }
                                triedDown = true;
                            }
                            break;
                    }
                }
                while (keepSearching && !(triedLeft && triedRight && triedUp && triedDown));
            }
        }

        //Fills the maze with walls
        public void ResetMaze()
        {
            for (int y = 0; y < GRID_DIMENSIONS; ++y)
                for (int x = 0; x < GRID_DIMENSIONS; ++x)
                    cellsOccupied[x, y] = true;
        }

        public Maze()
        {
            cellsOccupied = new bool[GRID_DIMENSIONS, GRID_DIMENSIONS];
            ResetMaze();
        }

        public List<GameParams> CreateMaze()
        {
            Random rand = new Random();
            GenerateMaze(rand, 1, 1);
            List<GameParams> result = new List<GameParams>();
            Vector2 currentLeft;
            Vector2 currentRight;
            for (currentLeft = Vector2.Zero; currentLeft.Y < GRID_DIMENSIONS; currentLeft = new Vector2(0, currentLeft.Y + 1))
            {
                for (currentRight = currentLeft; currentRight.X < GRID_DIMENSIONS; currentRight += Vector2.UnitX)
                {
                    if (!PositionIsOccupied(currentRight))
                    {
                        result.Add(new GameParams()
                        {
                            {"collisionMaskVisible", true},
                            {"polygonVertices", new List<Vector2>()
                                {
                                    currentLeft * CELL_SIZE,
                                    currentRight * CELL_SIZE,
                                    (currentRight + Vector2.UnitY) * CELL_SIZE,
                                    (currentLeft + Vector2.UnitY) * CELL_SIZE
                                }
                            },
                            {"color", Color.Black}
                        });
                        currentLeft = currentRight;
                        while (currentRight.X < GRID_DIMENSIONS && !PositionIsOccupied(currentRight))
                            currentRight += Vector2.UnitX;
                        if (currentRight.X != GRID_DIMENSIONS)
                            currentLeft = currentRight;
                    }
                }
                result.Add(new GameParams()
                {
                    {"collisionMaskVisible", true},
                    {"polygonVertices", new List<Vector2>()
                        {
                            currentLeft * CELL_SIZE,
                            new Vector2(GRID_DIMENSIONS, currentLeft.Y) * CELL_SIZE,
                            new Vector2(GRID_DIMENSIONS, currentLeft.Y + 1) * CELL_SIZE,
                            (currentLeft + Vector2.UnitY) * CELL_SIZE
                        }
                    },
                    {"color", Color.Black}
                });
            }
            return result;
        }

        //public bool Contains(int x, int y)
        //{
        //    return LOCATION.Contains(x, y);
        //}

        //Checks whether there's a wall in a given cell
        public bool PositionIsOccupied(Tuple<int, int> position)
        {
            return cellsOccupied[position.Item1, position.Item2];
        }

        public bool PositionIsOccupied(int x, int y)
        {
            return cellsOccupied[x / CELL_SIZE, y / CELL_SIZE];
        }

        public bool PositionIsOccupied(Vector2 position)
        {
            return PositionIsOccupied(new Tuple<int, int>((int)position.X, (int)position.Y));
        }

        //public void Load(ContentManager Content)
        //{
        //    texture = Content.Load<Texture2D>(@"Sprites/square");
        //}

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    for (int y = 0; y < GRID_DIMENSIONS; ++y)
        //        for (int x = 0; x < GRID_DIMENSIONS; ++x)
        //            spriteBatch.Draw(texture, new Rectangle(x * CELL_SIZE, y * CELL_SIZE, CELL_SIZE, CELL_SIZE), (cellsOccupied[x, y] ? Color.Black : Color.White));
        //}
    }
}
