using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy1Prefab, _enemyAltPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private Enemy _newEnemy;
    
    [SerializeField]
    private int _enemiesSpawned, _wave;
    
    private bool _stopSpawning = false;

    private Coroutine _enemySpawnRoutine, _powerUpRoutine, _altPowerUpRoutine, _negPowerUpRoutine, _altEnemySpawnRoutine;
    
    public void StartSpawning()
    {
        _enemySpawnRoutine = StartCoroutine(SpawnEnemyRoutine());
        
        _powerUpRoutine = StartCoroutine(SpawnPowerUpRoutine());

        _altPowerUpRoutine = StartCoroutine(SpawnAltPowerUpRotine());

        _negPowerUpRoutine = StartCoroutine(SpawnNegPowerUpRotine());
    }

    public void StartAltEnemySpawn()
    {
        _altEnemySpawnRoutine = StartCoroutine(SpawnAltEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning is false)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 posToSpawn = new Vector3((Random.Range(-8f, 8.5f)), 5.6f, 0f);            
            GameObject newEnemy = Instantiate(_enemy1Prefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            _enemiesSpawned += 1;
            _newEnemy = newEnemy.GetComponentInChildren<Enemy>();
            yield return _newEnemy;
            Movement();
            yield return new WaitForSeconds(Random.Range(2, 5));
        }
     }

    IEnumerator SpawnAltEnemyRoutine()
    {
        while (_stopSpawning is false)
        {
            yield return new WaitForSeconds(Random.Range(5, 11));
            Vector3 posToSpawn = new Vector3((Random.Range(-8f, 8.5f)), 5.6f, 0f);
            GameObject newEnemy = Instantiate(_enemyAltPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            _enemiesSpawned += 1;
            _newEnemy = newEnemy.GetComponentInChildren<Enemy>();
            yield return _newEnemy;
            Movement();
            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_stopSpawning is false)
        {
            yield return new WaitForSeconds(3.0f);
            Vector3 posToSpawnPowerup = new Vector3((Random.Range(-8.2f, 8.8f)), 5.5f, 0f);
            int randomPowerUp = Random.Range(0, 5);
            GameObject newPowerUp = Instantiate(powerups[randomPowerUp], posToSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    IEnumerator SpawnAltPowerUpRotine()
    {
        while (_stopSpawning is false)
        {
            yield return new WaitForSeconds(Random.Range(7, 10));
            Vector3 posToSpawnPowerup = new Vector3((Random.Range(-8.2f, 8.8f)), 5.5f, 0f);
            int randomPowerUp = Random.Range(5, 6);
            GameObject newPowerUp = Instantiate(powerups[randomPowerUp], posToSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    IEnumerator SpawnNegPowerUpRotine()
    {
        while (_stopSpawning is false)
        {
            yield return new WaitForSeconds(Random.Range(5, 11));
            Vector3 posToSpawnPowerup = new Vector3((Random.Range(-8.2f, 8.8f)), 5.5f, 0f);
            GameObject newPowerUp = Instantiate(powerups[6], posToSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    public void StopEnemySpawn()
    {
        StopCoroutine(_enemySpawnRoutine);
    }

    public void StopPowerUpSpawn()
    {
        StopCoroutine(_powerUpRoutine);
        StopCoroutine(_altPowerUpRoutine);
        StopCoroutine(_negPowerUpRoutine);
    }

    public void onPlayerDeath()
    {
            _stopSpawning = true;           
    }

    public void Wave(int wave)
    {
        _wave = wave;
    }

    public void EnemiesSpawned()
    {
        _enemiesSpawned = 0;
    }

    private void Movement()
    {
        if (_enemiesSpawned > 2 && _wave == 1)
        {
            _newEnemy.AlternativeMovement();
        }

        if (_wave == 2)
        {
            if (_enemiesSpawned > 0 && _enemiesSpawned < 6)
            {
                _newEnemy.AlternativeMovement();
            }
            else if (_enemiesSpawned > 5)
            {
                _newEnemy.AltMoveOne();
            }
        }

        if (_wave > 2)
        {
            {
                _newEnemy.AltMoveOne();
            }
        }
    }
}
