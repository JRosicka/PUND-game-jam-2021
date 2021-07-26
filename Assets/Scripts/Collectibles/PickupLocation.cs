using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupLocation : MonoBehaviour {
    public Collectible CurrentCollectible;
    public SpriteRenderer CollectibleIcon;

    protected bool hasHadMapFragmentBefore;

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
    
    protected void OnTriggerEnter(Collider other) {
        if (CurrentCollectible == null) return;
        
        PirateShip ship = other.gameObject.GetComponent<PirateShip>();
        if (ship == null) return;

        PlayerController player = ship.PlayerController;
        GameManager.Instance.IslandManager.OnCollectiblePickedUp(player, CurrentCollectible);

        CollectibleIcon.sprite = null;
        Destroy(CurrentCollectible);
        CurrentCollectible = null;
    }
}
