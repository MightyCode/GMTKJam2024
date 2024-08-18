using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    [SerializeField]
    private Transform tpPoint;

    public void OnEnter()
    {
        GameObject playerBody = PlayerManager.Instance.gameObject;

        // Move player to new coordinates
        playerBody.GetComponent<CharacterController>().enabled = false;
        PlayerManager.Instance.gameObject.transform.position = tpPoint.position;
        playerBody.GetComponent<CharacterController>().enabled = true;
    }
}
