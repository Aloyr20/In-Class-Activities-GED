using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform player;

    void SpawnFireball()
    {
        // Instantiate the prefab
        GameObject newFireball = Instantiate(fireballPrefab, transform.position, transform.rotation);

        // Get the Fireball script
        Fireballs fireballScript = newFireball.GetComponent<Fireballs>();

        // Assign the player Transform
        fireballScript.player = player;
    }
}