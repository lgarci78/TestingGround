using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;
using NavMeshPlus.Components;
using UnityEditor.PackageManager.UI;
using UnityEngine.UIElements;

public class BoundaryPatrol : Node
{
    private Transform _transform;
    private NavMeshAgent _navMeshAgent;
    private float _maxDistance;
    private Bounds _roamBounds;
    private Vector3 _targetPoint;
    private float _stoppingDistance = 1.0f;
    private GameObject _aiGameObject;
    private float _patrolSpeed;

    public BoundaryPatrol(Transform transform, NavMeshAgent navMeshAgent, float maxDistance, float patrolSpeed)
    {
        _transform = transform;
        _navMeshAgent = navMeshAgent;
        _maxDistance = maxDistance;
        _aiGameObject = DetectCurrentZone();

        // Initialize _roamBounds
        _roamBounds = CalculateRoamBounds(_aiGameObject);

        _targetPoint = GetRandomPointInBounds();
        _patrolSpeed = patrolSpeed;
        _navMeshAgent.speed = _patrolSpeed;
    }

    private GameObject DetectCurrentZone()
    {
        // Cast a ray from the AI's position downward in 2D space to detect the current zone (trigger zone)
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, Vector2.down, Mathf.Infinity);
        if (hit.collider != null)
        {
            GameObject zone = hit.collider.gameObject;
            //Debug.Log(zone.name);
            return zone;
        }
        
        // Default to no zone found
        return null;
    }

    private Bounds CalculateRoamBounds(GameObject currentZone)
    {
        // Initialize the bounds
        Bounds combinedBounds = new Bounds(Vector3.zero, Vector3.zero);

        // Find all NavMeshModifiers with the "Roam" area type.
        NavMeshModifier[] roamModifiers = GameObject.FindObjectsOfType<NavMeshModifier>();
        foreach (var modifier in roamModifiers)
        {
            if (modifier.overrideArea && modifier.area == NavMesh.GetAreaFromName("Roam"))
            {
                // Check if the modifier's zone matches the AI's current zone
                if (modifier.gameObject == currentZone)
                {
                    //Debug.Log("FOUND THE ZONE");
                    // Get the associated GameObject's bounds and encapsulate it
                    Bounds bounds = modifier.gameObject.GetComponent<Renderer>().bounds;
                    combinedBounds = bounds;

                    // Print debug information
                    string gameObjectName = modifier.gameObject.name;
                    Debug.Log("Expanding _roamBounds with GameObject: " + gameObjectName);
                }
            }
        }

        return combinedBounds;
    }

    public override NodeState Evaluate()
    {
        if (_navMeshAgent.remainingDistance <= _stoppingDistance)
        {
            _targetPoint = GetRandomPointInBounds();
            _navMeshAgent.SetDestination(_targetPoint);
        }

        return NodeState.RUNNING;
    }

    private Vector3 GetRandomPointInBounds()
    {
        if (_aiGameObject != null)
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(_roamBounds.min.x, _roamBounds.max.x),
                Random.Range(_roamBounds.min.y, _roamBounds.max.y),
                _roamBounds.center.z
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, _maxDistance, NavMesh.AllAreas))
            {
                   return hit.position;
            }
        }

        // Default to the AI's current position if no valid point is found
        return _transform.position;
    }
}
