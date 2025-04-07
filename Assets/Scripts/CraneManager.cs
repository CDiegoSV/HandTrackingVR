using UnityEngine;
using UnityEngine.XR.Content.Interaction;

namespace Dante {
	public class CraneManager : MonoBehaviour
	{
		#region References

		[SerializeField] protected XRJoystick _joystick;
		//[SerializeField] protected XRLever _xRLever;
        [SerializeField] protected Rigidbody _rigidbody;

        #endregion

        #region Knobs

        [SerializeField] protected float moveSpeed;

        #endregion

        #region RuntimeVariables

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
            MoveCrane(_input);
        }

        #endregion

        #region PublicMethods

        

        public void RemoveAllJoystickListeners()
        {
            _joystick.onValueChangeX.RemoveAllListeners();
            _joystick.onValueChangeY.RemoveAllListeners();
        }

        public void GetJoystickListeners()
        {
            _joystick.onValueChangeX.AddListener(MoveCraneX);
            _joystick.onValueChangeY.AddListener(MoveCraneZ);
        }

        #endregion

        #region LocalMethods



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
