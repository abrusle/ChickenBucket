using UnityEngine;

namespace ChickenBucket.Runtime.Workers
{
    public class SceneWorker : MonoBehaviour
    {
        public Transform WorldOrigin => worldOrigin;
        [SerializeField] private Transform worldOrigin = null;

    }
}