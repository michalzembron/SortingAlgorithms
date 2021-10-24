using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public Slider loadingProgressSlider;
    public TMP_Text loadingProgressText;

    public void LoadLevel(int levelID)
    {
        AudioManager.instance.PlayAudioEffect("buttonFX");
        loadingProgressSlider.gameObject.SetActive(true);
        StartCoroutine(LoadAsync(levelID));
    }

    IEnumerator LoadAsync(int levelID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelID);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingProgressSlider.value = progress;
            loadingProgressText.text = (progress * 100f).ToString("F0") + "%";
            yield return null;
        }
    }

    public void QuitGame()
    {
        AudioManager.instance.PlayAudioEffect("buttonFX");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
