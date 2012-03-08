using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadKatanaBrothers
{
    public interface IProperty
    {
    }

    public class Property<T> : IProperty
    {
        T Value
        {
            get;
            set;
        }

        public Property(T value)
        {
            Value = value;
        }
    }
}
