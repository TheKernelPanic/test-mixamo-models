using UnityEngine;

namespace Example.Armament
{
    public class Bullet : MonoBehaviour
    {
        public GameObject impactEffect;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Debug.Log("Impact bullet");
            }
        }
    }
}