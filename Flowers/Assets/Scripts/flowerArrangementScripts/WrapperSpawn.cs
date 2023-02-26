using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapperSpawn : MonoBehaviour
{
    public int rows, columns;
    public Vector3 spawnPoint;
    public float columnDis, rowDis;

    public GameObject wrapperPrefab, ribbonPrefab;
    public Transform wrapperParent, ribbonParent;
    GameObject temp, reference;

    [SerializeField] List<ItemDetails> wrappers;
    [SerializeField] List<ItemDetails> ribbons;

    private int counter = 0;

    void Start()
    {
        spawnPoint = wrapperParent.transform.position;
        //WRAPPER
        for (int b = 0; b < rows; b++)
        {
            if (b == 0) 
            { 
                temp = Instantiate(wrapperPrefab, spawnPoint, Quaternion.identity, wrapperParent); 
                reference = temp;

                temp.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_BaseMap", wrappers[counter].texture);
                temp.transform.GetChild(1).GetComponent<Renderer>().material.SetTexture("_BaseMap", wrappers[counter].decalTexture);
                counter++;
            }
            else
            {
                temp = Instantiate(wrapperPrefab,
                new Vector3(reference.transform.position.x, temp.transform.position.y - rowDis, spawnPoint.z),
                Quaternion.identity, wrapperParent);

                temp.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_BaseMap", wrappers[counter].texture);
                temp.transform.GetChild(1).GetComponent<Renderer>().material.SetTexture("_BaseMap", wrappers[counter].decalTexture);
                counter++;

            }
            for (int a = 0; a < columns; a++)
            {
                temp = Instantiate(wrapperPrefab,
                    new Vector3(temp.transform.position.x + columnDis, temp.transform.position.y, spawnPoint.z),
                    Quaternion.identity, wrapperParent);

                temp.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_BaseMap", wrappers[counter].texture);
                temp.transform.GetChild(1).GetComponent<Renderer>().material.SetTexture("_BaseMap", wrappers[counter].decalTexture);
                counter++;
            }
        }

        counter = 0;

        //RIBBON
        for (int b = 0; b < rows; b++)
        {
            if (b == 0)
            {
                temp = Instantiate(ribbonPrefab, spawnPoint, Quaternion.identity, ribbonParent); 
                reference = temp;

                temp.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_BaseMap", ribbons[counter].texture);
                counter++;
            }
            else
            {
                temp = Instantiate(ribbonPrefab,
                new Vector3(reference.transform.position.x, temp.transform.position.y - rowDis, spawnPoint.z),
                Quaternion.identity, ribbonParent);

                temp.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_BaseMap", ribbons[counter].texture);
                counter++;
            }
            for (int a = 0; a < columns; a++)
            {
                temp = Instantiate(ribbonPrefab,
                    new Vector3(temp.transform.position.x + columnDis, temp.transform.position.y, spawnPoint.z),
                    Quaternion.identity, ribbonParent);

                temp.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_BaseMap", ribbons[counter].texture);
                counter++;
            }
        }
    }


    [System.Serializable]
    public class ItemDetails
    {
        [SerializeField] public string name;
        [SerializeField] public Texture2D texture;
        [SerializeField] public Texture2D decalTexture;
    }
}
