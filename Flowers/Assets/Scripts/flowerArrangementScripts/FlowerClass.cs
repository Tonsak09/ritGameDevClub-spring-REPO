using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerClass : MonoBehaviour
{
    public string name;
    public Material sprite;
    public string rarity;
    public FlowerClass(string name, Material sprite, string rarity)
    {
        this.name = name;
        this.sprite = sprite;
        this.rarity = rarity;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
