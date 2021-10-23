using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text selectedAlgorithmText;
    public TMP_Text stepsCounterText;
    private int algorithmID;
    private int stepsCounter;

    public GameObject itemsToSortSpawn;
    public List<GameObject> itemsToSort = new List<GameObject>();

    private bool needsRepositioning;
    private GameObject firstItem;
    private GameObject secondItem;
    Vector3 firstItemPos;
    Vector3 secondItemPos;

    float t;

    void Start()
    {
        CubeSpawner.instance.SpawnCubes(RandomizeValues.instance.randomizedValues.Count);

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
        t += Time.deltaTime / .5f;

        if (needsRepositioning)
        {
            firstItem.transform.position = Vector3.Lerp(firstItemPos, secondItemPos, t);
            secondItem.transform.position = Vector3.Lerp(secondItemPos, firstItemPos, t);
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
        switch (algorithmID)
        {
            case 0:
                Debug.Log("Bubble Sort");
                StartCoroutine(BubbleSort());
                break;
            case 1:
                Debug.Log("Insertion Sort");
                StartCoroutine(InsertionSort());
                break;
            case 2:
                Debug.Log("Selection Sort");
                StartCoroutine(SelectionSort());
                break;
            default:
                Debug.Log("Bubble Sort");
                StartCoroutine(BubbleSort());
                break;
        }
    }

    IEnumerator BubbleSort()
    {
        int n = itemsToSort.Count;
        stepsCounter = 0;
        while (n > 1)
        {
            for (int i = 0; i < n-1; i++)
            {
                if (itemsToSort[i].GetComponent<ItemToSort>().value > itemsToSort[i + 1].GetComponent<ItemToSort>().value)
                {
                    GameObject itemToSortTemp = itemsToSort[i];

                    firstItem = itemsToSort[i];
                    secondItem = itemsToSort[i + 1];
                    firstItemPos = firstItem.transform.position;
                    secondItemPos = secondItem.transform.position;
                    t = 0;

                    needsRepositioning = true;

                    itemsToSort[i] = itemsToSort[i + 1];
                    itemsToSort[i + 1] = itemToSortTemp;

                    stepsCounter++;
                    stepsCounterText.text = $"Steps: {stepsCounter}";

                    yield return new WaitForSeconds(.6f);
                }
            }
            n--;
        }
    }

    IEnumerator InsertionSort()
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
                firstItemPos = firstItem.transform.position;
                secondItemPos = secondItem.transform.position;
                t = 0;

                needsRepositioning = true;

                itemsToSort[j] = itemsToSort[j - 1];
                itemsToSort[j - 1] = itemToSortTemp;
                j--;

                stepsCounter++;
                stepsCounterText.text = $"Steps: {stepsCounter}";

                yield return new WaitForSeconds(.6f);
            }
            i++;
        }
    }

    IEnumerator SelectionSort()
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
                firstItemPos = firstItem.transform.position;
                secondItemPos = secondItem.transform.position;
                t = 0;

                needsRepositioning = true;

                itemsToSort[i] = itemsToSort[jMin];
                itemsToSort[jMin] = itemToSortTemp;

                stepsCounter++;
                stepsCounterText.text = $"Steps: {stepsCounter}";

                yield return new WaitForSeconds(.6f);
            }
        }
    }
}
