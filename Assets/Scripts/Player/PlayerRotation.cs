using System;
using Example.Cameras;
using UnityEngine;

namespace Example.Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float _speedRotation = 250f;
        private Camera _mainCamera;
        private Plane _groundPlane;
        private Vector3 _lastMousePosition;

        private void Start()
        {
            _mainCamera = Camera.main;
            _groundPlane = new Plane(Vector3.up, transform.position);
            _lastMousePosition = Input.mousePosition;
        }

        private void Update()
        {
            if (_lastMousePosition != Input.mousePosition)
            {
                UpdateRotationToMouse();
                _lastMousePosition = Input.mousePosition;
            }
        }

        private void UpdateRotationToMouse()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (_groundPlane.Raycast(ray, out float rayDistance))
            {
                Vector3 targetPoint = ray.GetPoint(rayDistance);
                Vector3 lookDirection = targetPoint - transform.position;
                lookDirection.y = 0; // Keep plane
                
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

                if (lookDirection.sqrMagnitude > 0.01f) // Avoid unnecessary rotations
                {
                    Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _speedRotation * Time.deltaTime);
                }
            }
        }
    }
}