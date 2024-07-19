using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectClassroom
{
    public class ValidNameException:Exception
    {
        public ValidNameException(string message) : base(message)
        {

        }


    }
}
