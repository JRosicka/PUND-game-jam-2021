public class MapFragmentCollectible : Collectible {
    private int mapFragmentCount;

    public override void ApplyCollectible(PlayerController player) {
        for (int i = 0; i < mapFragmentCount; i++) {
            player.AddMapFragment();
        }
    }

    public void SetFragmentCount(int newMapFragmentCount) {
        mapFragmentCount = newMapFragmentCount;
    }
}
