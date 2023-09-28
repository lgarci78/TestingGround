using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

public class Patrol : Node
{
    private Transform _transform;
    private Transform[] _waypoints;
    private NavMeshAgent _navMeshAgent;

    //private Animator animator;

    private int currentWaypointIndex = 0;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public Patrol(Transform transform, Transform[] waypoints, NavMeshAgent navMeshAgent)           // moves towards custom points assigned in the inspector
    {
        _transform = transform;
        //animator = transform.GetComponent<Animator>();
        _waypoints = waypoints;
        _navMeshAgent = navMeshAgent;
    }

    public override NodeState Evaluate()
    {
        if (_navMeshAgent.isStopped)
        {
            Debug.Log("stopped");
            _navMeshAgent.isStopped = false;
            waiting = false;
        }
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter < waitTime)
                waiting = false;
                //animator.SetBool("Walking", true);
        }
        else{
            Transform wp = _waypoints[currentWaypointIndex];
            //Debug.Log(wp);
            //Debug.Log(Vector2.Distance(_transform.position, wp.position));
            if (Vector2.Distance(_transform.position, wp.position) < 0.01f)
            {
                
                //Debug.Log(wp.position);
                _transform.position = wp.position;
                waitCounter = 0f;
                waiting = true;

                currentWaypointIndex = (currentWaypointIndex + 1) % _waypoints.Length;
                _navMeshAgent.ResetPath();
                //Debug.Log("Index increase");
                //animator.SetBool("Walking", false);
            }
            else
            {
                //Debug.Log(_transform.position);
                //Debug.Log("moving towards");
                _navMeshAgent.SetDestination(wp.position);

                // Lock rotations to specific Euler angles
                _transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
