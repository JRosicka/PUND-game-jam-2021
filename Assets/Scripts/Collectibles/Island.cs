using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Island : PickupLocation {
    public float MinShipDistanceForCollectibleToAppear;
    public DialogLine Riddle;
    
    public bool IsEligibleForCollectibleAssignment() {
        return GameManager.Instance.ClosestShipDistance(transform.position) > MinShipDistanceForCollectibleToAppear;
    }

    public bool IsEligibleForMapFragmentAssignment() {
        return IsEligibleForCollectibleAssignment() && !hasHadMapFragmentBefore;
    }
}
