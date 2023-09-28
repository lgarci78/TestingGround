using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Sequence : Node                                    // ONLY IF ALL CHILD NODES SUCCEED WILL SEQUENCE GO THROUGH (think of an AND gate)
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()                        //OVERRIDE EVALUATE METHOD 
        {
            bool anyChildIsRunning = false;

            foreach (Node node in children)                         // ITERATE THROUGH THE CHILDREN AND CHECKS STATES AFTER EVALUATION
            {
                switch(node.Evaluate())
                {
                    case NodeState.FAILURE:                         //IF ANY CHILD FAILS, WE STOP AND RETURN THE FAIL STATE
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;  // CHECK IF ANY ARE RUNNING OR IF ALL HAVE SUCCEEDED 
            return state;
        }
    }
}

