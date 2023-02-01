using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Validation
{
    public class NotSucceededOperationException : Exception
    {
        public NotSucceededOperationException(string message) : base(message)
        {

        }
    }
}
