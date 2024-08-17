using UnityEngine;

public class Upgrade : MonoBehaviour
{
    /* List upgrade */
    /**
     * Speed 1 : player speed * 1.5
     * Speed 2 : player speed * 2
     * 
     * Attack : Unlock the attack
     * Dash : Unlock the dash
     * 
     * Attack Damage 1 : Attack damage * 1.5
     * Attack Damage 2 : Attack damage * 2
     * 
     * Attack Speed 1 : Attack speed * 1.5
     * 
     * Dash speed 1 : Dash speed * 1.5
     * 
     * Time Limit 1 : Time limit * 3
     * Time Limit 2 : Time limit * 3
     * Time Limit 3 : Time limit * 2
     * Time Limit 4 : Time limit * 1.5
     * 
     */

    [SerializeField] TheBeast theBeast;

    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.Instance;

        // Apply upgrade to player
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
                theBeast.waitingForFeeding *= 3;
                // Time limit * 3
                break;
            case "Time Limit 2":
                theBeast.waitingForFeeding *= 3;
                // Time limit * 3
                break;
            case "Time Limit 3":
                theBeast.waitingForFeeding *= 2;
                // Time limit * 2
                break;
            case "Time Limit 4":
                theBeast.waitingForFeeding *= 1.5f;
                // Time limit * 1.5
                break;
        }
    }
}
