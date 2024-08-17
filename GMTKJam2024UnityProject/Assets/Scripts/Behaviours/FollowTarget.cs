using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private GameObject target;


    private void Update()
    {
        this.transform.position = target.transform.position;
    }
}
