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
        // Move player to new coordinates
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = tpPoint.position;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
