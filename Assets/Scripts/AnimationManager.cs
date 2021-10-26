using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region Singleton
    public static AnimationManager instance;
    public void Awake() => instance = this;
    #endregion

    public Animator animator;
    private string currentState;

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
}
