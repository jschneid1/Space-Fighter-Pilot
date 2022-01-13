using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy1Prefab, _enemyAltPrefab, _bossEnemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private Enemy _newEnemy;
    
    [SerializeField]
    private int _enemiesSpawned, _wave, _enemyShielded, _rammingEnemy,_fireBackwardEnemy, _dodgeEnemy, _powerUpDestroy;
    
    private bool _stopSpawning = false;

    private Coroutine _enemySpawnRoutine, _powerUpRoutine, _altPowerUpRoutine, _negPowerUpRoutine, _altEnemySpawnRoutine, _spawnAmmoRoutine;

    [SerializeField]
    private float _ammoSpawnRate = 1.0f;
    
    public void StartSpawning()
    {
        _enemySpawnRoutine = StartCoroutine(SpawnEnemyRoutine());
        
        _powerUpRoutine = StartCoroutine(SpawnPowerUpRoutine());

        _altPowerUpRoutine = StartCoroutine(SpawnAltPowerUpRotine());

        _negPowerUpRoutine = StartCoroutine(SpawnNegPowerUpRotine());

        _spawnAmmoRoutine = StartCoroutine(SpawnAmmoRoutine());
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
            yield return new WaitForSeconds(Random.Range(5.0f, 11.0f));
            Vector3 posToSpawn = new Vector3((Random.Range(-8f, 8.5f)), 5.6f, 0f);
            GameObject newEnemy = Instantiate(_enemyAltPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            _enemiesSpawned += 1;
            _newEnemy = newEnemy.GetComponentInChildren<Enemy>();
            yield return _newEnemy;
            Movement();
            yield return new WaitForSeconds(10.0f);
        }
    }

    IEnumerator SpawnAmmoRoutine()
    {
        while (_stopSpawning is false)
        {
            yield return new WaitForSeconds(_ammoSpawnRate * 10.0f);
            Vector3 posToSpawnPowerup = new Vector3((Random.Range(-8.2f, 8.8f)), 5.5f, 0f);
            GameObject newPowerUp = Instantiate(powerups[3], posToSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(_ammoSpawnRate * Random.Range(1.0f, 5.0f));
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_stopSpawning is false)
        {
            yield return new WaitForSeconds(7.0f);
            Vector3 posToSpawnPowerup = new Vector3((Random.Range(-8.2f, 8.8f)), 5.5f, 0f);
            int randomPowerUp = Random.Range(0, 3);
            GameObject newPowerUp = Instantiate(powerups[randomPowerUp], posToSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));
        }
    }

    IEnumerator SpawnAltPowerUpRotine()
    {
        while (_stopSpawning is false)
        {
            yield return new WaitForSeconds(Random.Range(17.0f, 23.0f));
            Vector3 posToSpawnPowerup = new Vector3((Random.Range(-8.2f, 8.8f)), 5.5f, 0f);
            int randomPowerUp = Random.Range(4, 6);
            GameObject newPowerUp = Instantiate(powerups[randomPowerUp], posToSpawnPowerup, Quaternion.identity);
        }
    }

    IEnumerator SpawnNegPowerUpRotine()
    {
        while (_stopSpawning is false)
        {
            yield return new WaitForSeconds(Random.Range(25.0f, 30.0f));
            Vector3 posToSpawnPowerup = new Vector3((Random.Range(-8.2f, 8.8f)), 5.5f, 0f);
            GameObject newPowerUp = Instantiate(powerups[6], posToSpawnPowerup, Quaternion.identity);
        }
    }

    public void StopEnemySpawn()
    {
        StopCoroutine(_enemySpawnRoutine);
    }

    public void StopAltEnemySpawn()
    {
        StopCoroutine(_altEnemySpawnRoutine);
    }

    public void StopPowerUpSpawn()
    {
        StopCoroutine(_spawnAmmoRoutine);
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
        _rammingEnemy = Random.Range(1, 4);
        _fireBackwardEnemy = Random.Range(2, 5);
        _dodgeEnemy = Random.Range(2, 5);
        _powerUpDestroy = Random.Range(2, 5);
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
                _ammoSpawnRate = 0.75f;
            }
        }

        else if (_wave > 2)
        {
            {
                _newEnemy.AltMoveOne();
                _ammoSpawnRate = 0.5f;
            }
            if (_enemiesSpawned == 6)
            {
                StartAltEnemySpawn();
            }
        }

        if(_enemiesSpawned == _enemyShielded && _wave > 1)
        {
            _newEnemy.EnemyShieldActivate();
            _enemyShielded += Random.Range(2, 5);
        }

        else if(_enemiesSpawned == _rammingEnemy && _wave > 2)
        {
            _newEnemy.ActivateRam();
            _rammingEnemy += Random.Range(2, 5);
        }

        else if (_enemiesSpawned == _fireBackwardEnemy && _wave > 2)
        {
            _newEnemy.ActivateBackTurret();
            _fireBackwardEnemy += Random.Range(2, 5);
        }

        else if (_enemiesSpawned == _dodgeEnemy && _wave > 2)
        {
            _newEnemy.ActivateDodgeAbility();
        }

        else if (_enemiesSpawned == _powerUpDestroy && _wave > 2)
        {
            _newEnemy.ActivatePowerUpDestroy();
        }
    }

    public void BossEnemySpawn()
    {
        Vector3 posToSpawn = new Vector3((Random.Range(-8f, 8.5f)), 7.8f, 0f);
        Instantiate(_bossEnemyPrefab, posToSpawn, Quaternion.identity);
    }
}
