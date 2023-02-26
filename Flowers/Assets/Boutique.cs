using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boutique : MonoBehaviour
{

    [SerializeField] List<Renderer> flowers;
    [SerializeField] Renderer ribbon;
    [SerializeField] Renderer wrapFront;
    [SerializeField] Renderer wrapBack;

    public void InitializeImages(List<Texture2D> _flowers, Texture2D _ribbon, Texture2D _wrapFront, Texture2D _wrapBack)
    {
        for (int i = 0; i < flowers.Count; i++)
        {
            flowers[i].material.SetTexture("_BaseMap", _flowers[i]);
        }

        ribbon.material.SetTexture("_BaseMap", _ribbon);
        wrapFront.material.SetTexture("_BaseMap", _wrapFront);
        wrapBack.material.SetTexture("_BaseMap", _wrapBack);
    }
}
