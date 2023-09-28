using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class RangedBT : BehaviorTree.MyTree
{

    public NavMeshAgent agent;
    public Transform shootingPoint;
    public GameObject projectilePrefab;

    public static float speed = 2f;
    public static float fovRange = 15f;
    public static float range = 5f;
    public static float maxDistance = 5f;

    public Transform playerTransform;
    public float projectileSpeed = 10f;
    public float cooldown = 4f;
    public float distance = 9f;

    public ProjectileManager projectileManager;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new RangeCheckInRange(transform),
                new CloseAndShoot(transform, playerTransform, shootingPoint, projectilePrefab, distance, cooldown, agent, projectileSpeed, projectileManager),
            }),
            new BoundaryPatrol(transform, agent, maxDistance, speed),
        });

        // Node root = new Patrol(transform, waypoints);


        return root;
    }
}
