public class HealthCollectible : UpgradeCollectible {
    public override void ApplyCollectible(PlayerController player) {
        player.Heal(1);    // Always just 1 health for now
    }
}
