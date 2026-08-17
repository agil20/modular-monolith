using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions;

public class ConfilictException:Exception
{
    public ConfilictException(string message):base(message)
    {
        
    }
}
