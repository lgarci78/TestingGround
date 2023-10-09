using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class BoundaryBT : MyTree
{

    public NavMeshAgent agent;

    [SerializeField] public float speed = 8f;
    [SerializeField] public static float fovRange = 10f;
    [SerializeField] public static float range = 6f;
    [SerializeField] public float maxDistance = 5f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform),
            }),

            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRangeBP(transform),
                new TaskGoToTarget(transform, agent),
            }),
            new BoundaryPatrol(transform, agent, maxDistance,speed),
        });

        

        return root;
    }
}