using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MindGame
{
    class FarStepException:Exception
    {
        public FarStepException() : base() { }

        public FarStepException(string message) : base(message) { }
    }
}
