using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Buyable : MonoBehaviour
{
    [SerializeField] private float price;
    [SerializeField] private TheBeast theBeast;
    [SerializeField] private Upgrade upgradeManager;

    private TMP_Text shopText;

    private bool isBought = false;

    private Material baseMaterial;
    public Material tryingBuy;
    public Material bought;

    // Start is called before the first frame update
    void Start()
    {
        baseMaterial = GetComponent<MeshRenderer>().material;
        shopText = GetComponentInChildren<TMP_Text>();

        if (price == -1)
        {
            shopText.color = Color.red;
            shopText.text = name + "\nPrice : XXX";
            GetComponent<MeshRenderer>().material = bought;
        }

        shopText.text = name + " \nPrice : " + price;

        if (isBought)
        {
            shopText.color = Color.grey;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryBuy()
    {
        if (!isBought)
        { 
            if (theBeast.CurrentFood >= price)
            {
                theBeast.SetFood(theBeast.CurrentFood - price);
                upgradeManager.ApplyUpgrade(name);
                isBought = true;

                //Replace material by bought
                GetComponent<MeshRenderer>().material = bought;
            }
            else
            {
                GetComponent<MeshRenderer>().material = tryingBuy;
            }
        }
    }

    public void Leave()
    {
        if (!isBought)
        {
            GetComponent<MeshRenderer>().material = baseMaterial;
        }
    }
}
