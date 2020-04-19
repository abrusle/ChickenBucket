using System;
using PathCreation;
using UnityEngine;

namespace ChickenBucket.Runtime
{
    public class Wagon : MonoBehaviour
    {

        [Range(0, 1)]
        public float positionOnPathAtStart;
        public float moveSpeed;

        [SerializeField] private PathCreator pathCreator = null;
        [SerializeField] private bool debugLog = false;

        private PlayerInputWorker _inputWorker;
        private bool _listenToPlayerInput = false;
        private Transform _tr;
        private float _currentDistanceOnTrack;

        private void Awake()
        {
            _tr = transform;
        }

        public void Init()
        {
            _inputWorker = WorkerManager.Get<PlayerInputWorker>();
            
            _tr.position = pathCreator.path.GetPointAtTime(positionOnPathAtStart);
            _currentDistanceOnTrack = pathCreator.path.GetClosestDistanceAlongPath(_tr.position);
            
            _listenToPlayerInput = true;
        }

        private void Update()
        {
            if (_listenToPlayerInput)
                ProcessLivePlayerInput();
        }

        private void ProcessLivePlayerInput()
        {
            UpdateTransform();
        }

        private void UpdateTransform()
        {
            float newDist = _currentDistanceOnTrack + Mathf.Clamp(_inputWorker.MoveInput * moveSpeed * Time.deltaTime, -0.999f, 0.999f);
            if (newDist <= pathCreator.path.length && newDist >= 0)
            {
                _currentDistanceOnTrack = newDist;
                _tr.position = pathCreator.path.GetPointAtDistance(_currentDistanceOnTrack, EndOfPathInstruction.Stop);
                _tr.rotation = pathCreator.path.GetRotationAtDistance(_currentDistanceOnTrack, EndOfPathInstruction.Stop);
                Print($"Current distance: {_currentDistanceOnTrack}");
            }
        }

        private void Print(string message)
        {
            if (!debugLog) return;
            Debug.Log($"<color=purple>{message}</color>");
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (pathCreator != null)
            {
                transform.position = pathCreator.path.GetPointAtTime(positionOnPathAtStart);
            }
        }
#endif
    }
}