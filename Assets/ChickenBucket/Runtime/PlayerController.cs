using ChickenBucket.Runtime.Workers;
using UnityEngine;

namespace ChickenBucket.Runtime
{
    [RequireComponent(typeof(TrackFollower))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ProjectileLauncher projectileLauncher = null;
        
        private PlayerInputWorker _inputWorker = null;
        
        private TrackFollower _trackFollower = null;
        private bool _listenToPlayerInput = false;

        private void Awake()
        {
            _trackFollower = GetComponent<TrackFollower>();
        }

        public void Init()
        {
            _inputWorker = WorkerManager.Instance.Get<PlayerInputWorker>();
            
            _inputWorker.FireButtonClick += Fire;

            _listenToPlayerInput = true;
        }

        private void Fire()
        {
            projectileLauncher.Launch();
        }

        private void Update()
        {
            if (_listenToPlayerInput)
            {
                Move();
                Aim();
            }
        }

        private void Aim()
        {
            projectileLauncher.AimTowards(_inputWorker.AimInput.normalized);
        }

        private void Move()
        {
            _trackFollower.MoveAlongTrack(_inputWorker.MoveInput);
        }
    }
}