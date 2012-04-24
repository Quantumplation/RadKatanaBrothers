using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadKatanaBrothers
{
    public class Representation
    {
        public virtual void Initialize() { }
        public virtual void Create() { }
        public virtual void Terminate() { }

        public delegate void Created(Representation rep);
        public delegate void Terminated(Representation rep);

        public Entity Parent
        {
            get;
            set;
        }
    }
}
