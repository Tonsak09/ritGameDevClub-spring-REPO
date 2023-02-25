using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSelection : MonoBehaviour
{
    public Vector3 bouquetPosition; //starting point of the first bouquet
    public float bouquetDis, flowerDis;

    //storing all clicked flowers into a obj list
    public List<GameObject> flowerSelection;
    public int bouquetFlowerAmount;

    public List<GameObject> bouquetOne;
    public List<GameObject> bouquetTwo;
    public List<GameObject> bouquetThree;

    public GameObject table;

    [SerializeField] private Material selMaterial;
    int count = 1;
    Vector3 tempPos;
    void Update()
    {
        //flower is on layer 6
        int layerMask = 1 << 6;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, layerMask)) {
            if (Input.GetButtonDown("Fire1")) {
                print("Flower is detected");

                //adding to the list
                flowerSelection.Add(hit.collider.gameObject);

                //highlighting the object idk(for visuals????)
                var selection = hit.transform;
                var selRender = selection.GetComponent<Renderer>(); 
                if(selection != null) {
                    selRender.material = selMaterial; 
                }
            }
        }
        //spawning the bouquets in a row after selection
        //
        if (flowerSelection.Count >= bouquetFlowerAmount) {
            switch (count) {
                case 1:
                    for(int a = 0; a< flowerSelection.Count; a++) { bouquetOne.Add(flowerSelection[a].gameObject); }
                    flowerSelection.Clear();
                    bouquetOne[0].transform.position = bouquetPosition;
                    tempPos = bouquetPosition;
                    //the first flower will be nudged a bit to the right
                    foreach (var flower in bouquetOne) {
                        flower.transform.position = new Vector3(tempPos.x + flowerDis, tempPos.y, tempPos.z);
                        tempPos = flower.transform.position;
                    }
                    //wrapping process
                    table.gameObject.SetActive(true);

                    table.gameObject.SetActive(false);
                    //ribbon process
                    table.gameObject.SetActive(true);

                    table.gameObject.SetActive(false);
                    break;
                case 2:
                    for (int a = 0; a < flowerSelection.Count; a++) { bouquetTwo.Add(flowerSelection[a].gameObject); }
                    flowerSelection.Clear();
                    bouquetTwo[0].transform.position = new Vector3(bouquetPosition.x + bouquetDis, bouquetPosition.y, bouquetPosition.z);
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
                    flowerSelection.Clear();
                    bouquetThree[0].transform.position = new Vector3(bouquetPosition.x + 2*bouquetDis, bouquetPosition.y, bouquetPosition.z);
                    tempPos = bouquetThree[0].transform.position;
                    //the first flower will be nudged a bit to the right
                    foreach (var flower in bouquetThree)
                    {
                        flower.transform.position = new Vector3(tempPos.x + flowerDis, tempPos.y, tempPos.z);
                        tempPos = flower.transform.position;
                    }
                    break;
            } count++;
        }
    }
}
