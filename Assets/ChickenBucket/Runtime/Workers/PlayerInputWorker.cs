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

        public Vector2 AimInput
        {
            get
            {
                if (_aimInputCache == null)
                    _aimInputCache = _aimAction.ReadValue<Vector2>();
                return _aimInputCache.Value;
            }
        }
    
        [SerializeField] private InputActionAsset inputActionAsset = null;

        private InputAction _fireAction;
        private InputAction _moveAction;
        private InputAction _aimAction;
        private float? _moveInputCache = null;
        private Vector2? _aimInputCache = null;

        private void Awake()
        {
            _moveAction = inputActionAsset["Move"];
            _fireAction = inputActionAsset["Fire"];
            _aimAction = inputActionAsset["Aim"];
        }

        private void LateUpdate()
        {
            _moveInputCache = null;
            _aimInputCache = null;
        }

        public void EnableActions()
        {
            _moveAction.Enable();
            _aimAction.Enable();
            _fireAction.Enable();
            _fireAction.started += _ => FireButtonClick?.Invoke();
        }
    }
}