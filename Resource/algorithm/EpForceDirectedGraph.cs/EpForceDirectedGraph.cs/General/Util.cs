/*! 

@section DESCRIPTION

An Interface for the Util Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph.cs
{
    public class Util
    {
        private static Random random = new Random();
        public static float Random()
        {
            var result = random.NextDouble();
            return (float)result;
        }
    }

}
