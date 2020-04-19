using System.Collections;
using UnityEngine;

namespace ChickenBucket.Runtime.Workers
{
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        public float speed = 10;
        public float acceleration = 1;

        private Vector3 _dir;
        public void Go(Vector3 direction)
        {
            _dir = direction;
            StartCoroutine(Travel());
        }

        private IEnumerator Travel()
        {
            while (transform.position.magnitude <= 100)
            {
                speed *= acceleration;
                transform.Translate(_dir * speed);
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}