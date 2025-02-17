using System;
using Example.Cameras;
using UnityEngine;

namespace Example.Armament
{
    public class LineWeapon : MonoBehaviour
    {
        public Camera mainCamera;
        public LineRenderer lineRenderer;
        public Transform muzzle;
        public LayerMask groundLayer;
        
        public float laserDistance = 100f;
        
        
        private void Update()
        {
            // 1. Obtener la posición mundial del ratón (mouseWorldPos). 
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 mouseWorldPos;
            if (Physics.Raycast(ray, out hit, 1000f, groundLayer))
            {
                mouseWorldPos = hit.point;
            }
            else
            {
                // Si no choca con nada, tomamos algún punto lejano
                mouseWorldPos = ray.GetPoint(1000f); 
            }

            // 2. Calcular dirección
            Vector3 direction = (mouseWorldPos - muzzle.position).normalized;

            // 3. Definir los puntos del Line Renderer
            lineRenderer.SetPosition(0, muzzle.position); // Inicio en la boca del arma

            // "Infinito" puede ser un número grande. 
            // Si quieres que no atraviese paredes, haz otro raycast. Ejemplo:
            RaycastHit laserHit;
            if(Physics.Raycast(muzzle.position, direction, out laserHit, laserDistance))
            {
                // Si choca con algo, ponemos el final en el punto de impacto
                lineRenderer.SetPosition(1, laserHit.point);
            }
            else
            {
                // De lo contrario, dibujamos "hasta" laserDistance
                lineRenderer.SetPosition(1, muzzle.position + direction * laserDistance);
            }
        }
    }
}