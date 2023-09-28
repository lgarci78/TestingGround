using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class RangeCheckInRange : Node
{
    private static int enemyLayerMask = 1 << 6;

    private Transform _transform;
    //private Animator animator;

    public RangeCheckInRange(Transform transform)
    {
        _transform = transform;
        //animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                _transform.position, RangedBT.fovRange, enemyLayerMask);
            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                Debug.Log(colliders[0].name);
                //animator.SetBool("Walking", true);
                state = NodeState.SUCCESS;
                return state;
            }
            //Debug.Log("No target");
            state = NodeState.FAILURE;
            return state;
        }
        //Debug.Log("I have a target");
        state = NodeState.SUCCESS;
        return state;
    }
}
