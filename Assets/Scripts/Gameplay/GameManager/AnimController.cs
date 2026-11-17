using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] GameObject mario;

    Animator marioAnimator;
    MarioController marioMovement;

    void Start()
    {
        marioAnimator = mario.GetComponent<Animator>();
        marioMovement = mario.GetComponent<MarioController>();
    }

    void Update()
    {
        if (marioMovement.GetIsRunning())
        {
            marioAnimator.SetBool("isWalking", true);
        }
        else
        {
            marioAnimator.SetBool("isWalking", false);
        }

        if (marioMovement.GetIsDead())
        {
            marioAnimator.SetBool("isDead", true);
        }
        else
        {
            marioAnimator.SetBool("isDead", false);
        }

        if (marioMovement.GetIsGrounded())
        {
            marioAnimator.SetBool("isJumping", false);
        }
        else
        {
            marioAnimator.SetBool("isJumping", true);
        }

        if (marioMovement.GetIsBig())
        {
            marioAnimator.SetBool("isBig", true);
        }
        else
        {
            marioAnimator.SetBool("isBig", false);
        }

    }
}