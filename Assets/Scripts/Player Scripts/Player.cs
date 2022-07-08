using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Movement Variables")]
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float speedBoost;
    [SerializeField] private bool paused = false;

    [Header("Player Animation Variables")]
    [SerializeField] private Vector2 animDirection;
    [SerializeField] private bool jump = false;
    [SerializeField] private bool wave = false;

    [Header("Player References")]
    [SerializeField] private PlayerItem playerItem;
    [SerializeField] private Animator playerAnim;
    private Rigidbody2D playerRb;

    // Get and Set
    public PlayerItem GetPlayerItem { get { return playerItem; } }
    public float GetCurrentSpeed { get { return moveSpeed; } }
    public float GetDefaultSpeed {  get { return defaultSpeed; } }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        ResetPlayerSpeed();

        if (jump)
        {
            PlayerJumpAnimation();
        }

        if (wave)
        {
            PlayerWaveAnimation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        PlayerAnimations();
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            PlayerMovement();
        }
    }

    private void MovementInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        moveDirection.Normalize();
    }

    private void PlayerMovement()
    {
        playerRb.velocity = moveDirection * moveSpeed;
    }

    private void PlayerAnimations()
    {

        animDirection = new Vector2(Mathf.Round(moveDirection.x), Mathf.Round(moveDirection.y));

        if (moveDirection != Vector2.zero)
        {
            playerAnim.SetFloat("moveX", animDirection.x);
            playerAnim.SetFloat("moveY", animDirection.y);
        }

        playerAnim.SetBool("walking", playerRb.velocity != Vector2.zero);
    }

    public void PlayerWaveAnimation()
    {
        paused = true;
        playerAnim.SetTrigger("wave");
    }

    public void PlayerJumpAnimation()
    {
        paused = true;
        playerAnim.SetTrigger("jump");
    }

    public void IncreasePlayerSpeed()
    {
        moveSpeed += speedBoost;
    }

    public void ResetPlayerSpeed()
    {
        moveSpeed = defaultSpeed;
    }

    public void PausePlayer()
    {
        paused = !paused;
        playerRb.velocity = Vector2.zero;
    }
}
