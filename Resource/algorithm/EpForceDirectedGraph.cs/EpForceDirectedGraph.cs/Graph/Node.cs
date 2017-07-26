/*! 

@section DESCRIPTION

An Interface for the Node Class.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpForceDirectedGraph.cs
{
    public class Node
    {
        public Node(string iId, NodeData iData = null)
        {
            ID = iId;
            Data = new NodeData();
            if (iData != null)
            {
                Data.initialPostion = iData.initialPostion;
                Data.label = iData.label;
                Data.mass = iData.mass;
            }
            Pinned = false;
        }

        public string ID
        {
            get;
            private set;
        }
        public NodeData Data
        {
            get;
            private set;
        }


        public bool Pinned
        {
            get;
            set;
        }
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Node p = obj as Node;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (ID == p.ID);
        }

        public bool Equals(Node p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (ID == p.ID);
        }

        public static bool operator ==(Node a, Node b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.ID == b.ID;
        }

        public static bool operator !=(Node a, Node b)
        {
            return !(a == b);
        }
    }
}
