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

    public Slider itemsAmountSlider;
    public TMP_Text currentAmountText;
    public TMP_Text randomizedValuesText;
    public Button loadLevelButton;
    private int itemsAmount = 5;

    private void Start()
    {
        AudioManager.instance.ChangeBackgroundMusic();

        RandomizeValues.instance.randomizedValues = SaveManager.instance.LoadRandomizedValues();

        SetRandomizedValuesText();

        var amount = RandomizeValues.instance.randomizedValues.Count;
        itemsAmount = amount;
        itemsAmountSlider.value = amount;
    }

    void Update()
    {
        if (RandomizeValues.instance.randomizedValues != null)
            loadLevelButton.interactable = true;
    }

    public void SetItemsAmount(float itemsAmount)
    {
        this.itemsAmount = (int)itemsAmount;
        currentAmountText.text = itemsAmount.ToString();
    }

    public void SetRandomizedValuesText()
    {
        randomizedValuesText.text = "";
        if (RandomizeValues.instance.randomizedValues != null)
        {
            for (int i = 0; i < RandomizeValues.instance.randomizedValues.Count; i++)
            {
                randomizedValuesText.text += $"{RandomizeValues.instance.randomizedValues[i]} ";
            }
        }
    }

    public void GetNewRandomizedValues()
    {
        RandomizeValues.instance.RandomizeNewValues(itemsAmount);
        SetRandomizedValuesText();
    }
}
