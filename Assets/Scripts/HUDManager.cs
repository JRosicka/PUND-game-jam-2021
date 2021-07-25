using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public List<GameObject> health1;
    public List<GameObject> health2;

    public List<GameObject> maps1;
    public List<GameObject> maps2;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.damageEvent.AddListener(ApplyDamage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(int playerID)
    {
        if (playerID == 0) // Player 1 takes damage
        {
            // Disable highest health bar
            for (int n = 2; n >= 0; n--)
            {
                if (health1[n].activeSelf == true)
                {
                    health1[n].SetActive(false);
                    break;
                }
            }

            if (health1[0].activeSelf == false) // the ship is out of HP!
            {
                // Transfer all map fragments to attacker
                int heldFragments = 0;
                // Find how many map fragments player 1 owns and deactivate them
                while (maps1[heldFragments].activeSelf == true)
                {
                    maps1[heldFragments].SetActive(false);
                    heldFragments++;
                }

                // Activate these map fragments for player 2
                for (int n = 0; n < 3; n++)
                {
                    if (maps2[n].activeSelf == false && heldFragments > 0)
                    {
                        maps2[n].SetActive(true);
                        heldFragments--;
                    }
                }

                // Kill ship somehow
            }
        } else // Player 2 takes damage
        {
            // Disable highest health bar
            for (int n = 2; n >= 0; n--)
            {
                if (health2[n].activeSelf == true)
                {
                    health2[n].SetActive(false);
                    break;
                }
            }

            if (health2[0].activeSelf == false) // the ship is out of HP!
            {
                // Transfer all map fragments to attacker
                int heldFragments = 0;
                // Find how many map fragments player 2 owns and deactivate them
                while (maps2[heldFragments].activeSelf == true)
                {
                    maps2[heldFragments].SetActive(false);
                    heldFragments++;
                }

                // Activate these map fragments for player 1
                for (int n = 0; n < 3; n++)
                {
                    if (maps1[n].activeSelf == false && heldFragments > 0)
                    {
                        maps1[n].SetActive(true);
                        heldFragments--;
                    }
                }

                // Kill ship somehow
            }
        }
    }
}
