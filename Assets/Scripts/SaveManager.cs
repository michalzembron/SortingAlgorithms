using UnityEngine;
using TMPro;

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
}
