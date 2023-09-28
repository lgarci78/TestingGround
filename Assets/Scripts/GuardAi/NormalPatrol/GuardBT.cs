using System.Collections.Generic;
using BehaviorTree;
using UnityEngine.AI;

public class GuardBT : MyTree
{
    public UnityEngine.Transform[] waypoints;
    public NavMeshAgent agent;

    public static float speed = 2f;
    public static float fovRange = 6f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform, agent),
            }),
            new Patrol(transform, waypoints, agent),
        });

        // Node root = new Patrol(transform, waypoints);
        

        return root;
    }
}
