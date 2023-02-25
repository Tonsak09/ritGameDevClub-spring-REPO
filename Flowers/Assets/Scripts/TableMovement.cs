using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMovement : MonoBehaviour
{
    [SerializeField] [Range(0f, 4f)] float lerpTime;
    [SerializeField] Vector3 end, start;
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
        if (flower.movedown)
        {
            transform.position = Vector3.Lerp(transform.position, end, lerpTime * Time.deltaTime);
            time += Time.deltaTime;
        }
        if (time > 2) { flower.movedown = false; time = 0; }
        if (flower.moveup)
        {
            transform.position = Vector3.Lerp(transform.position, start, lerpTime * Time.deltaTime);
            time += Time.deltaTime;
        }
        if (time > 2) { flower.moveup = false; time = 0; }
    }
}
