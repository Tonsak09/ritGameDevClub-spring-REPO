using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] [Range(0f, 4f)] float lerpTime;
    [SerializeField] Vector3 end, start;

    //the ref and the timer
    public FlowerSelection flower;
    float time;
    public bool wowBool = false;
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
        //TABLE
        if (flower.tabdown&&transform.position!=end && gameObject.tag == "table")
        {
            transform.position = Vector3.Lerp(transform.position, end, lerpTime * Time.deltaTime);
        }
        if (flower.tabup&&transform.position!=start && gameObject.tag == "table")
        {
            transform.position = Vector3.Lerp(transform.position, start, lerpTime * Time.deltaTime);
        }
        //ARRANGEMENT SCREEN
        if (flower.arrangedown && transform.position != end && gameObject.tag == "arrange")
        {
            transform.position = Vector3.Lerp(transform.position, end, lerpTime * Time.deltaTime);
            time += Time.deltaTime;
        }
        if (time > 3) { 
            flower.arrangedown = false; 
            flower.arrangeup = true; 
            transform.position = Vector3.Lerp(transform.position, start, lerpTime * Time.deltaTime);
        }
        if (time > 4) time = 0;
        //WOW
        if (flower.wowdown && transform.position != end && gameObject.tag == "wow"&&!wowBool)
        {
            transform.position = Vector3.Lerp(transform.position, end, lerpTime * Time.deltaTime);
        }
        if(flower.wowdown && transform.position != start && gameObject.tag == "wow")
        {
            time += Time.deltaTime;
            //flower.wowdown = false;
            wowBool = true;
            if (time > 10)
            {
                transform.position = Vector3.Lerp(transform.position, start, lerpTime * Time.deltaTime);
            }
            if(time>11)time = 0;
        }
    }
    public bool wowComplete()
    {
        return wowBool;
    }
}
