using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree      // namespace promotes code organization, reduces naming conflicts, and enhances the readability and maintainability of your codebase.
{
    public enum NodeState   //basic states for a nodestate
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class Node 
    {
        protected NodeState state; // protected - classes that derive from this node class can access and modify this value 

        public Node parent;
        protected List<Node> children = new List<Node>(); // creates a link in both directions, allows flow between children to parents

        private Dictionary<string, object> dataContext = new Dictionary<string, object>(); //data storage (mapping of named variables(any type))

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
                _Attach(child);
        }

        private void _Attach(Node node) //creates the attachment between a node and its new child
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;   //virtual allows each derived node class to implement its own evaluation, unique roles in behavior trees

        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        public object GetData(string key)                           //In order to retrieve data, it must be done recursively to go through every node
        {
            object value = null;
            if(dataContext.TryGetValue(key, out value))
                return value;
            
            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)                           //In order to delete data, it must be done recursively to go through every node
        {                                                             //If found it deletes data, if not found it ignores the request
            if(dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }
            
            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }
}

