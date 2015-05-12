using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BugRunner
{
    class MoneySubZeroException:Exception
    {
        public MoneySubZeroException()
            : base()
        {
        }

        public MoneySubZeroException(string message)
            : base(message)
        {
        }
    }
}
