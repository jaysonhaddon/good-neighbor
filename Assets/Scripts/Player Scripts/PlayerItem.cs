using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{

    [Header("Player Item Variables")]
    [SerializeField] private Sprite currentItemSprite;
    [SerializeField] private string currentItemName;
    [SerializeField] private bool hasItem = false;

    [Header("Player Item References")]
    private SpriteRenderer playerItemSr;

    // Get and Set
    public Sprite CurrentItemSprite { get { return currentItemSprite; } }
    public string CurrentItemName { get { return currentItemName; } }
    public bool PlayerItemCheck { get { return hasItem; } }

    // Start is called before the first frame update
    void Start()
    {
        playerItemSr = GetComponent<SpriteRenderer>();
    }

    public void SetNewItem(Sprite newSprite, string newName)
    {
        currentItemSprite = newSprite;
        currentItemName = newName;
        playerItemSr.sprite = currentItemSprite;
        hasItem = true;
    }

    public void SetNoItem()
    {
        currentItemName = null;
        currentItemSprite = null;
        playerItemSr.sprite = null;
        hasItem = false;
    }
}
