using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{

    public static UiManager instance;

    [Header("Level UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI peopleHelpedText;
    [SerializeField] private TextMeshProUGUI peopleToHelpText;
    [SerializeField] private GameObject interactionText;
    [SerializeField] private GameObject speedBoostObject;
    [SerializeField] private TextMeshProUGUI speedBoostText;
    [SerializeField] private string speedBoostActive;
    [SerializeField] private string speedBoostReset;
    [SerializeField] private float activeTime;
    [SerializeField] private int buttonClickSound;

    [Header("Pause Menu UI")]
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private bool canPause = true;
    [SerializeField] private bool gamePaused = false;

    [Header("Options Menu UI")]
    [SerializeField] private GameObject optionsMenuCanvas;

    [Header("Start Sate UI")]
    [SerializeField] private GameObject startStateCanvas;

    [Header("Lose State UI")]
    [SerializeField] private GameObject loseStateCanvas;
    [SerializeField] private int loseStateSound;

    [Header("Win State UI")]
    [SerializeField] private GameObject winStateCanvas;
    [SerializeField] private int winStateSound;

    [Header("Credits Menu UI")]
    [SerializeField] private GameObject creditsMenuCanvas;

    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private Animator fadePanelAnim;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (!gamePaused)
            {
                PauseGame();
            }
            else if (gamePaused)
            {
                ResumeGame();
            }
        }
    }

    public void StartStateButton()
    {
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        player.PausePlayer();
        GameManager.instance.GameStart();
        startStateCanvas.SetActive(false);
    }

    public void LoseState()
    {
        canPause = false;
        player.PausePlayer();
        DisableSpeedBoostUI();
        SoundManager.instance.PlaySoundEffect(loseStateSound);
        loseStateCanvas.SetActive(true);
    }

    public void WinState()
    {
        canPause = false;
        player.PausePlayer();
        DisableSpeedBoostUI();
        SoundManager.instance.PlaySoundEffect(winStateSound);
        winStateCanvas.SetActive(true);
    }

    public void PauseGame()
    {
        canPause = false;
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        pauseMenuCanvas.SetActive(true);
        gamePaused = true;
        player.PausePlayer();
        GameManager.instance.PauseCountdownTime();
    }

    public void ResumeGame()
    {
        canPause = true;
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        pauseMenuCanvas.SetActive(false);
        gamePaused = false;
        player.PausePlayer();
        GameManager.instance.PauseCountdownTime();
    }

    public void ActivateInteractionText()
    {
        interactionText.SetActive(true);
    }

    public void DeactivateInteractionText()
    {
        interactionText.SetActive(false);
    }

    public void SpeedBoostActivate()
    {
        speedBoostText.text = speedBoostActive;
        speedBoostObject.SetActive(true);
        Invoke("DisableSpeedBoostUI", activeTime);
    }

    public void SpeedBoostReset()
    {
        speedBoostText.text = speedBoostReset;
        speedBoostObject.SetActive(true);
        Invoke("DisableSpeedBoostUI", activeTime);
    }

    private void DisableSpeedBoostUI()
    {
        speedBoostObject.SetActive(false);
    }

    public void OptionsButton()
    {
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        pauseMenuCanvas.SetActive(false);
        optionsMenuCanvas.SetActive(true);
    }

    public void MainMenuOptionsButton()
    {
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        optionsMenuCanvas.SetActive(true);
    }

    public void CreditsButton()
    {
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        optionsMenuCanvas.SetActive(false);
        creditsMenuCanvas.SetActive(true);
    }

    public void EndCreditsButton()
    {
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        creditsMenuCanvas.SetActive(true);
    }

    public void DisableEndCredits()
    {
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        creditsMenuCanvas.SetActive(false);
    }

    public void DisableCredits()
    {
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        creditsMenuCanvas.SetActive(false);
        optionsMenuCanvas.SetActive(true);
    }

    public void DisableMainMenuOptions()
    {
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        optionsMenuCanvas.SetActive(false);
    }

    public void BackButton()
    {
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        optionsMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }

    public void QuitButton()
    {
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        Application.Quit();
    }

    public void FadePanelActivate()
    {
        fadePanelAnim.SetTrigger("fade");
    }

    public void PeopleHelpedUpdate(int newNumber)
    {
        peopleHelpedText.text = newNumber.ToString();
    }

    public void SetPeopleToHelp(int newNumber)
    {
        peopleToHelpText.text = newNumber.ToString();
    }

    public void TimerUpdate(float newNumber)
    {
        float roundedNumber = Mathf.FloorToInt(newNumber);
        timerText.text = roundedNumber.ToString();
    }
}
