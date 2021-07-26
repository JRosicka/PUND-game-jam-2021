public class VictoryCollectible : Collectible {
    public override void ApplyCollectible(PlayerController player) {
        GameManager.Instance.EndGameWithWinner(player);
    }
}
