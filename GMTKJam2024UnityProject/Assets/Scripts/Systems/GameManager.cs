using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private TMP_Text playerResourceCounterText;

    private GameObject playerBody, playerUI;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get child object of player named "PlayerUI"
        playerUI = player.transform.Find("PlayerUI").gameObject;
        playerResourceCounterText = playerUI.transform.Find("ResourceCounter").GetComponent<TMP_Text>();

        playerBody = player.transform.Find("PlayerBody").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        playerResourceCounterText.text = PlayerManager.Instance.Resource.ToString();
    }
}
