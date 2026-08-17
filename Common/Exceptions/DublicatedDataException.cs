using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class DublicatedDataException:Exception
    {
        public DublicatedDataException(string message ):base(message)
        {
            
        }
    }
}
