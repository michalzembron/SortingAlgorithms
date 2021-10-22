using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    public static SaveManager instance;
    public void Awake() => instance = this;
    #endregion

    public TMP_Dropdown dropDown;

    public void SaveSelectedSortingAlgorithm()
    {
        PlayerPrefs.SetInt("algorithmID", dropDown.value);
    }

    public int LoadSelectedSortingAlgorithm()
    {
        return PlayerPrefs.GetInt("algorithmID");
    }

    public void SaveRandomizedValues(List<int> randomizedValues)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < randomizedValues.Count; i++)
        {
            if (i == randomizedValues.Count - 1)
                builder.Append(randomizedValues[i]);
            else
                builder.Append(randomizedValues[i]).Append(",");
        }
        Debug.Log(builder.ToString());
        PlayerPrefs.SetString("randomizedValues", builder.ToString());
    }

    public List<int> LoadRandomizedValues()
    {
        string loadedData = PlayerPrefs.GetString("randomizedValues");
        List<int> randomizedValues = new List<int>();
        if (loadedData != null && !loadedData.Equals(""))
        {
            randomizedValues = loadedData.Split(',').Select(Int32.Parse).ToList();
        }

        return randomizedValues;
    }
}
