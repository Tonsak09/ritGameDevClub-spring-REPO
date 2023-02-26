using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap_RibMovement : MonoBehaviour
{
    [SerializeField][Range(0f, 4f)] float speed;
    [SerializeField] Vector3 end, start;
    [SerializeField] AnimationCurve curve;

    //the ref and the timer
    public FlowerSelection flower;
    float time;

    private Coroutine moveCo;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    void move()
    {
        //WRAPPER
        if (flower.wrapleft && transform.position != end && gameObject.tag=="wrapper")
        {
            if(moveCo == null)
            {
                moveCo = StartCoroutine(MoveToTarget(start, end));
                flower.wrapleft = false;
            }

            /*transform.position = Vector3.Lerp(transform.position, end, lerpTime * Time.deltaTime);
            time += Time.deltaTime;*/
        }
        if (time > 2) { flower.wrapleft = false; time = 0; }


        if (flower.wrapright && transform.position != start && gameObject.tag == "wrapper")
        {
            if (moveCo == null)
            {
                moveCo = StartCoroutine(MoveToTarget(end, start));
                flower.wrapright = false;
            }

            /*transform.position = Vector3.Lerp(transform.position, start, speed * Time.deltaTime);
            time += Time.deltaTime;*/
        }
        if (time > 2) { flower.wrapright = false; time = 0; }


        //RIBBON
        if (flower.ribleft && transform.position != end && gameObject.tag == "ribbon")
        {
            if (moveCo == null)
            {
                moveCo = StartCoroutine(MoveToTarget(start, end));
                flower.ribleft = false; // Ihave no idea why this makes this work 
            }
        }
        if (time > 2) 
        { 
            flower.ribleft = false; time = 0; 
        }
        if (flower.ribright && transform.position != start && gameObject.tag == "ribbon")
        {
            if (moveCo == null)
            {
                moveCo = StartCoroutine(MoveToTarget(end, start));
                flower.ribright = false;
            }

            /*transform.position = Vector3.Lerp(transform.position, start, speed * Time.deltaTime);
            time += Time.deltaTime;*/
        }
        if (time > 2) { flower.ribright = false; time = 0; }
    }

    private IEnumerator MoveToTarget(Vector3 start, Vector3 target)
    {
        float lerp = 0; 

        while(lerp <= 1)
        {
            transform.position = Vector3.Lerp(start, target, lerp);

            lerp += Time.deltaTime * speed;
            yield return null;
        }

        moveCo = null;
        time = 3;
    }
}
