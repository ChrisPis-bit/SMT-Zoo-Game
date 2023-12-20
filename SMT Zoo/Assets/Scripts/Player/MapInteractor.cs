using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapInteractor : MonoBehaviour
{
    /// <summary>
    /// Object which forward will be used as the interaction direction.
    /// </summary>
    [SerializeField] private Transform _interactor;
    [SerializeField] private Transform _indicator;
    [SerializeField] private Transform _testWorldIndicator;
    [SerializeField] private float _maxDistance = .75f;
    [SerializeField] private Vector2 _mapSize = new Vector2(100,100);

    public UnityEvent<Vector2> onPressed;

    private bool _pressed = false;

    private void Update()
    {
        //TODO: Controller input.
        if (Input.GetMouseButtonDown(0))
            _pressed = true;
    }

    private void FixedUpdate()
    {
        Physics.Raycast(_interactor.position, _interactor.forward, out RaycastHit hitInfo, _maxDistance);

        if (hitInfo.collider && hitInfo.collider.gameObject == gameObject)
        {
            _indicator.gameObject.SetActive(true);
            _indicator.position = hitInfo.point;
            _indicator.forward = hitInfo.normal;

            if (_pressed)
            {
                Vector3 localPos = transform.InverseTransformPoint(hitInfo.point);
                Vector2 mapCoords = new Vector2(localPos.x, localPos.y) * _mapSize;
                Debug.Log("Map: " + mapCoords + " - Local: " + localPos);

                if(_testWorldIndicator) _testWorldIndicator.position = new Vector3(mapCoords.x, 0, mapCoords.y);

                onPressed?.Invoke(mapCoords);
            }
        }
        else
        {
            _indicator.gameObject.SetActive(false);
        }

        _pressed = false;
    }
}
