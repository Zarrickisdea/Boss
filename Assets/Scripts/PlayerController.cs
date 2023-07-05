using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveVector;
    private bool isJumping;
    private bool isGrounded;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private string nextScene;

    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        switch (value.action.name)
        {
            case "Move":
                moveVector = value.ReadValue<Vector2>();
                break;

            case "Jump":
                if (value.performed && isGrounded)
                {
                    isJumping = true;
                    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                }
                break;

            case "Pause":
                bool isMenuActive = !pauseMenu.activeSelf;
                pauseMenu.SetActive(isMenuActive);
                Time.timeScale = isMenuActive ? 0f : 1f;
                break;

            default:
                break;
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM(AudioManager.BackgroundSound.Gameplay);
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isWalking", moveVector.magnitude > 0);

        animator.SetFloat("JumpMagnitude", Mathf.Sign(rb.velocity.y));
    }

    private void FixedUpdate()
    {
        if (moveVector.magnitude > 0)
        {
            Vector2 movement = moveVector * moveSpeed * Time.fixedDeltaTime;

            Vector3 newPosition = transform.position + new Vector3(movement.x, movement.y, 0f);

            Vector3 cameraBoundsMin = mainCamera.ViewportToWorldPoint(Vector3.zero);
            Vector3 cameraBoundsMax = mainCamera.ViewportToWorldPoint(Vector3.one);
            newPosition.x = Mathf.Clamp(newPosition.x, cameraBoundsMin.x, cameraBoundsMax.x);
            newPosition.y = Mathf.Clamp(newPosition.y, cameraBoundsMin.y, cameraBoundsMax.y);

            transform.position = newPosition;

            if (movement.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movement.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        if (isJumping && isGrounded)
        {
            isJumping = false;
        }

        //Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);
    }

    public void Burn()
    {
        SceneManager.LoadScene("GameOver");
        AudioManager.Instance.StopMusic();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Endpoint":
                SceneManager.LoadScene(nextScene);
                break;
            case "Triggerman":
                collision.gameObject.GetComponent<TriggerScript>().SwitchBlockers();
                break;
            case "Triggerpeople":
                collision.gameObject.GetComponent<TriggerScript>().ErraticBlockers();
                break;
            default:
                break;
        }

        AudioManager.Instance.PlayEffect(AudioManager.Effects.Switch);
    }
}
