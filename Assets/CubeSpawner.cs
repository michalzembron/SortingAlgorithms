using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    #region Singleton
    public static CubeSpawner instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public GameObject itemSpawnerParent;
    public GameObject itemToSpawnPrefab;
    public Vector3[] itemSpawnPos;

    void Start()
    {
        //SpawnCubes(20);
        //SpawnCubes(9);
    }

    private void CalculatePositions(int cubeAmount)
    {
        if (cubeAmount % 2 == 0)
        {
            // Even number
            // Get two middle elements
            int middleLeftElement = Mathf.FloorToInt(cubeAmount / 2.0f);
            int middleRightElement = middleLeftElement + 1;

            itemSpawnPos = new Vector3[cubeAmount];
            itemSpawnPos[middleLeftElement - 1] = new Vector3(-1f, 0.5f, 0);
            itemSpawnPos[middleRightElement - 1] = new Vector3(1f, 0.5f, 0);

            for (int i = 0; i < middleLeftElement - 1; i++)
            {
                itemSpawnPos[i] = new Vector3(-(((middleLeftElement - 1) * 2) - i * 2) - 1, 0.5f, 0);
                itemSpawnPos[middleRightElement + i] = new Vector3(2 * (i + 1) + 1, 0.5f, 0);
            }
        }
        else
        {
            // Odd number
            // Get the middle element
            int middleElement = Mathf.CeilToInt(cubeAmount / 2.0f);

            itemSpawnPos = new Vector3[cubeAmount];
            itemSpawnPos[middleElement - 1] = new Vector3(0, 0.5f, 0);

            for (int i = 0; i < middleElement - 1; i++)
            {
                itemSpawnPos[i] = new Vector3(-(((middleElement - 1) * 2) - i * 2), 0.5f, 0);
                itemSpawnPos[middleElement + i] = new Vector3(2 * (i + 1), 0.5f, 0);
            }
        }
    }

    public void SpawnCubes(int cubeAmount)
    {
        CalculatePositions(cubeAmount);
        for (int i = 0; i < cubeAmount; i++)
        {
            Instantiate(itemToSpawnPrefab, itemSpawnPos[i], itemToSpawnPrefab.transform.rotation, itemSpawnerParent.transform);
        }
    }
}
