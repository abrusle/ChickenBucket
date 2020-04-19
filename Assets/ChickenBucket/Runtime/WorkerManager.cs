using UnityEngine;

namespace ChickenBucket.Runtime
{
    public class WorkerManager : MonoBehaviour
    {

        #region Singleton

        private static WorkerManager _instance;
        // public static WorkerManager Instance => _instance;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (_instance != null && _instance != this)
                Destroy(this.gameObject);
            else
                _instance = this;
        }

        #endregion

        public static T Get<T>()
        {
            var found = _instance.GetComponentInChildren<T>();
            if (found == null) throw new System.Exception($"Worker of type {nameof(T)} was not found.");
            return found;
        }
    }
}