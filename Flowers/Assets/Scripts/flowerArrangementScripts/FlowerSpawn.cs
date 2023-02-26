using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawn : MonoBehaviour
{
    public int rows, columns;
    public Vector3 spawnPoint;
    public float columnDis, rowDis;

    //[SerializeField] List<string> names;
    [SerializeField] GameObject flowerShell;
    [SerializeField] List<FlowerDetails> flowers;
    //[SerializeField] List<string> rarities;
    //[SerializeField] List<Material> materials;
    GameObject temp, reference;

    public List<GameObject> flowerObj;

    void Start()
    {
        for (int b = 0; b < rows * (columns + 1) + 1; b++)
        {
            //int num = Random.Range(0, names.Count);
            flowers.Add(null);
        }
        for (int b = 0; b < rows; b++)
        {
            if (b == 0)
            {
                temp = Instantiate(flowerShell, spawnPoint, Quaternion.identity); 
                reference = temp;

                flowerObj.Add(temp);
            }
            else
            {
                temp = Instantiate(flowerShell,
                new Vector3(reference.transform.position.x, temp.transform.position.y - rowDis, spawnPoint.z),
                Quaternion.identity);

                flowerObj.Add(temp);
            }

            for (int a = 0; a < columns; a++)
            {
                temp = Instantiate(flowerShell,
                    new Vector3(temp.transform.position.x + columnDis, temp.transform.position.y, spawnPoint.z),
                    Quaternion.identity);

                flowerObj.Add(temp);
            }
        }
    }
    void Update()
    {
        if(flowers != null)
        {
            /*flowerObj = FindObjectsOfType<FlowerClass>();
            if(flowerObj != null)
            {
                for (int a = 0; a < flowerObj.Length; a++)
                {
                    *//*flowerObj[a].GetComponent<FlowerClass>().name = flowers[a].name;
                    flowerObj[a].GetComponent<FlowerClass>().sprite = flowers[a].sprite;
                    flowerObj[a].GetComponent<FlowerClass>().rarity = flowers[a].rarity;*//*

                    flowerObj[a].GetComponent<Renderer>().material.SetTexture("_BaseMap", flowers[a].texture);
                }
            }*/

            for (int i = 0; i < flowerObj.Count; i++)
            {
                flowerObj[i].transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_BaseMap", flowers[i].texture);
            }
        }
    }

    [System.Serializable]
    public class FlowerDetails
    {
        [SerializeField] public string name;
        [SerializeField] public Texture2D texture;
        [SerializeField] [Range(0, 2)] public int rarity;
    }
}

