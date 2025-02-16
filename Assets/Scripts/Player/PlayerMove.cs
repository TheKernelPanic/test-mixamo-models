using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [Header("Movements parameters")]
        public float speedMovement = 2.5f;
        public float jumpPower = 5f;
        public float gravity = -9.81f;

        [Header("Components references")]
        public Animator animator;
        public CharacterController characterController;
    
        private float _x, _y;
        private bool _isGrounded;
        private bool _isJumping;
        private bool _isCrouching;
        private bool _isSprinting;
        private Vector3 _velocity;
        private Vector3 _movement;
        
        void Update()
        {
            // Applying gravity
            if (!_isGrounded)
            {
                if (_isJumping)
                {
                    _isJumping = false;
                    animator.SetBool("IsJumping", false);
                }
                _velocity.y += gravity * Time.deltaTime;

                characterController.Move(_velocity * Time.deltaTime);
            }
            else if (_isJumping)
            {
                _isGrounded = false;
                animator.SetBool("IsJumping", true);
                animator.SetBool("IsGrounded", false);
                _velocity.y = 0;
                _velocity.y += jumpPower;

                characterController.Move(_velocity * Time.deltaTime);
            }

            // Jump
            if (Input.GetButtonDown("Jump") && !_isCrouching)
            {
                _isJumping = true;
            }

            // Crouching
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (_isGrounded && !_isJumping)
                {
                    _isCrouching = !_isCrouching;
                    animator.SetBool("IsCrouching", _isCrouching);
                }
            }
            
            // Sprinting
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (_isCrouching)
                {
                    _isCrouching = false;
                    animator.SetBool("IsCrouching", _isCrouching);
                }

                if (!_isJumping)
                {
                    _isSprinting = true;
                }
            }
            else
            {
                _isSprinting = false;
            }
            animator.SetBool("IsSprinting", _isSprinting);

            // Movement
            _y = Input.GetAxis("Vertical");
            _x = Input.GetAxis("Horizontal");

            UpdateMovement();
        
            animator.SetFloat("VelX", _x);
            animator.SetFloat("VelY", _y);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.transform.CompareTag("Ground") && !_isGrounded)
            {
                _isGrounded = true;
                animator.SetBool("IsGrounded", true);
            }
        }
        
        private void UpdateMovement()
        {
            var direction = (_x * transform.right + _y * transform.forward).normalized;
            _velocity.y += Physics.gravity.y * Time.deltaTime;

            var speed = speedMovement;
            if (_isGrounded)
            {
                speed -= 1f;
            }
            if (_isSprinting)
            {
                speed += 2f;
            }
            characterController.Move((direction * speed + _velocity) * Time.deltaTime);
        }
    }
}


