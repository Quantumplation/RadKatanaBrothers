using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RadKatanaBrothers
{
    public abstract class Manager
    {
        public abstract void AddRepresentation(Representation rep);
        public abstract void Run(float elapsedMilliseconds);
    }
}
