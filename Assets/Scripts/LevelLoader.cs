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
}
