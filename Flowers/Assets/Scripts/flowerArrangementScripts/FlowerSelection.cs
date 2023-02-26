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

    public GameObject chosenWrap, chosenRib;

    public bool tabdown = false, tabup = true;
    public bool arrangedown = false, arrangeup = true;
    public bool wowdown = false, wowup = true; 
    public bool wrapleft = false, wrapright = false;
    public bool ribleft = false, ribright = false;

    public LayerMask flowerMask, wrapperMask, ribbonMask;

    public bool wowBool { get; set; }
    [SerializeField] Camera cam;
    [SerializeField] private Material selMaterial;
    int count = 1;
    Vector3 tempPos;
    public bool moveOn = false;
    float time, three;
    [SerializeField] FlowerSpawn spawn;
    public bool selectFlowers { get; set; }

    //get set stuff
    public bool tmoveDown
    {
        get { return tabdown; }
        set { tabdown = value; }
    }
    public bool tmoveUp
    {
        get { return tabup; }
        set { tabup = value; }
    }

    private void Start()
    {
        flowerSelection = new List<GameObject>();
        wowBool = false;
        selectFlowers = true;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(ray, out hit, 10000))
            {
                print(hit.collider.name);
            }

            if (Physics.Raycast(ray, out hit, 10000, flowerMask) && selectFlowers)
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
                    selection.GetComponent<Collider>().enabled = false;
                }

                if(flowerSelection.Count >= 5)
                {
                    selectFlowers = false;
                }
            }
            if (Physics.Raycast(ray, out hit, 10000, wrapperMask) &&!wrapleft)
            {
                //ONLY ONE WRAPPER CAN BE SELECTED
                print("Wrapper is detected");
                //highlighting the object idk(for visuals????)
                var selection = hit.transform;
                var selRender = selection.GetComponent<Renderer>();
                if (selection != null && selRender.tag == "wrapper")
                {
                    //selRender.material = selMaterial;
                }
                chosenWrap = hit.collider.gameObject;
                wrapright = true;
                ribleft = true;
            }
            if (Physics.Raycast(ray, out hit, 10000, ribbonMask)&&!ribleft )
            {
                //ONLY ONE RIBBON CAN BE SELECTED
                print("Ribbon is detected");
                //highlighting the object idk(for visuals????)
                var selection = hit.transform;
                var selRender = selection.GetComponent<Renderer>();
                if (selection != null && selRender.tag == "ribbon")
                {
                    //selRender.material = selMaterial;
                }
                chosenRib = hit.collider.gameObject;
                ribright = true;
                //arranging screen
                //show the finished bouquet here
                StartCoroutine(OrphanWrapAndRib(2)); // Orphan maker after 2 seconds 
                
            }
        }
        if (moveOn)three+=Time.deltaTime;
        if(three>3)
        {
            if (count == 2) { moveBouquet(bouquetOne); }
            else if (count == 3) { moveBouquet(bouquetTwo); }
            else { moveBouquet(bouquetThree); }
            time += Time.deltaTime;
        }
        if (time > 2)
        {
            moveOn = false;
            three = 0;
            time = 0;
            selectFlowers = true;

        }
        if (bouquetThree.Count > 1 && three > 3)
        {
            wowBool = true;
            count = 1;
        }

        //{
        //    wowup = false;
        //    wowdown = true;
        //}
        //transitising from picking->wrapping->ribbon between each bouquet
            flowerPick();
        wowComplete();
        spawn.spawned = false;
    }
    public bool wowComplete()
    {
        return wowBool;
    }
    void moveBouquet(List<GameObject> bouquet)
    {
        foreach(var item in bouquet)
        {
            if (item != null) {item.transform.position = Vector3.Lerp(item.transform.position, 
                new Vector3(item.transform.position.x-5, item.transform.position.y, item.transform.position.z), 1 * Time.deltaTime); }
            
        }
    }
    void presentFlowers(List<GameObject> bouquet)
    {
        moveOn = true;
        //moving to the center
        foreach(var flower in bouquet)
        {
            flower.transform.position = new Vector3(flower.transform.position.x-1f, flower.transform.position.y, flower.transform.position.z);
        }
        chosenWrap.transform.position = new Vector3(0, 1.5f, -1.6f);
        bouquet.Add(chosenWrap);
        chosenWrap = null;
        chosenRib.transform.position = new Vector3(0, 1.5f, -1.6f);
        bouquet.Add(chosenRib);
        chosenRib = null;
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
            tabup = false;
            tabdown = true;
            //once the bouquets flowers are selected, its time for the wrapping
            wrapleft = true;
        }
    }

    private IEnumerator OrphanWrapAndRib(float time)
    {
        yield return new WaitForSeconds(time);

        chosenWrap.transform.parent = null;
        chosenRib.transform.parent = null;

        if (count == 2) { presentFlowers(bouquetOne); }
        else if (count == 3) { presentFlowers(bouquetTwo); }
        else { presentFlowers(bouquetThree); }
        //time to pick the next bouquet
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(cam.ScreenPointToRay(Input.mousePosition));
    }
}
