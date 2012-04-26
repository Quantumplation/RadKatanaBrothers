using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;

namespace RadKatanaBrothers
{
    public static class World
    {
        static Dictionary<string, Entity> entities;
        static Dictionary<string, Manager> managers;
        static List<string> entitiesToRemove;
        public static int Score
        {
            get;
            set;
        }

        public static void Initialize()
        {
            entities = new Dictionary<string, Entity>();
            managers = new Dictionary<string, Manager>();
            entitiesToRemove = new List<string>();
            RenderManager render = new RenderManager();
            AddManager<RenderManager>(id: "graphics");
            AddManager<PhysicsManager>(id: "physics");
            AddManager<GameplayManager>(id: "gameplay");
            Factory.RegisterManager<RenderManager>(GetManager<RenderManager>(id: "graphics"), typeof(CircleRepresentation), typeof(MeshRepresentation), typeof(SpriteRepresentation), typeof(TextRepresentation));
            Factory.RegisterManager<PhysicsManager>(GetManager<PhysicsManager>(id: "physics"), typeof(PhysicsRepresentation));
            Factory.RegisterManager<GameplayManager>(GetManager<GameplayManager>(id: "gameplay"), typeof(GameplayRepresentation));
            Factory.RegisterCallback<Entity>((settings) => new Entity(settings));
            Factory.RegisterCallback<Player>((settings) => new Player(settings));
            Factory.RegisterCallback<StaticSolid>((settings) => new StaticSolid(settings));
            Factory.RegisterCallback<SpriteRepresentation>((settings) => new SpriteRepresentation(settings));
            Factory.RegisterCallback<MeshRepresentation>((settings) => new MeshRepresentation(settings));
            Factory.RegisterCallback<CircleRepresentation>((settings) => new CircleRepresentation(settings));
            Factory.RegisterCallback<TextRepresentation>((settings) => new TextRepresentation(settings));
            Factory.RegisterCallback<PhysicsRepresentation>((settings) => new PhysicsRepresentation());
            Factory.RegisterCallback<GameplayRepresentation>((settings) => new GameplayRepresentation());
        }

        public static void LoadMaze(int seed)
        {
            ClearLevel();
            //AddEntity<StaticSolid>("floor", new GameParams()
            //{
            //    {"collisionMaskVisible", true},
            //    {"polygonVertices", new List<Vector2>()
            //        {
            //            new Vector2(0, 688),
            //            new Vector2(720, 688),
            //            new Vector2(720, 720),
            //            new Vector2(0, 720)
            //        }
            //    },
            //    {"color", Color.PaleTurquoise},
            //    {"deadly", true}
            //});
            Maze maze = new Maze();
            List<GameParams> rectangles = maze.CreateMaze(seed);
            for (int i = 0; i < rectangles.Count; ++i)
                AddEntity<StaticSolid>("maze" + i, rectangles[i]);
            AddEntity<Player>("player", new GameParams()
            {
                {"position", new Vector2(72, 72)}
            });
            Random rand = new Random();
            List<Vector2> usedPoints = new List<Vector2>();
            for (int i = 0; i < 50; ++i)
            {
                int x, y;
                do
                {
                    x = (2 * rand.Next(Maze.GRID_DIMENSIONS)) * Maze.CELL_SIZE;
                    y = (2 * rand.Next(Maze.GRID_DIMENSIONS)) * Maze.CELL_SIZE;
                } while (usedPoints.Contains(new Vector2(x, y)));
                usedPoints.Add(new Vector2(x,y));
                AddEntity<StaticSolid>("obstacle" + i, new GameParams()
                {
                    {"deadly", true},
                    {"collisionMaskVisible", true},
                    {"color", Color.Purple},
                    {"polygonVertices", new List<Vector2>()
                    {
                        new Vector2(0, 0),
                        new Vector2(Maze.CELL_SIZE, 0),
                        new Vector2(Maze.CELL_SIZE, Maze.CELL_SIZE),
                        new Vector2(0, Maze.CELL_SIZE)
                    }}
                });
                GetEntity<StaticSolid>("obstacle" + i).AddProperty<Vector2>("position", new Vector2(x, y));

            }
            AddEntity<StaticSolid>("goal", new GameParams()
            {
                {"collisionMaskVisible", true},
                {"polygonVertices", new List<Vector2>()
                {
                    new Vector2((Maze.GRID_DIMENSIONS - 1) * Maze.CELL_SIZE),
                    new Vector2(Maze.GRID_DIMENSIONS * Maze.CELL_SIZE, (Maze.GRID_DIMENSIONS - 1) * Maze.CELL_SIZE),
                    new Vector2(Maze.GRID_DIMENSIONS * Maze.CELL_SIZE),
                    new Vector2((Maze.GRID_DIMENSIONS - 1) * Maze.CELL_SIZE, Maze.GRID_DIMENSIONS * Maze.CELL_SIZE)
                }},
                //{"polygonVertices", new List<Vector2>()
                //{
                //    new Vector2((Maze.GRID_DIMENSIONS - 2) * Maze.CELL_SIZE),
                //    new Vector2(Maze.GRID_DIMENSIONS * Maze.CELL_SIZE, (Maze.GRID_DIMENSIONS - 2) * Maze.CELL_SIZE),
                //    new Vector2(Maze.GRID_DIMENSIONS * Maze.CELL_SIZE),
                //    new Vector2((Maze.GRID_DIMENSIONS - 2) * Maze.CELL_SIZE, Maze.GRID_DIMENSIONS * Maze.CELL_SIZE)
                //}},
                //{"polygonVertices", new List<Vector2>()
                //{
                //    new Vector2((Maze.GRID_DIMENSIONS - 2) * Maze.CELL_SIZE, (Maze.GRID_DIMENSIONS - 2) * Maze.CELL_SIZE),
                //    new Vector2((Maze.GRID_DIMENSIONS - 1) * Maze.CELL_SIZE, (Maze.GRID_DIMENSIONS - 2) * Maze.CELL_SIZE),
                //    new Vector2((Maze.GRID_DIMENSIONS - 1) * Maze.CELL_SIZE, (Maze.GRID_DIMENSIONS - 1) * Maze.CELL_SIZE),
                //    new Vector2((Maze.GRID_DIMENSIONS - 2) * Maze.CELL_SIZE, (Maze.GRID_DIMENSIONS - 1) * Maze.CELL_SIZE)
                //}},
                {"color", Color.Red},
                {"deadly", true}
            });
            GetEntity<StaticSolid>("goal").AddProperty<Vector2>("position", Vector2.Zero).Value -= new Vector2(Maze.CELL_SIZE);

            foreach (var entity in entities.Values)
                entity.Initialize();
        }

