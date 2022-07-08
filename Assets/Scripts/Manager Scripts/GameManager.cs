using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Game Manager Variables")]
    [SerializeField] private int peopleToHelp;
    [SerializeField] private int peopleHelped;
    [SerializeField] private float levelTime;
    [SerializeField] private bool startCountdown = true;
    [SerializeField] private List<GameObject> levelItems = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UiManager.instance.SetPeopleToHelp(peopleToHelp);
        UiManager.instance.TimerUpdate(levelTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (startCountdown)
        {
            TimeCountdown();
        }
    }

    public void GameStart()
    {
        startCountdown = true;
    }

    private void TimeCountdown()
    {
        levelTime -= Time.deltaTime;
        UiManager.instance.TimerUpdate(levelTime);

        if (levelTime <= 0)
        {
            startCountdown = false;
            levelTime = 0;
            UiManager.instance.TimerUpdate(levelTime);
            UiManager.instance.LoseState();
            Debug.Log("You lost the round!");
        }
    }

    public void IncreaseScore()
    {
        peopleHelped++;
        if (peopleHelped >= peopleToHelp)
        {
            peopleHelped = peopleToHelp;
            startCountdown = false;
            UiManager.instance.WinState();
        }
        UiManager.instance.PeopleHelpedUpdate(peopleHelped);
    }

    public void PauseCountdownTime()
    {
        startCountdown = !startCountdown;
    }

    public void ItemCheck()
    {
        foreach(GameObject item in levelItems)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
            }
        }
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < levelItems.Count; i++)
        {
            ItemPickup itemToRemove = levelItems[i].GetComponent<ItemPickup>();

            if (itemToRemove.GetItemName == itemName)
            {
                levelItems.Remove(itemToRemove.gameObject);
            }
        }
    }

}
