using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace RadKatanaBrothers
{
    class GameplayManager : Manager
    {
        List<GameplayRepresentation> representations;
        public GameplayManager()
        {
            representations = new List<GameplayRepresentation>();
        }

        public override void AddRepresentation(Representation rep)
        {
            representations.Add(rep as GameplayRepresentation);
        }

        public override void Run(float elapsedMilliseconds)
        {
            foreach (var reps in representations)
                reps.Update();
        }
    }
}
