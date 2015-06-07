using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugRacer
{
    public class HasNoEnuoghMoneyException:ApplicationException
    {
        public HasNoEnuoghMoneyException() 
        { }

        public HasNoEnuoghMoneyException(string message)
        :base(message){ }

        public HasNoEnuoghMoneyException(string message,Exception ex)
            : base(message,ex) { }
    }
}
