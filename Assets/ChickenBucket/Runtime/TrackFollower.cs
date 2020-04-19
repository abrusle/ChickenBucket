using ChickenBucket.Runtime.Workers;
using PathCreation;
using UnityEngine;

namespace ChickenBucket.Runtime
{
    public class TrackFollower : MonoBehaviour
    {

        [Range(0, 1)]
        public float positionOnPathAtStart;
        public float moveSpeed;
        public bool rotateAlongPath;

        [SerializeField] private PathCreator pathCreator = null;
        [SerializeField] private bool debugLog = false;

        private Transform _tr;
        private float _currentDistanceOnTrack;

        private void Awake()
        {
            _tr = transform;
            GoToPositionOnTrack(positionOnPathAtStart);
        }

        public void GoToPositionOnTrack(float time)
        {
            _tr.position = pathCreator.path.GetPointAtTime(positionOnPathAtStart);
            _currentDistanceOnTrack = pathCreator.path.GetClosestDistanceAlongPath(_tr.position);
            if (rotateAlongPath)
                _tr.rotation = pathCreator.path.GetRotationAtDistance(_currentDistanceOnTrack);
        }

        public void MoveAlongTrack(float weightedDirection)
        {
            float newDist = _currentDistanceOnTrack + Mathf.Clamp(weightedDirection * moveSpeed * Time.deltaTime, -1, 1);
            if (newDist <= pathCreator.path.length && newDist >= 0)
            {
                _currentDistanceOnTrack = newDist;
                _tr.position = pathCreator.path.GetPointAtDistance(_currentDistanceOnTrack, EndOfPathInstruction.Stop);
                if (rotateAlongPath)
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