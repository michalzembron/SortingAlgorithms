using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    #region Singleton
    public static MainMenuManager instance;
    public void Awake() => instance = this;
    #endregion

    public TMP_Text randomizedValuesText;
    public Button loadLevelButton;

    private void Start()
    {
        RandomizeValues.instance.randomizedValues = SaveManager.instance.LoadRandomizedValues();
        SetRandomizedValuesText();
    }

    void Update()
    {
        if (RandomizeValues.instance.randomizedValues != null)
            loadLevelButton.interactable = true;
    }

    public void SetRandomizedValuesText()
    {
        randomizedValuesText.text = "";
        if (RandomizeValues.instance.randomizedValues != null)
        {
            for (int i = 0; i < RandomizeValues.instance.randomizedValues.Count; i++)
            {
                Debug.Log(RandomizeValues.instance.randomizedValues[i]);
                randomizedValuesText.text += $"{RandomizeValues.instance.randomizedValues[i]} ";
            }
        }
    }

    public void GetNewRandomizedValues()
    {
        RandomizeValues.instance.RandomizeNewValues();
        SetRandomizedValuesText();
    }
}
