using Dante;
using UnityEngine;

public class CraneAnimationEvents : MonoBehaviour
{
    [SerializeField] protected CraneManager craneManager;

    public void StateMechanic(CraneStates state)
    {
        craneManager.StateMechanic(state);
    }
}
