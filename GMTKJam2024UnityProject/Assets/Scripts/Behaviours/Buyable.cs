using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Buyable : MonoBehaviour
{
    [SerializeField] private float price;
    [SerializeField] private string upgrade;
    [SerializeField] private TheBeast theBeast;
    [SerializeField] private Upgrade upgradeManager;

    [SerializeField] private TMP_Text shopText;

    private bool isBought = false;

    private Material baseMaterial;
    public Material tryingBuy;
    public Material bought;

    // Start is called before the first frame update
    void Start()
    {
        if (price == -1)
        {
            shopText.color = Color.gray;
            shopText.text = upgrade + "\nprice : XXX";
        }

        shopText.text = upgrade + " \nprice : " + price;

        if (isBought)
        {
            shopText.color = Color.green;
        }

        baseMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryBuy()
    {
        Debug.Log("--TryBuy");

        if (theBeast.CurrentFood >= price)
        {
            theBeast.SetFood(theBeast.CurrentFood - price);
            upgradeManager.ApplyUpgrade(upgrade);
            isBought = true;
            shopText.color = Color.green;

            //Replace material by bought
            GetComponent<MeshRenderer>().material = bought;
        } else
        {
            GetComponent<MeshRenderer>().material = tryingBuy;
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