        static void ClearLevel()
        {
            entities.Clear();
            foreach (var manager in managers.Values)
                manager.ClearRepresentations();
        }

        //public void LoadMap(string filename)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    try
        //    {
        //        doc.Load(filename + ".xml");
        //        XmlNodeList loadEntities = doc.GetElementsByTagName("Entity");
        //        foreach (XmlNode entity in loadEntities)
        //        {
        //            Entity newEntity = Factory.Produce(entity.Attributes["class"].Value) as Entity;
        //            XmlNode node = entity.FirstChild;
        //            do
        //            {
        //                if (node.Name == "Representation")
        //                {
        //                    // TODO: Pi
        //                    GameParams Settings = new GameParams();
        //                    foreach (XmlNode child in node.ChildNodes)
        //                        Settings.Add(child.Name, Factory.Produce(child.Attributes["type"].Value, child.Attributes["value"].Value));
        //                    newEntity.AddRepresentation(node.Attributes["type"].Value, node.Attributes["name"].Value, Settings);
        //                }
        //                node = node.NextSibling;
        //            }
        //            while (node != entity.LastChild);
        //            //for (XmlNode node = entity.FirstChild; node != entity.LastChild; node = node.NextSibling)
        //                //Add the representations
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}

        public static void AddEntity<T>(string id, GameParams settings = null) where T : Entity
        {
            T entity = Factory.Produce<T>(settings);
            entity.ID = id;
            entities.Add(id, entity);
        }
        public static void AddManager<T>(string id) where T : Manager, new()
        {
            managers.Add(id, new T());
        }

        public static T GetEntity<T>(string id) where T : Entity
        {
            return (entities[id] as T);
        }

        public static T GetManager<T>(string id) where T : Manager
        {
            return (managers[id] as T);
        }

        public static void RunAllManagers(float elapsedMilliseconds)
        {
            foreach (var manager in managers.Values)
                manager.Run(elapsedMilliseconds);
            for (int i = 0; i < entitiesToRemove.Count; ++i)
            {
                entities[entitiesToRemove[i]].Terminate();
                entities.Remove(entitiesToRemove[i]);
            }
            entitiesToRemove.Clear();
        }

        public static void PrepareToRemoveEntity(string id)
        {
            entitiesToRemove.Add(id);
        }
    }
}
