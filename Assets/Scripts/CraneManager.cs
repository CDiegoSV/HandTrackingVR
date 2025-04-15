using UnityEngine;
using UnityEngine.XR.Content.Interaction;

namespace Dante {

    public enum CraneStates {
        NONE, RESTART, INTERACTABLE, SHIPPING
    }

	public class CraneManager : MonoBehaviour
	{
		#region References

		[SerializeField] protected XRJoystick _joystick;
        [SerializeField] protected XRPushButton _xRPushButton;
		//[SerializeField] protected XRLever _xRLever;
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] protected Animator _animator;

        [SerializeField] protected Transform _winZone;
        [SerializeField] protected Transform _defaultZone;

        #endregion

        #region Knobs

        [SerializeField] protected float moveSpeed;

        #endregion

        #region RuntimeVariables

        protected CraneStates craneState;

        protected Vector3 _input;

        #endregion

        #region UnityMethods

        private void OnEnable()
        {
            StateMechanic(CraneStates.RESTART);
        }

        private void OnDisable()
        {
            RemoveAllJoystickListeners();
        }

        private void FixedUpdate()
        {
            ExecuteState();
        }

        #endregion

        #region PublicMethods

        public void StateMechanic(CraneStates craneNextState) {
            FinalizeState();
            craneState = craneNextState;
            InitializeState();
        }

        public void RemoveAllJoystickListeners()
        {
            _joystick.onValueChangeX?.RemoveAllListeners();
            _joystick.onValueChangeY?.RemoveAllListeners();
            _input = Vector3.zero;
        }

        public void GetJoystickListeners()
        {
            RemoveAllJoystickListeners();
            _joystick.onValueChangeX.AddListener(MoveCraneX);
            _joystick.onValueChangeY.AddListener(MoveCraneZ);
        }

        #endregion

        #region LocalMethods

        protected void InitializeState() {
            switch (craneState) {
                case CraneStates.INTERACTABLE:
                    GetJoystickListeners();
                    _input = Vector3.zero;
                    _rigidbody.velocity = Vector3.zero;
                    _rigidbody.isKinematic = false;
                    _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                    break;

                case CraneStates.SHIPPING:
                    _rigidbody.isKinematic= true;
                    break;
            }
        }
        protected void ExecuteState() {
            switch (craneState) {
                case CraneStates.RESTART:
                    Vector3 tempCraneDefaultDirection = (_defaultZone.position - transform.position).normalized;
                    MoveCrane(tempCraneDefaultDirection);
                    if (Vector3.Distance(transform.position, _defaultZone.position) < 0.2f)
                    {
                        StateMechanic(CraneStates.INTERACTABLE);
                    }
                    break;

                case CraneStates.INTERACTABLE:
                    if(_input.magnitude > 0)
                        MoveCrane(_input);
                    break;

                case CraneStates.SHIPPING:
                    Vector3 tempCraneDirection = (_winZone.position - transform.position).normalized;
                    MoveCrane(tempCraneDirection);
                    if(Vector3.Distance(transform.position, _winZone.position) < 0.2f) {
                        _animator.SetBool("Release", true);
                        _animator.SetBool("Release", true);
                    }
                    break;
            }
        }

        protected void FinalizeState() {
            switch (craneState) {
                case CraneStates.RESTART:
                    _xRPushButton.toggleValue = false;
                    break;

                case CraneStates.INTERACTABLE:
                    _xRPushButton.toggleValue = true;
                    break;

                case CraneStates.SHIPPING:
                    _animator.SetBool("Release", false);
                    break;
            }
        }

        protected void MoveCrane(Vector3 p_inputDirection)
        {
            
            _rigidbody.MovePosition(_rigidbody.position + p_inputDirection.normalized * moveSpeed * Time.fixedDeltaTime);
        }

        protected void MoveCraneX(float p_xMovement)
        {
            _input.x = p_xMovement;
        }

        protected void MoveCraneY(float p_yMovement)
        {
            _input.y = p_yMovement;
        }

        protected void MoveCraneZ(float p_zMovement)
        {
            _input.z = p_zMovement;
        }

        #endregion

        #region GettersSetters



        #endregion
    }
}
