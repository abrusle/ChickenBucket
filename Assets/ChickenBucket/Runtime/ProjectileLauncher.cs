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

        public void Launch()
        {
            var projectile = Instantiate(projectilePrefab, spawnRoot, true);
            projectile.transform.position = transform.position;
            projectile.Go(transform.up);
        }

        public void AimTowards(Vector2 direction)
        {
            transform.up = direction;
        }
    }
}