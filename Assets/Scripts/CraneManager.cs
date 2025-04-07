using UnityEngine;
using UnityEngine.XR.Content.Interaction;

namespace Dante {

    public enum CraneStates {
        NONE, INTERACTABLE, SHIPPING
    }

	public class CraneManager : MonoBehaviour
	{
		#region References

		[SerializeField] protected XRJoystick _joystick;
		//[SerializeField] protected XRLever _xRLever;
        [SerializeField] protected Rigidbody _rigidbody;

        [SerializeField] protected Transform _winZone;

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
            GetJoystickListeners();

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
            _joystick.onValueChangeX.RemoveAllListeners();
            _joystick.onValueChangeY.RemoveAllListeners();
            _input = Vector3.zero;
        }

        public void GetJoystickListeners()
        {
            _joystick.onValueChangeX.AddListener(MoveCraneX);
            _joystick.onValueChangeY.AddListener(MoveCraneZ);
            StateMechanic(CraneStates.INTERACTABLE);
        }

        #endregion

        #region LocalMethods

        protected void InitializeState() {
            switch (craneState) {
                case CraneStates.INTERACTABLE:

                    break;

                case CraneStates.SHIPPING:

                    break;
            }
        }
        protected void ExecuteState() {
            switch (craneState) {
                case CraneStates.INTERACTABLE:
                    if(_input.magnitude > 0)
                        MoveCrane(_input);
                    break;
                case CraneStates.SHIPPING:
                    Vector3 tempCraneDirection = (transform.position - _winZone.position).normalized;
                    MoveCrane(tempCraneDirection);
                    if(Vector3.Distance(transform.position, _winZone.position) < 0.1f) {
                        StateMechanic(CraneStates.INTERACTABLE);
                    }
                    break;
            }
        }

        protected void FinalizeState() {
            switch (craneState) {
                case CraneStates.INTERACTABLE:

                    break;

                case CraneStates.SHIPPING:

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
