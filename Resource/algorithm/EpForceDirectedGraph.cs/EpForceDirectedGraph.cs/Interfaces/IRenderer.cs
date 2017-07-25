/*! 

@section DESCRIPTION

An Interface for the Renderer.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph.cs
{

    public interface IRenderer
    {

        void Clear();
        void Draw(float iTimeStep);
    }
}
