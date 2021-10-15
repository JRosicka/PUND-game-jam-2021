public class MapFragmentCollectible : Collectible {
    private int mapFragmentCount;
    
    public bool WasDroppedByPlayer;

    public override void ApplyCollectible(PlayerController player) {
        AudioManager.Instance.PlayMapPiecePickup();
        for (int i = 0; i < mapFragmentCount; i++) {
            player.AddMapFragment();
        }
    }

    public override bool CanBeCollectedByPlayer(PlayerController player) {
        return true;
    }

    public void SetFragmentCount(int newMapFragmentCount) {
        mapFragmentCount = newMapFragmentCount;
    }
}
