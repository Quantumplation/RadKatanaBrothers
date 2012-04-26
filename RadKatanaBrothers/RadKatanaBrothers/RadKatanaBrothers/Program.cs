using System;

namespace RadKatanaBrothers
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //NetworkManager manager = new NetworkManager();
            //manager.Initialize();
            //Entity parent = new Entity();
            //var prop = parent.AddProperty<int>("score", 0);
            //Factory.RegisterCallback<NetworkRepresentation>(o => new NetworkRepresentation(o));
            //Factory.RegisterManager<NetworkManager>(manager, typeof(NetworkRepresentation));
            //parent.AddRepresentation<NetworkRepresentation>("rep", new GameParams() { { "score", 1 } });
            //manager.AddRepresentation(parent.GetRepresentation<NetworkRepresentation>("rep"));
            //parent.Initialize();
            using (Game1 game = new Game1())
            {
                game.Run();
            }
            //int value = 0;
            //while (true)
            //{
            //    manager.Run(0);
            //    if(value % 10 == 0)
            //        prop.Value = value;
            //    value += 1;
            //    System.Threading.Thread.Sleep(1000);
            //}
        }
    }
#endif
}

