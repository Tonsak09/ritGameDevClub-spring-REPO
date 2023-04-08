using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public virtual void Click()
    {
        print(this.gameObject.name + " has been clicked");
    }
}
