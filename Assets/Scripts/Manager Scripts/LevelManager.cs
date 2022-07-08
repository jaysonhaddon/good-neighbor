using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Manager Variables")]
    [SerializeField] private float waitTime;

    public void StartGame()
    {
        int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ReloadCurrentLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LevelLoading());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private IEnumerator LevelLoading()
    {
        int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        UiManager.instance.FadePanelActivate();
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
