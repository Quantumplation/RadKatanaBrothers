using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RadKatanaBrothers
{
    public abstract class Manager
    {
        public virtual void AddRepresentation(Representation rep) { }
        public virtual void RemoveRepresentation(Representation rep) { }
        public abstract void ClearRepresentations();
        public abstract void Run(float elapsedMilliseconds);
    }
}
