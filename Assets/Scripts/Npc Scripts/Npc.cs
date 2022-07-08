using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{

    [Header("Npc Movement Variables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDistance;
    [SerializeField] private Vector2 homePosition;
    [SerializeField] private Vector2 currentDirection;
    [SerializeField] private Vector2 animationDirection;
    [SerializeField] private Vector2 negativeDirection;
    [SerializeField] private bool shouldMove = true;
    [SerializeField] private bool flipDirection = false;
    [SerializeField] private float wanderTime;
    [SerializeField] private float startWanderTime;
    [SerializeField] private float waitTime;
    [SerializeField] private float startWaitTime;

    [Header("Npc Item Variables")]
    [SerializeField] private string itemWanted;
    [SerializeField] private bool itemReceived = false;
    [SerializeField] private bool canInteract = false;

    [Header("Npc Animation Variables")]
    [SerializeField] private GameObject confettiParticle;
    [SerializeField] private bool jump = false;
    [SerializeField] private bool wave = false;

    [Header("Npc Sound Variables")]
    [SerializeField] private int itemDeliveredSound;
    [SerializeField] private int itemIncorrectSound;

    [Header("Npc References")]
    [SerializeField] private Animator npcAnim;
    [SerializeField] private GameObject contextClue;
    private Rigidbody2D npcRb;
    private Player thePlayer;

    private void Awake()
    {
        npcRb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        homePosition = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        wanderTime = startWanderTime;
        waitTime = startWaitTime;
        DirectionRoll();

        if (jump)
        {
            NpcJumpAnimation();
        }

        if (wave)
        {
            NpcWaveAnimation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove && !itemReceived)
        {
            NpcWander();
            NpcMovement();
        }

        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (!itemReceived)
            {
                PlayerInteraction();
            }
        }

        NpcAnimation();
    }

    private void NpcAnimation()
    {
        npcAnim.SetFloat("moveX", animationDirection.x);
        npcAnim.SetFloat("moveY", animationDirection.y);
    }

    private void NpcJumpAnimation()
    {
        shouldMove = false;
        contextClue.SetActive(false);
        npcAnim.SetTrigger("jump");
    }

    private void NpcWaveAnimation()
    {
        shouldMove = false;
        contextClue.SetActive(false);
        npcAnim.SetTrigger("wave");
    }
    private void NpcMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentDirection, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shouldMove = false;
            npcAnim.SetBool("walking", false);
            npcRb.bodyType = RigidbodyType2D.Static;
            thePlayer = other.GetComponent<Player>();
            UiManager.instance.ActivateInteractionText();
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !itemReceived)
        {
            shouldMove = true;
            npcAnim.SetBool("walking", true);
            npcRb.bodyType = RigidbodyType2D.Dynamic;
            UiManager.instance.DeactivateInteractionText();
            thePlayer = null;
            canInteract = false;
        }
    }

    private void PlayerInteraction()
    {

        if (thePlayer.GetPlayerItem.PlayerItemCheck)
        {
            if (itemWanted == thePlayer.GetPlayerItem.CurrentItemName)
            {
                itemReceived = true;
                SoundManager.instance.PlaySoundEffect(itemDeliveredSound);
                UiManager.instance.DeactivateInteractionText();
                NpcJumpAnimation();
                thePlayer.GetPlayerItem.SetNoItem();
                thePlayer.IncreasePlayerSpeed();
                Instantiate(confettiParticle, contextClue.transform.position, Quaternion.identity);
                UiManager.instance.SpeedBoostActivate();
                GameManager.instance.IncreaseScore();
                GameManager.instance.RemoveItem(itemWanted);
            }
            else if (itemWanted != thePlayer.GetPlayerItem.CurrentItemName)
            {
                SoundManager.instance.PlaySoundEffect(itemIncorrectSound);

                if (thePlayer.GetCurrentSpeed != thePlayer.GetDefaultSpeed)
                {
                    UiManager.instance.SpeedBoostReset();
                    thePlayer.ResetPlayerSpeed();
                }
            }
        }
        else
        {
            SoundManager.instance.PlaySoundEffect(itemIncorrectSound);
        }
    }

    private void NpcWander()
    {
        npcAnim.SetBool("walking", true);
        wanderTime -= Time.deltaTime;

        if (wanderTime <= 0)
        {
            currentDirection = homePosition;

            if (!flipDirection)
            {
                animationDirection *= negativeDirection;
                flipDirection = true;
            }

            if (Vector2.Distance(homePosition, transform.position) <= 0.1)
            {
                waitTime -= Time.deltaTime;
                npcAnim.SetBool("walking", false);

                if (waitTime <= 0)
                {
                    DirectionRoll();
                    wanderTime = startWanderTime;
                    waitTime = startWaitTime;
                    npcAnim.SetBool("walking", true);
                    flipDirection = false;
                }
            }
        }
    }

    private void DirectionRoll()
    {
        int newRoll = Random.Range(1, 4);

        if (newRoll == 1)
        {
            currentDirection = Vector2.right;
        }
        else if (newRoll == 2)
        {
            currentDirection = Vector2.left;
        }
        else if (newRoll == 3)
        {
            currentDirection = Vector2.up;
        }
        else if (newRoll == 4)
        {
            currentDirection = Vector2.down;
        }
        animationDirection = currentDirection;
        currentDirection = new Vector2(transform.position.x + (currentDirection.x * moveDistance), 
            transform.position.y + (currentDirection.y * moveDistance));
    }

}
