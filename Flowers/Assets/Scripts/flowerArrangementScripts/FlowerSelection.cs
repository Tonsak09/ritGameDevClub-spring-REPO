using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//the most unorganized script...
public class FlowerSelection : MonoBehaviour
{
    public Vector3 bouquetPosition; //starting point of the first bouquet
    public float flowerDis;

    //storing all clicked flowers into a obj list
    public List<GameObject> flowerSelection;
    public int bouquetFlowerAmount;

    public List<GameObject> bouquetOne;
    public List<GameObject> bouquetTwo;
    public List<GameObject> bouquetThree;

    public bool tabmovedown = false, tabmoveup = false;
    public bool boumovedown = false, boumoveup = false; //doing this later
    public bool wrapleft = false, wrapright = false;

    public LayerMask flowerMask, wrapperMask;

    [SerializeField] private Material selMaterial;
    int count = 1;
    Vector3 tempPos;

    //get set stuff
    public bool tmoveDown
    {
        get { return tabmovedown; }
        set { tabmovedown = value; }
    }
    public bool tmoveUp
    {
        get { return tabmoveup; }
        set { tabmoveup = value; }
    }
    public bool bmoveDown
    {
        get { return boumovedown; }
        set { boumovedown = value; }
    }
    public bool bmoveUp
    {
        get { return boumoveup; }
        set { boumoveup = value; }
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(ray, out hit, flowerMask))
            {
                print("Flower is detected");
                //adding to the list
                flowerSelection.Add(hit.collider.gameObject);
                //highlighting the object idk(for visuals????)
                var selection = hit.transform;
                var selRender = selection.GetComponent<Renderer>();
                if (selection != null && selRender.tag == "flower")
                {
                    selRender.material = selMaterial;
                }
            }
            if (Physics.Raycast(ray, out hit, wrapperMask))
            {
                //ONLY ONE WRAPPER CAN BE SELECTED
                print("Wrapper is detected");
                //highlighting the object idk(for visuals????)
                var selection = hit.transform;
                var selRender = selection.GetComponent<Renderer>();
                if (selection != null && selRender.tag == "wrapper")
                {
                    selRender.material = selMaterial;
                }
                //wrapright = true;
            }
        }
        //transitising from picking->wrapping->ribbon between each bouquet
            flowerPick();

    }
    void wrapperPick()
    {
        wrapleft = true;
    }
        //spawning the bouquets in a row after selection
    void flowerPick()
    {
        if (flowerSelection.Count >= bouquetFlowerAmount)
        {
            switch (count)
            {
                case 1:
                    for (int a = 0; a < flowerSelection.Count; a++) { bouquetOne.Add(flowerSelection[a].gameObject); }
                    bouquetOne[0].transform.position = bouquetPosition;
                    tempPos = bouquetPosition;
                    //the first flower will be nudged a bit to the right
                    foreach (var flower in bouquetOne)
                    {
                        flower.transform.position = new Vector3(tempPos.x + flowerDis, tempPos.y, tempPos.z);
                        tempPos = flower.transform.position;
                    }
                    break;
                case 2:
                    for (int a = 0; a < flowerSelection.Count; a++) { bouquetTwo.Add(flowerSelection[a].gameObject); }
                    bouquetTwo[0].transform.position = bouquetPosition;
                    tempPos = bouquetTwo[0].transform.position;
                    //the first flower will be nudged a bit to the right
                    foreach (var flower in bouquetTwo)
                    {
                        flower.transform.position = new Vector3(tempPos.x + flowerDis, tempPos.y, tempPos.z);
                        tempPos = flower.transform.position;
                    }
                    break;
                case 3:
                    for (int a = 0; a < flowerSelection.Count; a++) { bouquetThree.Add(flowerSelection[a].gameObject); }
                    bouquetThree[0].transform.position = bouquetPosition;
                    tempPos = bouquetThree[0].transform.position;
                    //the first flower will be nudged a bit to the right
                    foreach (var flower in bouquetThree)
                    {
                        flower.transform.position = new Vector3(tempPos.x + flowerDis, tempPos.y, tempPos.z);
                        tempPos = flower.transform.position;
                    }
                    break;
            }
            count++;
            flowerSelection.Clear();
            tabmovedown = true;
            //once the bouquets flowers are selected, its time for the wrapping
            wrapperPick();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition));
    }
}
