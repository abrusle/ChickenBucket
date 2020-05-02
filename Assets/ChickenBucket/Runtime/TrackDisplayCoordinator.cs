using PathCreation;
using UnityEngine;

namespace ChickenBucket.Runtime
{
    [ExecuteAlways]
    public class TrackDisplayCoordinator : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer = null;
        [SerializeField] private PathCreator pathCreator = null;

        private void OnEnable()
        {
            if (pathCreator != null)
                pathCreator.pathUpdated += OnPathUpdated;
            CopyPathToLine();
        }

        private void OnPathUpdated()
        {
            CopyPathToLine();
        }

        private void CopyPathToLine()
        {
            lineRenderer.positionCount = pathCreator.path.NumPoints;
            lineRenderer.SetPositions(pathCreator.path.localPoints);
        }
    }
}