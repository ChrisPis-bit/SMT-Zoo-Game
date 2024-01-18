using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GuardState
{
    Patrol,
    Inspect
}

public class GuardAI : MonoBehaviour
{
    [SerializeField] private WayPointHandler _wayPointHandler;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _reachDistance = .2f;
    [SerializeField] private float _inspectTime = 3.0f;
    [SerializeField] private float _recalculateReturnPathInterval = 1.0f;

    private Vector3 _target;
    private uint _targetIndex = 0;
    private float _currentInspectTime;
    private float _currentRecalculateTime;
    private bool _isReturning;

    public GuardState State { get; private set; }

    private void Start()
    {
        StartPatrol();
    }

    private void Update()
    {
        switch (State)
        {
            case GuardState.Patrol:
                Patrol();
                break;
            case GuardState.Inspect:
                Inspect();
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
}
