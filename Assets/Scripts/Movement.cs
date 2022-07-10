using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    public CharacterController controller;
    public StateMachine meleeStateMachine;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("groundSpeed", Mathf.Abs(horizontalMove));
        if (Input.GetKeyDown("w"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (meleeStateMachine.CurrentState.GetType() != typeof(IdleCombatState))
        {
            jump = false;
            horizontalMove = horizontalMove * 0.01f;
        }
        controller.Move(horizontalMove, crouch, jump);
        if (jump)
        {
            animator.SetTrigger("jump");
            jump = false;
        }
    }
}
