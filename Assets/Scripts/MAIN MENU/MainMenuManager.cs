using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public TMP_Text loadingText;
    public Slider loadingBar;
    public GameObject globals;
    public int scene;

    public void QuitClick()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    public void OptionsClick()
    {

    }

    public void PlayClick()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    IEnumerator LoadSceneCoroutine()
    {
        loadingScreen.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            loadingBar.value = progress;
            if (async.progress == 0.9f)
            {
                loadingText.text = "Press SPACE to continue.";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    async.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
