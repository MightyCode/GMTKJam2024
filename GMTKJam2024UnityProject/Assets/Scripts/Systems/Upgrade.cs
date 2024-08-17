using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private TheBeast theBeast;

    private PlayerManager playerManager;

    [SerializeField] private List<string> initialUpgrades = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        theBeast = TheBeast.Instance;

        playerManager = PlayerManager.Instance;

        // Apply upgrade to player
        foreach (string upgrade in initialUpgrades)
        {
            ApplyUpgrade(upgrade);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyUpgrade(string upgrade)
    {
        switch (upgrade) {
            case "Speed 1":
                playerManager.Speed *= 1.5f;
                break;
            case "Speed 2":
                playerManager.Speed *= 2f;
                break;
            case "Attack":
                playerManager.CanAttack = true;
                // Unlock the attack
                break;
            case "Dash":
                playerManager.CanDash = true;
                // Unlock the dash
                break;
            case "Attack Damage 1":
                // Attack damage * 1.5
                break;
            case "Attack Damage 2":
                // Attack damage * 2
                break;
            case "Attack Speed 1":
                // Attack speed * 1.5
                break;
            case "Dash speed 1":
                playerManager.DashSpeed *= 1.5f;
                // Dash speed * 1.5
                break;
            case "Time Limit 1":
                theBeast.WaitingForFeeding *= 2;
                // Time limit * 3
                break;
            case "Time Limit 2":
                theBeast.WaitingForFeeding *= 2;
                // Time limit * 3
                break;
            case "Time Limit 3":
                theBeast.WaitingForFeeding *= 2;
                // Time limit * 2
                break;
            case "Time Limit 4":
                theBeast.WaitingForFeeding *= 1.5f;
                // Time limit * 1.5
                break;
        }
    }
}
