using System;
using UnityEngine;

namespace Example.Armament
{
    public class WeaponShot : MonoBehaviour
    {
        [Header("Bullet settings")]
        public GameObject bullet;
        public Transform spawnPoint;
        public AudioSource soundEffect;
        
        public float shotForce = 1500f;
        public float shotRate = 0.5f;

        private float _shotRateTime = 0;
        private float _bulletLifeTime = 2f;
        
        public void Shot()
        {
            if (Time.time > _shotRateTime)
            {
                Vector3 targetPoint = GetAimPoint();
                
                Vector3 direction = (targetPoint - spawnPoint.position).normalized;
                
                Debug.DrawLine(spawnPoint.position, targetPoint, Color.yellow, 2f);
                Debug.DrawRay(targetPoint, Vector3.up * 2, Color.green, 2f);
                
                GameObject newBullet = Instantiate(
                    bullet,
                    spawnPoint.position,
                    Quaternion.LookRotation(direction));

                newBullet.GetComponent<Rigidbody>().AddForce(direction * shotForce, ForceMode.Impulse);
                
                Sound();
                
                _shotRateTime = Time.time + shotRate;

                Destroy(newBullet, _bulletLifeTime);
            }
        }

        private void Sound()
        {
            soundEffect.Play();
        }

        private Vector3 GetAimPoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, spawnPoint.position);
            
            if (plane.Raycast(ray, out float distance))
            {
                return ray.GetPoint(distance);
            }
            return spawnPoint.position + spawnPoint.forward * 10f;
        }
    }
}