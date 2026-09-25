using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarioPowerUpsSpawner : PowerUpsSpawner
{
    [SerializeField] GameObject mushroomPrefab;
    [SerializeField] GameObject fireFlowerPrefab;

    public override PowerUps SpawnPowerUp()
    {
        MarioController mario = FindObjectOfType<MarioController>();

        if (mario == null)
        {
            return null;
        }

        if (mario.GetIsBig())
        {
            Debug.Log("Fire Mario");
            return new FireFlowerSpawn(fireFlowerPrefab);
        }
        Debug.Log("Big Mario");
        return new MushroomSpawn(mushroomPrefab);
    }
}