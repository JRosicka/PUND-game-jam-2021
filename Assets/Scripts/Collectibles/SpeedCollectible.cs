public class SpeedCollectible : UpgradeCollectible {
    public override void ApplyCollectible(PlayerController player) {
        player.ApplySpeedBoost(.02f, 1.5f);    // Always just apply the same additive bonus
    }
}
