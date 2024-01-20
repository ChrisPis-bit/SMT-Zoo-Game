using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GuardState
{
    Patrol,
    Inspect,
    Chase
}

public class GuardAI : MonoBehaviour
{
    [SerializeField] private WayPointHandler _wayPointHandler;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _reachDistance = .2f;
    [SerializeField] private float _playerGuardDistance = 5.0f;
    [SerializeField] private float _inspectTime = 3.0f;
    [SerializeField] private float _recalculateReturnPathInterval = 1.0f;

    [SerializeField] private float _sightRange = 7.5f;

    private Vector3 _target;
    private uint _targetIndex = 0;
    private float _currentInspectTime;
    private float _currentRecalculateTime;
    private bool _isReturning;
    private Vector3 _origin;

    public static event Action onPlayerCaught;

    public GuardState State { get; private set; }

    private void Start()
    {
        _origin = transform.position;
        StartPatrol();
    }

    private void Update()
    {
        // Check for sight range
        bool playerInSight = PlayerMovement.LocalPlayer != null && Vector3.Distance(PlayerMovement.LocalPlayer.transform.position, transform.position) <= _sightRange;

        if (playerInSight)
            State = GuardState.Chase;

        switch (State)
        {
            case GuardState.Patrol:
                Patrol();
                break;
            case GuardState.Inspect:
                Inspect();
                break;
            case GuardState.Chase:
                ChasePlayer();
                break;
        }
        
    }

    public void StartPatrol()
    {
        _agent.isStopped = false;
        _isReturning = true;
        _currentRecalculateTime = 0;
        _wayPointHandler.GetClosestPointToPath(transform.position, out _target, out _targetIndex);
        _agent.SetDestination(_target);
        State = GuardState.Patrol;
    }

    public void StartInspect(Vector3 inspectPos)
    {
        _currentInspectTime = 0;
        _target = inspectPos;
        _agent.SetDestination(_target);
        State = GuardState.Inspect;
    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, _target) <= _reachDistance)
        {
            _isReturning = false;
            _currentRecalculateTime = 0;

            _targetIndex = _wayPointHandler.GetNextTarget(_targetIndex);
            _target = _wayPointHandler.GetPoint(_targetIndex);
            _agent.SetDestination(_target);
        }

        // Recalculates the optimal path when returning to the patrol.
        if (_isReturning)
        {
            _currentRecalculateTime += Time.deltaTime;

            if (_currentRecalculateTime >= _recalculateReturnPathInterval)
            {
                _currentRecalculateTime = 0;
                _wayPointHandler.GetClosestPointToPath(transform.position, out _target, out _targetIndex);
                _agent.SetDestination(_target);
            }
        }
    }

    private void Inspect()
    {
        if (!_agent.isStopped && Vector3.Distance(transform.position, _target) <= _reachDistance)
            _agent.isStopped = true;

        if (_agent.isStopped)
        {
            _currentInspectTime += Time.deltaTime;
            if (_currentInspectTime >= _inspectTime)
                StartPatrol();
        }
    }

    private void ChasePlayer()
    {
        bool playerInSight = PlayerMovement.LocalPlayer != null && Vector3.Distance(PlayerMovement.LocalPlayer.transform.position, transform.position) <= _sightRange;

        if (!playerInSight)
        {
            StartPatrol();
            Debug.Log("leftState");
            return;
        }

        _agent.SetDestination(PlayerMovement.LocalPlayer.transform.position);

        float distanceToPlayer = Vector3.Distance(transform.position, PlayerMovement.LocalPlayer.transform.position);


        // If the player is too close, you can implement a behavior
        if (distanceToPlayer < _playerGuardDistance)
        {
            //Show Game Over - Failed State
            onPlayerCaught?.Invoke();
            transform.position = _origin;
            StartPatrol();
        }
    }
}
