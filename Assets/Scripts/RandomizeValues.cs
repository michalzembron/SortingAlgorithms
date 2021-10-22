using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomizeValues : MonoBehaviour
{
    #region Singleton
    public static RandomizeValues instance;
    public void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public List<int> randomizedValues;

    [Range(2, 20)]
    public int amount;

    [Range(1, 100)]
    public int maxValue;

    public void RandomizeNewValues()
    {
        randomizedValues.Clear();
        for (int i = 0; i < amount; i++)
        {
            int randomizedValue = Random.Range(0, maxValue);
            randomizedValues.Add(randomizedValue);
        }
        SaveManager.instance.SaveRandomizedValues(randomizedValues);
        MainMenuManager.instance.SetRandomizedValuesText();
    }
}
