using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Selector : Node                                    // ONLY IF A CHILD HAS SUCCEEDED OR IS RUNNING ( OR gate)
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()                        
        {

            foreach (Node node in children)                         
            {
                switch(node.Evaluate())
                {
                    case NodeState.FAILURE:                         
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;              // IF CHILD SUCCEEDS THEN RETURN SUCCESS
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}
