using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableParent : MonoBehaviour
{
    public GameObject objetToMonitor;

    private void Update()
    {
        if(objetToMonitor == null)
        {
            Destroy(gameObject);
        }
    }
}
