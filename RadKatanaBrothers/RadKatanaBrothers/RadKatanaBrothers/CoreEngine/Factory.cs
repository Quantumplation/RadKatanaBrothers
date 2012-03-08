using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadKatanaBrothers
{
    public class Factory
    {
        public static void Register<T>()
        {
            throw new NotImplementedException();
        }
        public static T Produce<T>(string id)
        {
            return default(T);
        }
    }
}
