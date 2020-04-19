using System;
using ChickenBucket.Runtime.Workers;
using UnityEngine;

namespace ChickenBucket.Runtime
{
    public class ProjectileLauncher : MonoBehaviour
    {
        public Projectile projectilePrefab;
        public Transform spawnRoot;

        
        private void Awake()
        {
            if (spawnRoot == null)
                spawnRoot = WorkerManager.Instance.Get<SceneWorker>().WorldOrigin;
        }

        public void Launch(Vector3? origin = null, Vector3? direction = null)
        {
            var projectile = Instantiate(projectilePrefab, spawnRoot, true);
            if (origin != null)
                projectile.transform.position = origin.Value;
            
            projectile.Go(direction ?? transform.up);
        }
    }
}