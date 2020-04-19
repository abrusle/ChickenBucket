using UnityEngine;

namespace ChickenBucket.Runtime
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        private static GameManager _instance;
        public static GameManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(this.gameObject);
            else
                _instance = this;
        }

        #endregion
    
        public Wagon wagon;

        private void Start()
        {
            WorkerManager.Get<PlayerInputWorker>().EnableActions();
        
            wagon.Init();
        }
    }
}