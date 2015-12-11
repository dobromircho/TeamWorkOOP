using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrqbvaDaSiIzberemIme.Exceptions
{
    public class ObjectOutOfRangeException : Exception
    {
        public ObjectOutOfRangeException(string message)
            : base(message)
        { 
        }
    }
}
