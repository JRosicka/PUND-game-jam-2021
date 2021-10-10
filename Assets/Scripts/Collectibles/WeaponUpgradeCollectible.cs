using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeCollectible : UpgradeCollectible {

    [Serializable]
    public struct WeaponConfig {
        public float bulletScale;
        public float bulletSpeed;
        public float bulletFireDelay;
    }

    public List<WeaponConfig> WeaponConfigByUpgradeLevel;
    
    public override void ApplyCollectible(PlayerController player) {
        player.IncrementWeaponLevel();
        player.ApplyWeaponUpgrade(WeaponConfigByUpgradeLevel[Mathf.Clamp(player.GetWeaponLevel(), 0, WeaponConfigByUpgradeLevel.Count - 1)]);
    }
}
