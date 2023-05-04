using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isJumpingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool isJumping = animator.GetBool(isJumpingHash);
        bool jumpingPressed = Input.GetKey("space");
        //if player presses spacebar key
        if (!isJumping && jumpingPressed)
        {
            //then set the isJumpingboolean to be True
            animator.SetBool(isJumpingHash, true);
        }
        
        //if player lets go of spacebar key
        if (isJumping && !jumpingPressed)
        {
            //then set the isJumpingboolean to be false
            animator.SetBool(isJumpingHash, false);
        }
    }
}
