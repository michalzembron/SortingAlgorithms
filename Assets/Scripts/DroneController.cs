using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    #region Singleton
    public static DroneController instance;
    public void Awake() => instance = this;
    #endregion
    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject leftTarget;
    public GameObject rightTarget;

    bool isLeft = false;
    bool isRight = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.instance.isReadyToMove)
        {
            GameManager.instance.isReadyToMove = false;
            StartCoroutine(ShuffleItems());
        }
    }

    IEnumerator ShuffleItems()
    {
        yield return null;
    }

    public void PickUpLeft(GameObject target)
    {
        leftTarget = target;
        isLeft = true;
    }

    public void PlaceLeft(Vector3 pos)
    {
        isLeft = false;
        leftTarget.transform.position = pos;
    }

    public void PickUpRight()
    {

    }
}
