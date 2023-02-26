using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap_RibMovement : MonoBehaviour
{
    [SerializeField][Range(0f, 4f)] float lerpTime;
    [SerializeField] Vector3 end, start;

    //the ref and the timer
    public FlowerSelection flower;
    float time;
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
            transform.position = Vector3.Lerp(transform.position, end, lerpTime * Time.deltaTime);
            time += Time.deltaTime;
        }
        if (time > 2) { flower.wrapleft = false; time = 0; }
        if (flower.wrapright && transform.position != start && gameObject.tag == "wrapper")
        {
            transform.position = Vector3.Lerp(transform.position, start, lerpTime * Time.deltaTime);
            time += Time.deltaTime;
        }
        if (time > 2) { flower.wrapright = false; time = 0; }
        //RIBBON
        if (flower.ribleft && transform.position != end && gameObject.tag == "ribbon")
        {
            transform.position = Vector3.Lerp(transform.position, end, lerpTime * Time.deltaTime);
            time += Time.deltaTime;
        }
        if (time > 2) { flower.ribleft = false; time = 0; }
        if (flower.ribright && transform.position != start && gameObject.tag == "ribbon")
        {
            transform.position = Vector3.Lerp(transform.position, start, lerpTime * Time.deltaTime);
            time += Time.deltaTime;
        }
        if (time > 2) { flower.ribright = false; time = 0; }
    }
}
