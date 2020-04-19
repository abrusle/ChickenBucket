using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenBucket.Runtime
{
    public class WorkerManager : MonoBehaviour
    {

        #region Singleton

        private static WorkerManager _instance;
        public static WorkerManager Instance => _instance;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (_instance != null && _instance != this)
                Destroy(this.gameObject);
            else
                _instance = this;
        }

        #endregion

        private static readonly Dictionary<Type, MonoBehaviour> CachedWorkers = new Dictionary<Type, MonoBehaviour>();

        public T Get<T>() where T : MonoBehaviour
        {
            Type wType = typeof(T);
            if (CachedWorkers.ContainsKey(wType))
                return (T) CachedWorkers[wType];
            
            T found = _instance.GetComponentInChildren<T>();
            if (found == null) throw new Exception($"Worker of type {nameof(T)} was not found.");
            CachedWorkers.Add(wType, found);
            return found;

        }
    }
}