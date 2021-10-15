public class VictoryCollectible : Collectible {
    public override void ApplyCollectible(PlayerController player) {
        GameManager.Instance.EndGameWithWinner(player);
    }

    public override bool CanBeCollectedByPlayer(PlayerController player) {
        // The player can only win if they are holding all the map fragments when they move to the victory location
        return player.CurrentMapFragmentCount == GameManager.Instance.IslandManager.totalMapFragmentCount;
    }
}
