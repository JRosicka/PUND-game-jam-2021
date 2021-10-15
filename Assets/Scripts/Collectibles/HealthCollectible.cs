public class HealthCollectible : UpgradeCollectible {
    public override void ApplyCollectible(PlayerController player) {
        AudioManager.Instance.PlayHealthPickup();
        player.Heal(1);    // Always just 1 health for now
    }

    public override bool CanBeCollectedByPlayer(PlayerController player) {
        return true;
    }
}
