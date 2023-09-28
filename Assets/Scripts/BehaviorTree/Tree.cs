using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    
    public abstract class MyTree : MonoBehaviour
    {
        private Node root = null;

        protected void Start()
        {
            root = SetupTree();     //Sets up behavior tree
        }

        private void Update()
        {
            if (root != null)
                root.Evaluate();    // If it has a tree it will be evaluated continuosly 
        }

        protected abstract Node SetupTree();    // responsible for creating and configuring the nodes, should return the root node
    }
}
