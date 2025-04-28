using UnityEngine;

namespace Dante {
	public class LaserCollisions : MonoBehaviour
	{
        #region References



        #endregion

        #region RuntimeVariables



        #endregion

        #region UnityMethods

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                SendMessageUpwards("ChangeLaserColor");
            }

            if(other.gameObject.name == "WinPlatform")
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                SendMessageUpwards("ChangeLaserColor");
            }

            if (other.gameObject.name == "WinPlatform")
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
        }

        #endregion

        #region PublicMethods



        #endregion

        #region LocalMethods



        #endregion

        #region GettersSetters



        #endregion
    }
}
