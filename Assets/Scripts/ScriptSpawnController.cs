using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSpawnController : MonoBehaviour
{

    [SerializeField] GameObject enemiesToSpawn;
    [SerializeField] Vector2 spawnArea;

    public void SpawnEnemy()
    {

        Vector3 position = RandomPos();

        GameObject newEnemy = Instantiate(enemiesToSpawn);
        newEnemy.transform.position = position;
        newEnemy.transform.parent = transform;

    }

    private Vector3 RandomPos()
    {
        Vector3 position = new Vector3();

        float f = Random.value > 0.5f ? -1f : 1f;

        if (Random.value > 0.5f)
        {
            position.x = Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }

        position.z = 0;

        return position;
    }

    public bool IsAlive()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
