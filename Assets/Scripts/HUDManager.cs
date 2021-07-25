using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public List<GameObject> health1;
    public List<GameObject> health2;

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

            if (health1[0].activeSelf == false)
            {
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

            if (health2[0].activeSelf == false)
            {
                // Kill ship somehow
            }
        }
    }
}
