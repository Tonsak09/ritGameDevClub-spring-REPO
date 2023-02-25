using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapperSpawn : MonoBehaviour
{
    public int rows, columns;
    public Vector3 spawnPoint;
    public float columnDis, rowDis;

    public GameObject wrapperPrefab;
    public Transform parent;
    GameObject temp, reference;

    void Start()
    {
        for (int b = 0; b < rows; b++)
        {
            if (b == 0) { temp = Instantiate(wrapperPrefab, spawnPoint, Quaternion.identity, parent); reference = temp;
            }
            else
            {
                temp = Instantiate(wrapperPrefab,
                new Vector3(reference.transform.position.x, temp.transform.position.y - rowDis, spawnPoint.z),
                Quaternion.identity, parent);
            }
            for (int a = 0; a < columns; a++)
            {
                temp = Instantiate(wrapperPrefab,
                    new Vector3(temp.transform.position.x + columnDis, temp.transform.position.y, spawnPoint.z),
                    Quaternion.identity, parent);
            }
        }
    }
}
