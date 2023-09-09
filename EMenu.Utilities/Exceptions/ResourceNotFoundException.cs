using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMenu.Utilities.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string exceptionMSG) : base(exceptionMSG)
        {
        }

        public ResourceNotFoundException(string exceptionMSG, Exception exceptionCause) : base(exceptionMSG, exceptionCause)
        {
        }
    }
}
