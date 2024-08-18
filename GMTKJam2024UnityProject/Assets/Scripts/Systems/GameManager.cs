using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;

    private void Awake()
    {
        Time.timeScale = 1f;
        Collectible.Empty();
        Damagable.Empty();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings + " " + SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
