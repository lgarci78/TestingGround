using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class CloseAndShoot : Node
{
    private Transform _transform;
    private Transform _playerTransform;
    private Transform _shootingPoint;
    private GameObject _projectilePrefab;
    private float _desiredDistance;
    private float _shootingCooldown;
    private NavMeshAgent _agent;
    private float _lastShotTime;
    private float _projectileSpeed;
    private Quaternion _initialRotation;

    private ProjectileManager _projectileManager;

    public CloseAndShoot(Transform transform, Transform playerTransform, Transform shootingPoint,
                             GameObject projectilePrefab, float desiredDistance, float shootingCooldown,
                             NavMeshAgent agent, float projectileSpeed, ProjectileManager projectileManager)
    {
        _transform = transform;
        _playerTransform = playerTransform;
        _shootingPoint = shootingPoint;
        _projectilePrefab = projectilePrefab;
        _desiredDistance = desiredDistance;
        _shootingCooldown = shootingCooldown;
        _agent = agent;
        _lastShotTime = -shootingCooldown;
        _projectileSpeed = projectileSpeed;
        _initialRotation = _transform.rotation;
        _projectileManager = projectileManager;
        //_agent.updateRotation = false; // Disable automatic rotation updates

    }

    public override NodeState Evaluate()
    {
        if (_playerTransform == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        float distanceToPlayer = Vector3.Distance(_transform.position, _playerTransform.position);

        if (distanceToPlayer > _desiredDistance)
        {
            Debug.Log("If the distance to player is greater than 9");
            if (distanceToPlayer > 20f)
            {
                _agent.ResetPath(); // Reset the path to stop pursuing the player
                //_transform.rotation = _initialRotation; // Reset the rotation
                state = NodeState.FAILURE; // Return a failure state
                Debug.Log("Player to far...returning to patrol");
            }
            else
            {
                Vector3 desiredPosition = _playerTransform.position + (_transform.position - _playerTransform.position).normalized * _desiredDistance;
                //Debug.Log("Before rotation change: " + _transform.rotation.eulerAngles);
                //_transform.rotation = _initialRotation;
                //Debug.Log("After rotation change: " + _transform.rotation.eulerAngles);
                _agent.SetDestination(desiredPosition);
                //_transform.rotation = _initialRotation;
                state = NodeState.RUNNING;
                Debug.Log("Player is within range, setting ai to desired position: " + desiredPosition);
                Debug.Log("distance to player: " + distanceToPlayer);
            }
            
        }
        else
        {
            //Debug.Log("If the distance to player is less than 9");
            _agent.ResetPath();

            if (Time.time - _lastShotTime >= _shootingCooldown)
            {
                Debug.Log("Before rotation change: " + _transform.rotation.eulerAngles);
                //_transform.rotation = _initialRotation;
                Debug.Log("After rotation change: " + _transform.rotation.eulerAngles);

                Shoot();
                //_transform.rotation = _initialRotation;
                _lastShotTime = Time.time;
                state = NodeState.SUCCESS;
            }
            else
            {
                state = NodeState.RUNNING;
            }
        }

        return state;
    }

    private void Shoot()
    {
        //_transform.rotation = _initialRotation;
        Vector3 directionToPlayer = (_playerTransform.position - _shootingPoint.position).normalized;
        GameObject projectile = GameObject.Instantiate(_projectilePrefab, _shootingPoint.position, Quaternion.identity);
        
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        if (projectileRigidbody != null)
        {
            projectileRigidbody.velocity = directionToPlayer * _projectileSpeed;
        }

        _projectileManager.DestroyProjectile(projectile, 3f);
    }

}
