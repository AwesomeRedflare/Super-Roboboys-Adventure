using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject smallEnemy;
    public GameObject medEnemy;
    public GameObject bigEnemy;

    public float spawnDistance;

    private bool hasSpawned = false;

    private void Update()
    {
        if(Vector2.Distance(player.transform.position, transform.position) < spawnDistance && hasSpawned == false)
        {
            if(PlayerController.goldAmount <= 25)
            {
                Instantiate(smallEnemy, transform.position, Quaternion.identity);
                hasSpawned = true;
            }

            if (PlayerController.goldAmount > 25 && PlayerController.goldAmount < 60)
            {
                Instantiate(medEnemy, transform.position, Quaternion.identity);
                hasSpawned = true;
            }

            if (PlayerController.goldAmount >= 60)
            {
                Instantiate(bigEnemy, transform.position, Quaternion.identity);
                hasSpawned = true;
            }
        }
    }

}
