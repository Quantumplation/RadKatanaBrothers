using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadKatanaBrothers
{
    public class Representation
    {
        public virtual void Initialize() { }

        public Entity Parent
        {
            get;
            set;
        }
    }
}
