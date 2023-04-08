using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableWobble : Clickable
{
    [SerializeField] Transform objToWobble;
    [SerializeField] float angleChange;
    [SerializeField] float speedTo;
    [SerializeField] float speedBack;
    [SerializeField] AnimationCurve curveTo;
    [SerializeField] AnimationCurve curveBack;
    private bool isWobbling = false;

    public override void Click()
    {
        if(!isWobbling)
        {
            StartCoroutine(Wobble());
            isWobbling = true;
        }
    }

    private IEnumerator Wobble()
    {
        float lerp = 0;
        float holdX = objToWobble.eulerAngles.x;

        while(lerp <= 1)
        {
            objToWobble.eulerAngles = new Vector3(Mathf.LerpUnclamped(holdX, holdX + angleChange, curveTo.Evaluate(lerp)), objToWobble.eulerAngles.y, objToWobble.eulerAngles.z);

            lerp += Time.deltaTime * speedTo;
            yield return null;
        }

        lerp = 0;
        while (lerp <= 1)
        {
            objToWobble.eulerAngles = new Vector3(Mathf.LerpUnclamped(holdX + angleChange, holdX, curveBack.Evaluate(lerp)), objToWobble.eulerAngles.y, objToWobble.eulerAngles.z);

            lerp += Time.deltaTime * speedBack;
            yield return null;
        }

        // Set back to original 
        objToWobble.eulerAngles = new Vector3(Mathf.LerpUnclamped(holdX, holdX + angleChange, 0), objToWobble.eulerAngles.y, objToWobble.eulerAngles.z);

        // Cleanup
        isWobbling = false;
    }
}
