using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    // Adjustable Parameters
    public float projectileSpeed;
    public float secondsUntilGravity;
    public PlayerController FiringPlayer;

    private Rigidbody ourRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        ourRigidbody = GetComponent<Rigidbody>();
        ourRigidbody.velocity = transform.forward * projectileSpeed; // Cannonball spawns traveling forward (relative to ???)
    }

    // Update is called once per frame
    void Update()
    {
        // The cannonball starts falling after a set amount of time
        secondsUntilGravity -= Time.deltaTime;
        if (secondsUntilGravity < 0)
        {
            ourRigidbody.constraints = RigidbodyConstraints.None; // Re-enables movement along the Y axis
        }

        // Despawns cannonball if it sinks beneath the waves
        if (transform.position.y < -50)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision thisCollision)
    {
        // Checks whether the collided object was a player
        GameObject theirGameObject = thisCollision.gameObject;
        PirateShip theirPlayerData = theirGameObject.GetComponent<PirateShip>();
        if (theirPlayerData != null) // if it hit a player
        {
            // Ignore the collision if the player hit themselves - could happen right after firing
            if (theirPlayerData == FiringPlayer.Ship) {
                return;
            }
            
            // Remove health
            theirPlayerData.PlayerController.ApplyDamage(1);
        }

        // Despawn cannonball
        Destroy(gameObject);
    }
}
