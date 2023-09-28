using System.Collections.Generic;
using BehaviorTree;
using UnityEngine.AI;

public class RandomGuardBT : MyTree
{
    public NavMeshAgent agent;

    public static float speed = 2f;
    public static float fovRange = 6f;
    public static float range = 10f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
            new CheckEnemyInFOVRange(transform),
            new TaskGoToTarget(transform,agent),
            }),
            new RandomPatrol(transform, agent, range),
        });
        
        return root;
    }
}
