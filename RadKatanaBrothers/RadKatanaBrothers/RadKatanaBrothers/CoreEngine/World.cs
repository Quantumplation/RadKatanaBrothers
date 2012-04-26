using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using Microsoft.Xna.Framework.Input;

namespace RadKatanaBrothers
{
    public static class World
    {
        static Dictionary<string, Entity> entities;
        static Dictionary<string, Manager> managers;
        static List<string> entitiesToRemove;

        static bool running = false;
        public static bool Running
        {
            get { return running; }
            set { lock (lockObj) { running = value; } }
        }

        public static void Initialize()
        {
            lockObj = new Int32();
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
            AddManager<NetworkManager>(id: "network");
            GetManager<NetworkManager>("network").Initialize();
            Factory.RegisterManager<NetworkManager>(GetManager<NetworkManager>("network"), typeof(NetworkRepresentation));
            Factory.RegisterCallback<NetworkRepresentation>((settings) => new NetworkRepresentation(settings));
        }

        public static void LoadMaze(int seed)
        {
            lock (lockObj)
            {
                ClearLevel();
                Maze maze = new Maze();
                List<GameParams> rectangles = maze.CreateMaze(seed);
                for (int i = 0; i < rectangles.Count; ++i)
                    AddEntity<StaticSolid>("maze" + i, rectangles[i]);
                AddEntity<Player>("player1", new GameParams()
            {
                {"position", new Vector2(Maze.CELL_SIZE * 1.5f)},
                {"remote", !NetworkManager.SERVER }
            });

                AddEntity<Player>("player2", new GameParams()
            {
                {"position", new Vector2(Maze.CELL_SIZE * 3.5f, Maze.CELL_SIZE * 1.5f)},
                {"remote", NetworkManager.SERVER}
            });
                Random rand = new Random(seed);
                List<Vector2> usedPoints = new List<Vector2>();
                for (int i = 0; i < 50; ++i)
                {
                    int x, y;
                    do
                    {
                        x = (2 * rand.Next(Maze.GRID_DIMENSIONS)) * Maze.CELL_SIZE;
                        y = (2 * rand.Next(Maze.GRID_DIMENSIONS)) * Maze.CELL_SIZE;
                    } while (usedPoints.Contains(new Vector2(x, y)));
                    usedPoints.Add(new Vector2(x, y));
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
                {"color", Color.Red},
                {"victory", true}
            });
                GetEntity<StaticSolid>("goal").AddProperty<Vector2>("position", Vector2.Zero).Value -= new Vector2(Maze.CELL_SIZE);

                foreach (var entity in entities.Values)
                    entity.Initialize();
            }
        }

        static void ClearLevel()
        {
            foreach (var entity in entities.Keys)
                PrepareToRemoveEntity(entity);
            foreach (var manager in managers.Values)
                manager.ClearRepresentations();
        }

        public static bool hack = false;
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

        static object lockObj;

        public static void RunAllManagers(float elapsedMilliseconds)
        {
            if (!Running)
                return;
            lock (lockObj)
            {
                foreach (var manager in managers.Values)
                    manager.Run(elapsedMilliseconds);
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && !hack)
                {
                    LoadMaze(9034);
                    hack = true;
                }
                for (int i = 0; i < entitiesToRemove.Count; ++i)
                {
                    entities[entitiesToRemove[i]].Terminate();
                    entities.Remove(entitiesToRemove[i]);
                }
                entitiesToRemove.Clear();
            }
        }

        public static void PrepareToRemoveEntity(string id)
        {
            entitiesToRemove.Add(id);
        }
    }
}
