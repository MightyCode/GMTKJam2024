using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Buyable : MonoBehaviour
{
    public float Price;
    private TheBeast theBeast;
    [SerializeField] private Upgrade upgradeManager;

    private TMP_Text shopText;

    public bool IsBought = false;

    private Material baseMaterial;
    public Material tryingBuy;
    public Material bought;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        theBeast = TheBeast.Instance;

        baseMaterial = GetComponent<MeshRenderer>().material;
        shopText = GetComponentInChildren<TMP_Text>();

        if (Price == LevelDataList.UNAVAILABLE_ITEM)
        {
            shopText.color = Color.red;
            shopText.text = name + "\nPrice : XXX";
            GetComponent<MeshRenderer>().material = tryingBuy;
        } else
        {
            shopText.text = name + " \nPrice : " + Price;
        }

        if (IsBought)
        {
            GetComponent<MeshRenderer>().material = bought;
            shopText.color = Color.grey;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryBuy()
    {
        if (!IsBought && Price > LevelDataList.UNAVAILABLE_ITEM)
        { 
            if (theBeast.CurrentFood >= Price)
            {
                theBeast.SetFood(theBeast.CurrentFood - Price);
                upgradeManager.ApplyUpgrade(name);
                IsBought = true;
                AudioPlayer.audioPlayer.PlayUpgradeAudio();

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
        if (!IsBought && Price > LevelDataList.UNAVAILABLE_ITEM)
        {
            GetComponent<MeshRenderer>().material = baseMaterial;
        }
    }
}
