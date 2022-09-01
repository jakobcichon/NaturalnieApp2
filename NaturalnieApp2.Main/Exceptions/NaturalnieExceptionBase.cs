using NaturalnieApp2.SharedInterfaces.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Main.Exceptions
{
    public class NaturalnieExceptionBase : Exception
    {
        public static ILogger? Logger;

        public NaturalnieExceptionBase()
        {
            Logger?.Exception(Message);
        }

        public NaturalnieExceptionBase(string? message)
        {
            if (message != null)
            {
                Logger?.Exception(message);
            }

        }
    }
}
