using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class VRRig : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;

    private void FixedUpdate()
    {
        AnimationHandler();

        print(playerMovement.playerState);
    }

    private void AnimationHandler()
    {
        switch(playerMovement.playerState)
        {
            case PlayerMovement.PlayerState.idle:
                animator.SetBool("idle", true);

                animator.SetBool("walking", false);
                animator.SetBool("running", false);
                animator.SetBool("jumping", false);
                animator.SetBool("crouching", false);

                break;

            case PlayerMovement.PlayerState.walking:
                animator.SetBool("walking", true);

                animator.SetBool("idle", false);
                animator.SetBool("running", false);
                animator.SetBool("jumping", false);
                animator.SetBool("crouching", false);

                break;

            case PlayerMovement.PlayerState.running:
                animator.SetBool("running", true);

                animator.SetBool("walking", false);
                animator.SetBool("idle", false);
                animator.SetBool("jumping", false);
                animator.SetBool("crouching", false);

                break;

            case PlayerMovement.PlayerState.jumping:
                animator.SetBool("jumping", true);

                animator.SetBool("walking", false);
                animator.SetBool("running", false);
                animator.SetBool("idle", false);
                animator.SetBool("crouching", false);

                break;

            case PlayerMovement.PlayerState.crouching:
                animator.SetBool("crouching", true);

                animator.SetBool("walking", false);
                animator.SetBool("running", false);
                animator.SetBool("jumping", false);
                animator.SetBool("idle", false);

                break;
        }
    }
}