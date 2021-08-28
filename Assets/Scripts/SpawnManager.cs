using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy1Prefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = false;
    
    public void StartSpawning()
    {
        
        StartCoroutine(SpawnEnemyRoutine());
        
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {  

        while (_stopSpawning is false)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 posToSpawn = new Vector3((Random.Range(-8f, 8.5f)), 5.6f, 0f);            
            GameObject newEnemy = Instantiate(_enemy1Prefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_stopSpawning is false)
        {
            yield return new WaitForSeconds(3.0f);
            Vector3 posToSpawnPowerup = new Vector3((Random.Range(-8.2f, 8.8f)), 5.5f, 0f);
            int randomPowerUp = Random.Range(0, 3);
            GameObject newPowerUp = Instantiate(powerups[randomPowerUp], posToSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    public void onPlayerDeath()
    {
            _stopSpawning = true;           
    }

    
}
