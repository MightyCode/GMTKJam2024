using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTrigger : MonoBehaviour
{

    public UnityEvent TriggerEvent;
    bool playerCollide;

    private void Start()
    {
        playerCollide = false;
    }


    private void Update()
    {
        if (playerCollide && Input.GetButtonDown("Interact"))
        {
            TriggerEvent.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && TriggerEvent != null)
        {
            playerCollide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && TriggerEvent != null)
        {
            playerCollide = false;
        }
    }
}
