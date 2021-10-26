using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region Singleton
    public static AnimationManager instance;
    public void Awake() => instance = this;
    #endregion

    public Animator animator;
    private string curerntState;

    public void ChangeAnimationState(string newState)
    {
        if (curerntState == newState) return;

        animator.Play(newState);
        curerntState = newState;
    }
}
