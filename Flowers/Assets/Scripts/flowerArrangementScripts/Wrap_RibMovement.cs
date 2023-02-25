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
        if (flower.wrapleft && transform.position != end)
        {
            transform.position = Vector3.Lerp(transform.position, end, lerpTime * Time.deltaTime);
            time += Time.deltaTime;
        }
        if (time > 3) { flower.wrapleft = false; time = 0; }
        if (flower.wrapright && transform.position != start)
        {
            transform.position = Vector3.Lerp(transform.position, start, lerpTime * Time.deltaTime);
            time += Time.deltaTime;
        }
        if (time > 3) { flower.wrapright = false; time = 0; }
    }
}
