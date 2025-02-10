using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movements parameters")]
    public float speedMovement = 6.0f;
    public float speedRotation = 250f;
    public float x, y;
    public float jumpPower = 5f;
    public float gravity = -9.81f;

    [Header("Components references")]
    public Animator animator;
    public CharacterController characterController;

    public Transform groundCheckPoint;
    public LayerMask groundMask;
    
    private bool isGrounded;
    private bool isJumping;
    private bool isCrouching;
    private Vector3 velocity;

    private Vector3 move;

    void Update()
    {

        // Applying gravity
        if (!isGrounded)
        {
            if (isJumping)
            {
                isJumping = false;
                animator.SetBool("IsJumping", false);
            }
            velocity.y += gravity * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);
        }
        else if (isJumping)
        {
            isGrounded = false;
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsGrounded", false);
            velocity.y = 0;
            velocity.y += jumpPower;

            characterController.Move(velocity * Time.deltaTime);
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        // Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isGrounded && !isJumping)
            {
                isCrouching = !isCrouching;
                animator.SetBool("IsCrouching", isCrouching);

                Debug.Log(isCrouching);
            }
        }

        // Movement
        y = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");

        move = x * transform.right + y * transform.forward;

        characterController.Move(move * speedMovement * Time.deltaTime);
        transform.Rotate(0, x * Time.deltaTime * speedRotation, 0);

        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Ground") && !isGrounded)
        {
            isGrounded = true;
            animator.SetBool("IsGrounded", true);
        }
    }
}
