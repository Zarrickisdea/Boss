using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveVector;
    private bool isJumping;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        playerInput.onActionTriggered -= OnActionTriggered;
    }

    private void OnEnable()
    {
        playerInput.onActionTriggered += OnActionTriggered;
    }

    public void OnActionTriggered(InputAction.CallbackContext value)
    {
        if (value.action.name == "Move")
        {
            moveVector = value.ReadValue<Vector2>();
        }
        else if (value.action.name == "Jump")
        {
            if (value.performed)
            {
                isJumping = true;
                animator.SetTrigger("Jumped");
            }
            else if (value.canceled)
            {
                isJumping = false;
                animator.ResetTrigger("Jumped");
            }
        }
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        bool isWalking = moveVector.magnitude > 0;
        animator.SetBool("isWalking", isWalking);
    }

    private void FixedUpdate()
    {
        // Move horizontally
        if (moveVector.magnitude > 0)
        {
            Vector2 movement = moveVector * moveSpeed * Time.fixedDeltaTime;
            transform.position += new Vector3(movement.x, movement.y, 0f);
        }

        // Jump
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }
    }
}
