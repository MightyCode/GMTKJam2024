using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public float zOffset;


    private void Update()
    {
        if(target != null)
        {
            this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z + zOffset);
        }
    }
}
