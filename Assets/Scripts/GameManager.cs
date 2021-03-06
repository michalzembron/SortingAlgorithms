using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    public void Awake() => instance = this;
    #endregion

    public List<GameObject> itemsToSort = new List<GameObject>();
    public GameObject startButton;
    public GameObject itemsToSortSpawn;
    public TMP_Text selectedAlgorithmText;
    public TMP_Text stepsCounterText;
    public TMP_Text timerText;
    public ParticleSystem sortCompleteEffect;
    public int stepsCounter;
    public bool isCompleted;

    public List<Move> moves = new List<Move>();
    public bool isReadyToMove = false;
    public bool isMovingItems = false;

    private int algorithmID;
    private float sortingTimer;

    void Start()
    {
        if (AudioManager.instance != null && CubeSpawner.instance != null)
        {
            AudioManager.instance.ChangeBackgroundMusic();
            CubeSpawner.instance.SpawnCubes(RandomizeValues.instance.randomizedValues.Count);
            CameraController.instance.AdjustCamera();
        }

        algorithmID = SaveManager.instance.LoadSelectedSortingAlgorithm();

        selectedAlgorithmText.text = algorithmID switch
        {
            0 => "Bubble Sort",
            1 => "Insertion Sort",
            2 => "Selection Sort",
            _ => "Bubble Sort",
        };

        for (int i = 0; i < itemsToSortSpawn.transform.childCount; i++)
        {
            itemsToSort.Add(itemsToSortSpawn.transform.GetChild(i).gameObject);
        }

        if (RandomizeValues.instance != null) GetRandomizedValues();
    }

    void GetRandomizedValues()
    {
        List<int> values = RandomizeValues.instance.randomizedValues;
        for (int i = 0; i < values.Count; i++)
        {
            itemsToSort[i].transform.GetChild(0).GetComponent<TMP_Text>().text = $"{values[i]}";
            itemsToSort[i].GetComponent<ItemToSort>().value = values[i];
        }
    }

    public void StartSorting()
    {
        AudioManager.instance.PlayAudioEffect("buttonFX");
        sortingTimer = 0;
        startButton.SetActive(false);
        StartCoroutine(CountSeconds());
        switch (algorithmID)
        {
            case 0:
                BubbleSort();
                break;
            case 1:
                InsertionSort();
                break;
            case 2:
                SelectionSort();
                break;
            default:
                BubbleSort();
                break;
        }
    }

    IEnumerator CountSeconds()
    {
        while (!isCompleted)
        {
            sortingTimer++;
            timerText.text = $"Time: {sortingTimer}";
            yield return new WaitForSeconds(1);
        }
    }

    private void BubbleSort()
    {
        int n = itemsToSort.Count;
        stepsCounter = 0;
        while (n > 1)
        {
            for (int i = 0; i < n - 1; i++)
            {
                if (itemsToSort[i].GetComponent<ItemToSort>().value > itemsToSort[i + 1].GetComponent<ItemToSort>().value)
                {
                    GameObject itemToSortTemp = itemsToSort[i];
                    
                    moves.Add(new Move(itemsToSort[i], itemsToSort[i + 1]));

                    itemsToSort[i] = itemsToSort[i + 1];
                    itemsToSort[i + 1] = itemToSortTemp;
                }
            }
            n--;
        }
        isReadyToMove = true;
    }

    private void InsertionSort()
    {
        int i = 1;
        stepsCounter = 0;
        while (i < itemsToSort.Count)
        {
            int j = i;
            while (j > 0 && itemsToSort[j - 1].GetComponent<ItemToSort>().value > itemsToSort[j].GetComponent<ItemToSort>().value)
            {
                GameObject itemToSortTemp = itemsToSort[j];

                moves.Add(new Move(itemsToSort[j], itemsToSort[j - 1]));

                itemsToSort[j] = itemsToSort[j - 1];
                itemsToSort[j - 1] = itemToSortTemp;
                j--;
            }
            i++;
        }
        isReadyToMove = true;
    }

    private void SelectionSort()
    {
        int n = itemsToSort.Count;
        stepsCounter = 0;

        for (int i = 0; i < n - 1; i++)
        {
            int jMin = i;
            for (int j = i + 1; j < itemsToSort.Count; j++)
            {
                if (itemsToSort[j].GetComponent<ItemToSort>().value < itemsToSort[jMin].GetComponent<ItemToSort>().value)
                    jMin = j;
            }

            if (jMin != i)
            {
                GameObject itemToSortTemp = itemsToSort[i];

                moves.Add(new Move(itemsToSort[i], itemsToSort[jMin]));

                itemsToSort[i] = itemsToSort[jMin];
                itemsToSort[jMin] = itemToSortTemp;
            }
        }
        isReadyToMove = true;
    }
}
