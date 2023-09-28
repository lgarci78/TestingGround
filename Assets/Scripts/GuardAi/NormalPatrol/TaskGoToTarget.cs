using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskGoToTarget : Node
{
    private Transform _transform;
    private NavMeshAgent _agent;
    private Quaternion _initialRotation;

    public TaskGoToTarget(Transform transform, NavMeshAgent agent)
    {
        _transform = transform;
        _agent = agent;
        _initialRotation = _transform.rotation;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (target != null)
        {

            float distanceToTarget = Vector3.Distance(_transform.position, target.position);

            if (distanceToTarget > 20f)
            {
                parent.parent.SetData("target", null);
                _agent.ResetPath();
                state = NodeState.FAILURE;
                return state;
            }

           if (!_agent.hasPath || _agent.destination != target.position)
            {
                _agent.SetDestination(target.position);
            }
        }

                // Lock rotation
        _transform.rotation = _initialRotation;

        state = NodeState.RUNNING;
        return state;
    }
}
