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

    Vector3 previousFirstItemPos;
    Vector3 previousSecondItemPos;

    void Update()
    {
        if (GameManager.instance.isReadyToMove)
        {
            GameManager.instance.isReadyToMove = false;
            StartCoroutine(ShuffleItems());
        }
        if (isLeft)
            leftTarget.transform.position = leftHand.transform.position;
        if (isRight)
            rightTarget.transform.position = rightHand.transform.position;
    }

    IEnumerator ShuffleItems()
    {
        List<Move> moves = GameManager.instance.moves;
        for (int currentMove = 0; currentMove < moves.Count; currentMove++)
        {
            previousFirstItemPos = moves[currentMove].firstItem.transform.position;
            previousSecondItemPos = moves[currentMove].secondItem.transform.position;

            yield return StartCoroutine(FlyToItem(moves[currentMove].firstItem.transform.position));
            PickUpLeft(moves[currentMove].firstItem);
            yield return new WaitForSeconds(.25f);

            yield return StartCoroutine(FlyToItem(moves[currentMove].secondItem.transform.position));
            PickUpRight(moves[currentMove].secondItem);
            yield return new WaitForSeconds(.25f);
            PlaceLeft(previousSecondItemPos);
            yield return new WaitForSeconds(.25f);

            yield return StartCoroutine(FlyToItem(previousFirstItemPos));
            PlaceRight(previousFirstItemPos);
            yield return new WaitForSeconds(.25f);

            GameManager.instance.stepsCounter++;
            GameManager.instance.stepsCounterText.text = $"Steps: {GameManager.instance.stepsCounter}";
        }
        GameManager.instance.isCompleted = true;
        AudioManager.instance.PlayAudioEffect("successFanfare");
        AnimationManager.instance.ChangeAnimationState("Sort Complete");
        GameManager.instance.sortCompleteEffect.Play();
    }

    IEnumerator FlyToItem(Vector3 itemPos)
    {
        float time = 0.5f;
        Vector3 startingPos = transform.localPosition;
        Vector3 finalPos = new Vector3(itemPos.x, transform.localPosition.y, transform.localPosition.z);
        float elapsedTime = 0;

        while (transform.localPosition.x != finalPos.x)
        {
            if (transform.localPosition.x - finalPos.x > 0)
                AnimationManager.instance.ChangeAnimationState("Move Right");
            else
                AnimationManager.instance.ChangeAnimationState("Move Left");

            transform.localPosition = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(.01f);
        }
        AnimationManager.instance.ChangeAnimationState("Idle");
        yield return new WaitForSeconds(.1f);
    }

    public void PickUpLeft(GameObject target)
    {
        leftTarget = target;
        isLeft = true;
        AudioManager.instance.PlayAudioEffect("boxSliding");
    }

    public void PlaceLeft(Vector3 pos)
    {
        isLeft = false;
        leftTarget.transform.position = pos;
        AudioManager.instance.PlayAudioEffect("boxSliding");
    }

    public void PickUpRight(GameObject target)
    {
        rightTarget = target;
        isRight = true;
        AudioManager.instance.PlayAudioEffect("boxSliding");
    }

    public void PlaceRight(Vector3 pos)
    {
        isRight = false;
        rightTarget.transform.position = pos;
        AudioManager.instance.PlayAudioEffect("boxSliding");
    }
}
