using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boutique : MonoBehaviour
{
    public List<Renderer> flowers;
    public Renderer ribbon;
    public Renderer wrapF;
    public Renderer wrapB;


    public void Init(List<Texture2D> _flowers, Texture2D _ribbon, Texture2D _wrapF, Texture2D _wrapB)
    {
        for (int i = 0; i < flowers.Count; i++)
        {
            flowers[i].material.SetTexture("_BaseMap", _flowers[i]);
        }

        ribbon.material.SetTexture("_BaseMap", _ribbon);
        wrapF.material.SetTexture("_BaseMap", _wrapF);
        wrapB.material.SetTexture("_BaseMap", _wrapB);
    }
}
