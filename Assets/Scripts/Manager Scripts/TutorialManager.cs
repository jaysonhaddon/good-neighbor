using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    [Header("Tutorial Manager Variables")]
    [SerializeField] private int index;
    [SerializeField] private string[] tutorialPrompts;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private string[] controlPrompts;
    [SerializeField] private TextMeshProUGUI controlText;
    [SerializeField] private Sprite[] tutorialSprites;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private GameObject startStateUI;
    [SerializeField] private int buttonClickSound;


    private void Start()
    {
        tutorialText.text = tutorialPrompts[index];
        controlText.text = controlPrompts[index];
        tutorialImage.sprite = tutorialSprites[index];
    }

    public void NextButton()
    {
        SoundManager.instance.PlaySoundEffect(buttonClickSound);
        index++;
        if (index > tutorialPrompts.Length - 1)
        {
            startStateUI.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            tutorialText.text = tutorialPrompts[index];
            controlText.text = controlPrompts[index];
            tutorialImage.sprite = tutorialSprites[index];
        }
    }
}
