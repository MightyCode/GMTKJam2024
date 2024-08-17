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
        GameObject playerBody = player.transform.Find("PlayerBody").gameObject;

        // Move player to new coordinates
        playerBody.GetComponent<CharacterController>().enabled = false;
        player.transform.position = tpPoint.position;
        playerBody.GetComponent<CharacterController>().enabled = true;
    }
}
