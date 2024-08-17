using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    [SerializeField]
    private Transform tpPoint;

    [SerializeField]
    private GameObject player;

    public void OnEnter()
    {
        player.transform.position = tpPoint.position;
    }
}
