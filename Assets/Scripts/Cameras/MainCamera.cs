using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example.Cameras
{
    public class MainCamera : MonoBehaviour
    {
        public Transform player;
        public Vector3 offset = new Vector3 (0, 10, -4);

        public float zoomSpeed = 5f;
        public float minZoom = 4f;
        public float maxZoom = 8f;

        private float _currentZoom;

        void Start()
        {
            _currentZoom = offset.magnitude;
        }

        void Update()
        {
            if (player != null)
            {
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                _currentZoom -= scroll * zoomSpeed;
                _currentZoom = Mathf.Clamp(_currentZoom, minZoom, maxZoom);
            }
        }

        void LateUpdate()
        {
            if (this.player != null)
            {
                float zoomLerp = Mathf.Lerp(offset.magnitude, _currentZoom, Time.deltaTime * zoomSpeed);
                offset = offset.normalized * zoomLerp;

                transform.position = player.position + offset + new Vector3(0, 4, 3);
                transform.LookAt(player.position + Vector3.up * 1.5f);
            }
        }
    }
}