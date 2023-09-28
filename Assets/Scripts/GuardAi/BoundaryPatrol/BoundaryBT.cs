using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class BoundaryBT : MyTree
{

    public NavMeshAgent agent;

    [SerializeField] public float speed = 8f;
    [SerializeField] public float fovRange = 6f;
    [SerializeField] public float range = 5f;
    [SerializeField] public float maxDistance = 5f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform, agent),
            }),
            new BoundaryPatrol(transform, agent, maxDistance,speed),
        });

        

        return root;
    }
}