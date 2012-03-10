using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadKatanaBrothers
{
    public class RenderManager : Manager
    {
        public override void AddRepresentation(Representation rep)
        {
        }

        List<GraphicsRepresentation> representations;
        public RenderManager()
        {
            representations = new List<GraphicsRepresentation>();
        }
        public override void Run()
        {
            foreach (var representation in representations)
            {
                Console.WriteLine(representation.ToString());
            }
        }
    }
}
