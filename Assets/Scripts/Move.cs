using UnityEngine;

[System.Serializable]
public class Move
{
    public GameObject firstItem;
    public GameObject secondItem;

    public Move(GameObject firstItem, GameObject secondItem)
    {
        this.firstItem = firstItem;
        this.secondItem = secondItem;
    }
}
