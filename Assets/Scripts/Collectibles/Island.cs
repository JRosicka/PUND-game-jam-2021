using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Island : MonoBehaviour {
    public Collectible CurrentCollectible;
    public SpriteRenderer CollectibleIcon;
    public float MinShipDistanceForCollectibleToAppear;
    public DialogLine Riddle;

    private bool hasHadMapFragmentBefore = false;

    public void ClearCollectible() {
        if (CurrentCollectible != null) {
            Destroy(CurrentCollectible);
        }

        CurrentCollectible = null;
    }

    public void AssignCollectible(Collectible newCollectible) {
        CurrentCollectible = newCollectible;
        CollectibleIcon.sprite = CurrentCollectible.Icon;

        if (newCollectible is MapFragmentCollectible) {
            hasHadMapFragmentBefore = true;
        }
    }

    public bool IsEligibleForCollectibleAssignment() {
        return GameManager.Instance.ClosestShipDistance(transform.position) > MinShipDistanceForCollectibleToAppear;
    }

    public bool IsEligibleForMapFragmentAssignment() {
        return IsEligibleForCollectibleAssignment() && !hasHadMapFragmentBefore;
    }

    private void OnTriggerEnter(Collider other) {
        PirateShip ship = other.gameObject.GetComponent<PirateShip>();
        if (ship == null) return;

        PlayerController player = ship.PlayerController;
        GameManager.Instance.IslandManager.OnCollectiblePickedUp(player, CurrentCollectible, this);

        CollectibleIcon.sprite = null;
        Destroy(CurrentCollectible);
        CurrentCollectible = null;
    }
}
