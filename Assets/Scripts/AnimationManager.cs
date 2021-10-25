using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region Singleton
    public static AnimationManager instance;
    public void Awake() => instance = this;
    #endregion

    public Animator animator;
    private string curerntState;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAnimationState(string newState)
    {
        if (curerntState == newState) return;

        animator.Play(newState);

        curerntState = newState;
    }
}
