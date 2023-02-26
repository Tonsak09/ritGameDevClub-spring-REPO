using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawn : MonoBehaviour
{
    public int rows, columns;
    public Vector3 spawnPoint;
    public float columnDis, rowDis;

    [SerializeField] List<string> names;
    [SerializeField] GameObject prefab;
    [SerializeField] List<FlowerClass> flowers;
    [SerializeField] List<string> rarities;
    [SerializeField] List<Material> materials;
    GameObject temp, reference;
    public FlowerClass[] flowerObj;

    void Start()
    {
        for (int b = 0; b < rows*(columns+1)+1; b++)
        {
            int num = Random.Range(0, names.Count);
            flowers.Add(new FlowerClass(names[num], materials[num], rarities[Random.Range(0, rarities.Count)]));
        }
        for (int b = 0; b < rows; b++)
        {
            if (b == 0)
            {
                temp = Instantiate(prefab, spawnPoint, Quaternion.identity); reference = temp;
            }
            else
            {
                temp = Instantiate(prefab,
                new Vector3(reference.transform.position.x, temp.transform.position.y - rowDis, spawnPoint.z),
                Quaternion.identity);
            }
            for (int a = 0; a < columns; a++)
            {
                temp = Instantiate(prefab,
                    new Vector3(temp.transform.position.x + columnDis, temp.transform.position.y, spawnPoint.z),
                    Quaternion.identity);
            }
        }
    }
    void Update()
    {
        if(flowers != null)
        {
            flowerObj = FindObjectsOfType<FlowerClass>();
            if(flowerObj != null)
            {
                for (int a = 0; a < flowerObj.Length; a++)
                {
                    flowerObj[a].GetComponent<FlowerClass>().name = flowers[a].name;
                    flowerObj[a].GetComponent<FlowerClass>().sprite = flowers[a].sprite;
                    flowerObj[a].GetComponent<FlowerClass>().rarity = flowers[a].rarity;
                }
            }
        }
    }
}

