/*! 


An Interface for the PhysicsData Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph.cs
{
    public class NodeData : GraphData
    {
        public NodeData():base()
        {
            mass = 1.0f;
            initialPostion = null;
            origID = ""; // for merging the graph
        }
        public float mass
        {
            get;
            set;
        }

        public AbstractVector initialPostion
        {
            get;
            set;
        }
        public string origID
        {
            get;
            set;
        }

    }
    public class EdgeData:GraphData
    {
        public EdgeData():base()
        {
            length = 1.0f;
        }
            public float length
        {
            get;
            set;
        }
    }
    public class GraphData
    {
        public GraphData()
        {
            label = "";
        }


        public string label
        {
            get;
            set;
        }


    }
}
