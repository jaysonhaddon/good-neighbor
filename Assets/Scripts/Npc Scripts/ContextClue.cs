using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    [Header("Context Clue variables")]
    [SerializeField] private bool hasClueAppeared = false;
    [SerializeField] private float deactivateTime;
    [SerializeField] private int contextClueSound;

    // References
    private Animator contextClueAnim;


    // Start is called before the first frame update
    void Start()
    {
        contextClueAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!hasClueAppeared)
            {
                contextClueAnim.SetBool("active", true);
                hasClueAppeared = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("DeactivateContextClue", deactivateTime);
        }
    }

    private void DeactivateContextClue()
    {
        contextClueAnim.SetBool("active", false);
    }

    private void ContextClueSound()
    {
        SoundManager.instance.PlaySoundEffect(contextClueSound);
    }
}
