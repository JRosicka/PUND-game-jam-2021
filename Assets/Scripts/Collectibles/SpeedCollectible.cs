public class SpeedCollectible : UpgradeCollectible {
    public override void ApplyCollectible(PlayerController player) {
        AudioManager.Instance.PlaySpeedUpgradePickup();
        player.ApplySpeedBoost(.02f, 1.5f);    // Always just apply the same additive bonus
    }

    public override bool CanBeCollectedByPlayer(PlayerController player) {
        return true;
    }
}
