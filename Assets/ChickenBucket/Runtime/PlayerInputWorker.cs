using UnityEngine;
using UnityEngine.InputSystem;

namespace ChickenBucket.Runtime
{
    public class PlayerInputWorker : MonoBehaviour
    {

        public float MoveInput
        {
            get
            {
                if (_moveInputCache == null)
                    _moveInputCache = _moveAction.ReadValue<float>();
                return _moveInputCache.Value;
            }
        }
    
        [SerializeField] private InputActionAsset inputActionAsset = null;

        private InputAction _moveAction;
        private float? _moveInputCache = null;

        private void Awake()
        {
            _moveAction = inputActionAsset["Move"];
        }

        private void LateUpdate()
        {
            _moveInputCache = null;
        }

        public void EnableActions()
        {
            _moveAction.Enable();
        }
    }
}