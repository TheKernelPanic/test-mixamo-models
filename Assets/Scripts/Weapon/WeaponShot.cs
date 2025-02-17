using System;
using UnityEngine;

namespace Example.Armament
{
    public class WeaponShot : MonoBehaviour
    {
        public GameObject bullet;
        public Transform spawnPoint;

        public float shotForce = 1500f;
        public float shotRate = 0.5f;

        private float _shotRateTime = 0;
        private uint _timeToDestroy = 2;
        
        public void Shot()
        {
            if (Time.time > _shotRateTime)
            {
                GameObject newBullet = Instantiate(
                    bullet,
                    spawnPoint.position,
                    spawnPoint.rotation);
                    
                newBullet.GetComponent<Rigidbody>().AddForce(
                    spawnPoint.forward * shotForce);

                _shotRateTime = Time.time + shotRate;
                    
                Destroy(newBullet, _timeToDestroy);
            }
        }
    }
}