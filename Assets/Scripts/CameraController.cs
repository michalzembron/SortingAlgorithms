using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Singleton
    public static CameraController instance;
    public void Awake() => instance = this;
    #endregion

    public Vector3[] cameraPositions;

    public void AdjustCamera()
    {
        int randomizedValuesAmount = RandomizeValues.instance.randomizedValues.Count;
        if (randomizedValuesAmount >= 15)
        {
            transform.position = cameraPositions[0];
        }
        else if (randomizedValuesAmount >= 10)
        {
            transform.position = cameraPositions[1];
        }
        else if (randomizedValuesAmount >= 5)
        {
            transform.position = cameraPositions[2];
        }
    }
}
