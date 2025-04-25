using System.Collections;
using UnityEngine;

namespace Dante {
	public class WinTrigger : MonoBehaviour
	{
        #region References

        [SerializeField] protected Transform ballDropPosition;
        [SerializeField] protected float secondsToWaitAnimation;


        #endregion

        #region RuntimeVariables



        #endregion

        #region UnityMethods

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                other.gameObject.transform.SetPositionAndRotation(ballDropPosition.position,ballDropPosition.rotation);
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation|RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezePositionZ;
                StartCoroutine(AnimationCoroutine(other.gameObject, secondsToWaitAnimation));
            }
        }

        #endregion

        #region PublicMethods



        #endregion

        #region LocalMethods

        protected IEnumerator AnimationCoroutine(GameObject ballGameObject, float secondsToWait)
        {
            yield return new WaitForSeconds(secondsToWait);
            ballGameObject.transform.GetChild(0).GetComponent<Animator>().enabled = true;
        }

        #endregion

        #region GettersSetters



        #endregion
    }
}
