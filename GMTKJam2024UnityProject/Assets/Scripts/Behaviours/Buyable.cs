using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Buyable : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private string upgrade;
    [SerializeField] private TheBeast theBeast;

    [SerializeField] private TMP_Text shopText;

    private bool isBought = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryBuy()
    {
        
    }

    public void Leave()
    {

    }
}
