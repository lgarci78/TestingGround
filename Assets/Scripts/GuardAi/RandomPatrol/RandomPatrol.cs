using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //important

using BehaviorTree;


public class RandomPatrol : Node
{
    private Transform _transform;
    private NavMeshAgent _navMeshAgent;
    private float _range;

    public RandomPatrol(Transform transform, NavMeshAgent agent, float range)
    {
        //Debug.Log("IN RandomPatrol");
        _navMeshAgent = agent;
        _range = range;
        _transform = transform;
        //Debug.Log(_transform.rotation);
    }

    public override NodeState Evaluate()
    {
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            //Debug.Log("LESS THAN");
            Vector2 point;
            if (RandomPoint(_transform.position, _range, out point))
            {
                //Debug.Log("LESS THAN");
                Vector3 point3D = new Vector3(point.x, point.y, _transform.position.z);
                Debug.DrawRay(point3D, Vector3.up, Color.blue, 1.0f);

                _transform.rotation = Quaternion.Euler(0f,0f,0f);
                //Debug.Log(_transform.rotation);

                _navMeshAgent.SetDestination(point3D);
            }
        }
        //Debug.Log(_transform.rotation);
        _transform.rotation = Quaternion.Euler(0f,0f,0f);
        state = NodeState.RUNNING;
        return state;
    }

    private bool RandomPoint(Vector3 center, float range, out Vector2 result)
    {
        //Debug.Log("STARTING RANDOM POINT");
        Vector2 rand = Random.insideUnitCircle;
        Vector2 add = rand * range;
        Vector2 randomPoint = new Vector2(center.x, center.y) + add;
        
        //Debug.Log("Vector2 " + "X: " + center.x + "Y: " + center.y);
        //Debug.Log("Rand: " + rand);
        //Debug.Log("ADD: " + add);
        //Debug.Log("Randompt " + randomPoint);
        NavMeshHit hit;

        Vector3 samplePosition = new Vector3(randomPoint.x, randomPoint.y, center.z);
        //Debug.Log("sampleposition" + samplePosition);
        int navMeshArea = NavMesh.GetAreaFromName("Roam");
        //Debug.Log(navMeshArea);
        //if (NavMesh.SamplePosition(samplePosition, out hit, 1.0f, navMeshArea))
        if (NavMesh.SamplePosition(samplePosition, out hit, 10.0f, NavMesh.AllAreas))
        {
            //Debug.Log("INSIDE OF SAMPLE POSITION");
            
            result = new Vector2(hit.position.x, hit.position.y);
            
            return true;
        }

        result = Vector2.zero;
        return false;
    }
    
}
