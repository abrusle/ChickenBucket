using UnityEngine;
using UnityEngine.InputSystem;

namespace ChickenBucket.Runtime.Workers
{
    public class PlayerInputWorker : MonoBehaviour
    {
        public event System.Action FireButtonClick;
        
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
        private InputAction _fireAction;
        private float? _moveInputCache = null;

        private void Awake()
        {
            _moveAction = inputActionAsset["Move"];
            _fireAction = inputActionAsset["Fire"];
        }

        private void LateUpdate()
        {
            _moveInputCache = null;
        }

        public void EnableActions()
        {
            _moveAction.Enable();
            _fireAction.Enable();
            _fireAction.started += _ => FireButtonClick?.Invoke();
        }
    }
}