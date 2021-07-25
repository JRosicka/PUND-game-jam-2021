using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public List<GameObject> health1;
    public List<GameObject> health2;

    public List<GameObject> maps1;
    public List<GameObject> maps2;

    public int MaxHP;
    public int StartHP;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.damageEvent.AddListener(ApplyDamage);
        EventManager.healEvent.AddListener(ApplyHealth);
        EventManager.mapFragmentCollectionEvent.AddListener(ApplyMapFragment);

        SetStartHP(health1);
        SetStartHP(health2);
        SetStartFragments(maps1);
        SetStartFragments(maps2);
    }

    private void SetStartHP(List<GameObject> healths) {
        UpdateCollectionDisplay(healths, StartHP, MaxHP);
    }

    private void SetStartFragments(List<GameObject> fragments) {
        foreach (GameObject fragment in fragments) {
            fragment.SetActive(false);
        }
    }
    
    public void ApplyDamage(int playerID)
    {
        if (playerID == 0) // Player 1 takes damage
        {
            // Disable highest health bar
            for (int n = MaxHP - 1; n >= 0; n--)
            {
                if (health1[n].activeSelf == true)
                {
                    health1[n].SetActive(false);
                    break;
                }
            }

            if (health1[0].activeSelf == false) // the ship is out of HP!
            {
                int heldFragments = 0;
                // Find how many map fragments player 1 owns and deactivate them
                foreach (GameObject mapFragment in maps1)
                {
                    if (mapFragment.activeSelf == true)
                    {
                        heldFragments++;
                        mapFragment.SetActive(false);
                    }
                }
            }
        } else // Player 2 takes damage
        {
            // Disable highest health bar
            for (int n = MaxHP - 1; n >= 0; n--)
            {
                if (health2[n].activeSelf == true)
                {
                    health2[n].SetActive(false);
                    break;
                }
            }

            if (health2[0].activeSelf == false) // the ship is out of HP!
            {
                int heldFragments = 0;
                // Find how many map fragments player 2 owns and deactivate them
                foreach (GameObject mapFragment in maps2)
                {
                    if (mapFragment.activeSelf == true)
                    {
                        heldFragments++;
                        mapFragment.SetActive(false);
                    }
                }
            }
        }
    }

    public void ApplyHealth(int playerID, int newHealth) {
        if (playerID == 0) {
            ApplyHealthToPlayer(health1, newHealth);
        } else if (playerID == 1) {
            ApplyHealthToPlayer(health2, newHealth);
        }
    }

    private void ApplyHealthToPlayer(List<GameObject> healths, int newHealth) {
        newHealth = Mathf.Clamp(newHealth, 0, newHealth);
        UpdateCollectionDisplay(healths, Mathf.Min(newHealth, MaxHP), MaxHP);
    }
    
    public void ApplyMapFragment(int playerID, int newMapFragmentCount) {
        if (playerID == 0) {
            ApplyMapFragmentsToPlayer(maps1, newMapFragmentCount);
        } else if (playerID == 1) {
            ApplyMapFragmentsToPlayer(maps2, newMapFragmentCount);
        }
    }

    private void ApplyMapFragmentsToPlayer(List<GameObject> fragments, int newFragmentCount) {
        UpdateCollectionDisplay(fragments, newFragmentCount, fragments.Count);
    }

    private void UpdateCollectionDisplay(List<GameObject> collection, int newCount, int maxCount) {
        int index = 0;
        for (int i = index; i < newCount; i++) {
            collection[i].SetActive(true);
            index++;
        }

        for (int i = index; i < maxCount; i++) {
            collection[i].SetActive(false);
        }

    }
}
