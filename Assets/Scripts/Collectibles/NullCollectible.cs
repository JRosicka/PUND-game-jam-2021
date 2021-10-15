public class NullCollectible : Collectible {
    public override void ApplyCollectible(PlayerController player) {
        // Nothing :)
    }

    public override bool CanBeCollectedByPlayer(PlayerController player) {
        return false;
    }
}
