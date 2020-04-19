using ChickenBucket.Runtime.Workers;
using UnityEngine;

namespace ChickenBucket.Runtime
{
    [RequireComponent(typeof(TrackFollower))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputWorker _inputWorker = null;
        
        private TrackFollower _trackFollower = null;
        private ProjectileLauncher _projectileLauncher;
        private bool _listenToPlayerInput = false;

        private void Awake()
        {
            _trackFollower = GetComponent<TrackFollower>();
            _projectileLauncher = GetComponent<ProjectileLauncher>();
        }

        public void Init()
        {
            _inputWorker = WorkerManager.Instance.Get<PlayerInputWorker>();
            
            _inputWorker.FireButtonClick += Fire;

            _listenToPlayerInput = true;
        }

        private void Fire()
        {
            _projectileLauncher.Launch(transform.position, transform.up);
        }

        private void Update()
        {
            if (_listenToPlayerInput)
                Move();
            Debug.DrawRay(transform.position, transform.up, Color.yellow);
        }

        private void Move()
        {
            _trackFollower.MoveAlongTrack(_inputWorker.MoveInput);
        }
    }
}