using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayPointHandler : MonoBehaviour
{
    [SerializeField] private Transform[] _wayPoints;

    public Transform[] WayPoints => _wayPoints;

    private Vector3 ClosestPointOnLine(Vector3 posA, Vector3 posB, Vector3 position)
    {
        Vector3 vecAP = position - posA;
        Vector3 vecAB = (posB - posA).normalized;

        float segLen = Vector3.Distance(posA, posB);
        float t = Vector3.Dot(vecAP, vecAB);

        if (t <= 0)
            return posA;

        if (t >= segLen)
            return posB;

        return posA + vecAB * t;
    }

    public void GetClosestPointToPath(Vector3 position, out Vector3 closestPoint, out uint startIndex)
    {
        float bestDist = float.MaxValue;
        closestPoint = position;
        startIndex = 0;

        for (uint i = 0; i < _wayPoints.Length; i++)
        {
            uint start = i;
            uint end = i + 1;
            if (end >= _wayPoints.Length) end = 0;

            Vector3 posA = _wayPoints[start].position;
            Vector3 posB = _wayPoints[end].position;

            Vector3 closestSegPoint = ClosestPointOnLine(posA, posB, position);

            float dist = Vector3.Distance(position, closestSegPoint);
            if (dist < bestDist)
            {
                bestDist = dist;
                closestPoint = closestSegPoint;
                startIndex = start;
            }
        }
    }

    public uint GetNextTarget(uint currentTarget)
    {
        uint next = currentTarget + 1;
        if (next >= _wayPoints.Length)
            next = 0;

        return next;
    }

    public Vector3 GetPoint(uint index)
    {
        return _wayPoints[index].position;
    }

    private void OnDrawGizmos()
    {
        if (_wayPoints.Length == 0 || _wayPoints.Any((w) => w == null))
            return;

        for (uint i = 0; i < _wayPoints.Length; i++)
        {
            uint start = i;
            uint end = i + 1;
            if (end >= _wayPoints.Length) end = 0;

            Vector3 posA = _wayPoints[start].position;
            Vector3 posB = _wayPoints[end].position;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(posA, .5f);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(posA, posB);
        }
    }
}
