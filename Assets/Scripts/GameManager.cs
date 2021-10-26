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

    public GameObject startButton;
    public TMP_Text selectedAlgorithmText;
    public TMP_Text stepsCounterText;
    public TMP_Text timerText;
    private int algorithmID;
    public int stepsCounter;

    public GameObject itemsToSortSpawn;
    public List<GameObject> itemsToSort = new List<GameObject>();

    private bool needsRepositioning;
    private GameObject firstItem;
    private GameObject secondItem;
    Vector3 firstItemPos;
    Vector3 secondItemPos;

    public bool isCompleted;
    float sortingTimer;
    float itemMovementTimer;

    public List<Move> moves = new List<Move>();
    public bool isReadyToMove = false;
    public bool isMovingItems = false;

    void Start()
    {
        if(AudioManager.instance != null) AudioManager.instance.ChangeBackgroundMusic();

        if (CubeSpawner.instance != null) CubeSpawner.instance.SpawnCubes(RandomizeValues.instance.randomizedValues.Count);

        algorithmID = SaveManager.instance.LoadSelectedSortingAlgorithm();
        switch (algorithmID)
        {
            case 0:
                selectedAlgorithmText.text = "Bubble Sort";
                break;
            case 1:
                selectedAlgorithmText.text = "Insertion Sort";
                break;
            case 2:
                selectedAlgorithmText.text = "Selection Sort";
                break;
            default:
                selectedAlgorithmText.text = "Bubble Sort";
                break;
        }

        for (int i = 0; i < itemsToSortSpawn.transform.childCount; i++)
        {
            itemsToSort.Add(itemsToSortSpawn.transform.GetChild(i).gameObject);
        }

        GetRandomizedValues();
    }

    void Update()
    {
        itemMovementTimer += Time.deltaTime / .5f;

        if (needsRepositioning)
        {
            firstItem.transform.position = Vector3.Lerp(firstItemPos, secondItemPos, itemMovementTimer);
            secondItem.transform.position = Vector3.Lerp(secondItemPos, firstItemPos, itemMovementTimer);
            if (firstItem.transform.position == secondItemPos && secondItem.transform.position == firstItemPos)
            {
                needsRepositioning = false;
            }
        }
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

                    firstItem = itemsToSort[i];
                    secondItem = itemsToSort[i + 1];
                    
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

                firstItem = itemsToSort[j];
                secondItem = itemsToSort[j - 1];

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
                {
                    jMin = j;
                }
            }

            if (jMin != i)
            {
                GameObject itemToSortTemp = itemsToSort[i];

                firstItem = itemsToSort[i];
                secondItem = itemsToSort[jMin];

                moves.Add(new Move(itemsToSort[i], itemsToSort[jMin]));

                itemsToSort[i] = itemsToSort[jMin];
                itemsToSort[jMin] = itemToSortTemp;
            }
        }
        isReadyToMove = true;
    }
}
