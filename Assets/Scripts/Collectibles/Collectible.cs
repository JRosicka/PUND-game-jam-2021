using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour {
    public Sprite Icon;
    public abstract void ApplyCollectible(PlayerController player);
}
