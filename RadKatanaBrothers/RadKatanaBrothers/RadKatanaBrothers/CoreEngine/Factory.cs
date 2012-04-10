using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadKatanaBrothers
{
    public static class Factory
    {
        static EventContainer managerCallbacks;
        static EventContainer productionCallbacks;

        static Factory()
        {
            managerCallbacks = new EventContainer();
            productionCallbacks = new EventContainer();
        }

        public static T Produce<T>(GameParams settings = null)
        {
            T produced = default(T);
            if (productionCallbacks.HasEvent(typeof(T).Name))
            {
                produced = productionCallbacks.GetEvent<Func<GameParams, T>>(typeof(T).Name)(settings);
                if (produced is Representation && managerCallbacks.HasEvent(typeof(T).Name))
                    managerCallbacks.GetEvent<Action<Representation>>(typeof(T).Name)(produced as Representation);
            }
            return produced;
        }

        public static Object Produce(string type, object param)
        {
            return null;
        }

        //Pi fix this
        public static Object Produce(string type, GameParams settings = null)
        {
            switch (type)
            {
                #region Entities
                case "Entity":
                    return Produce<Entity>(settings);
                case "Player":
                    return Produce<Player>(settings);
                #endregion

                #region Representations
                case "Representation":
                    return Produce<Representation>(settings);
                case "GraphicsRepresentation":
                    return Produce<GraphicsRepresentation>(settings);
                case "SpriteRepresentation":
                    return Produce<SpriteRepresentation>(settings);
                case "MeshRepresentation":
                    return Produce<MeshRepresentation>(settings);
                #endregion

                default:
                    throw new NotImplementedException("Factory.Produce does not recognize " + type);
            }
        }

        public static void RegisterManager<T>(T renderManager, params Type[] types) where T : Manager
        {
            foreach(Type t in types)
            {
                managerCallbacks.AddEvent<Action<Representation>>(t.Name, renderManager.AddRepresentation);
            }
        }
        public static void RegisterCallback<T>(Func<GameParams, T> callback)
        {
            productionCallbacks.AddEvent<Func<GameParams, T>>(typeof(T).Name, callback);
        }
    }
}
