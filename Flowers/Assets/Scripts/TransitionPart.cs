using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPart : MonoBehaviour
{
    [SerializeField] Vector3 targetOffset;
    [Header("Animation Settings")]
    [SerializeField] float speed;
    [SerializeField] AnimationCurve curveOut;
    [SerializeField] AnimationCurve curveIn;

    private Vector3 holdPosition;
    private bool transitioning;
    private bool isOut; // Whether in the shop or out of shop

    private void Start()
    {
        holdPosition = this.transform.position;

        isOut = true;
    }

    /// <summary>
    /// If not already transitioning this object will move to 
    /// its opposite transition state 
    /// </summary>
    public void TryTransition()
    {
        if(!transitioning)
        {
            transitioning = true;
            
            if(isOut)
            {
                StartCoroutine(TranstionOut());
            }
            else
            {
                StartCoroutine(TranstionIn());
            }

            isOut = !isOut;
        }
    }

    private IEnumerator TranstionOut()
    {
        float lerp = 0; 

        while (lerp <= 1)
        {
            this.transform.position = Vector3.Lerp(holdPosition, holdPosition + targetOffset, curveOut.Evaluate(lerp));

            lerp += Time.deltaTime * speed;
            yield return null;
        }

        transitioning = false;
    }

    private IEnumerator TranstionIn()
    {
        float lerp = 0;

        while (lerp <= 1)
        {
            this.transform.position = Vector3.Lerp(holdPosition + targetOffset, holdPosition, curveIn.Evaluate(lerp));
             
            lerp += Time.deltaTime * speed;
            yield return null;
        }

        transitioning = false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 target;

        // Allows gizmos to be drawn in both in editor and in play
        if(Application.isPlaying)
        {
            target = holdPosition + targetOffset;
            Gizmos.DrawLine(holdPosition, target);
        }
        else
        {
            target = this.transform.position + targetOffset;
            Gizmos.DrawLine(this.transform.position, target);
        }

        Gizmos.DrawSphere(target, 0.1f);

    }
}
