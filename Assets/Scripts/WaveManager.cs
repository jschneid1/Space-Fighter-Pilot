using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private GameManager _gameManager;
    private EnemyShieldBehaviour _enemy;

    [SerializeField]
    private bool _playerAlive = true, _bossSpawned = false;
    
    [SerializeField]
    private int _enemiesSpawned, _coRoutineUse, _wave, _enemiesAlive, _enemyShielded, _rammingEnemy, _bossAlive;
        
    [SerializeField]
    private SpriteRenderer[] _enemyArray;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        

        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
        if (_gameManager == null)
        {
            Debug.LogError("The Game Manager is NULL");
        }

        if(_enemy != null)
        {
            _enemy = GameObject.Find("EnemyShield").GetComponent<EnemyShieldBehaviour>();
        }

        _wave = 1;
        _spawnManager.Wave(_wave);
    }

    // Update is called once per frame
    void Update()
    {
        _enemyArray = GameObject.Find("SpawnManager").GetComponentsInChildren<SpriteRenderer>();
        _enemiesAlive = _enemyArray.Length;

        if (_playerAlive == true)
        {

            if (_enemiesSpawned == 5 && _wave == 1)
            {
                _spawnManager.StopEnemySpawn();
                if (_enemiesAlive == 0 && _coRoutineUse == 0)
                {
                    _uiManager.WaveOverSequence();
                    _spawnManager.StopPowerUpSpawn();
                    _coRoutineUse += 1;
                }
            }

            if (_enemiesSpawned == 10 && _wave == 2)
            {
                _spawnManager.StopEnemySpawn();
                if (_enemiesAlive == 0 && _coRoutineUse == 1)
                {
                    _uiManager.WaveOverSequence();
                    _spawnManager.StopPowerUpSpawn();
                    _coRoutineUse += 1;
                }
            }

            if (_enemiesSpawned == 15 && _wave == 3)
            {
                _spawnManager.StopEnemySpawn();
                _spawnManager.StopAltEnemySpawn();
                if (_enemiesAlive == 0 && _coRoutineUse == 2)
                {
                    _uiManager.WaveOverSequence();
                    _spawnManager.StopPowerUpSpawn();
                    _coRoutineUse += 1;
                }
            }

            if (_enemiesSpawned == 15 && _wave == 4 && _bossSpawned == false)
            {
                _spawnManager.BossEnemySpawn();
                _bossSpawned = true;
            }

            else if (_enemiesSpawned == 17 && _wave == 4)
            {
                _spawnManager.StopEnemySpawn();
                _spawnManager.StopAltEnemySpawn();
                if (_enemiesAlive == 0 && _coRoutineUse == 3 && _bossAlive == 0)
                {
                    _uiManager.WaveOverSequence();
                    _spawnManager.StopPowerUpSpawn();
                    _coRoutineUse += 1;
                    _gameManager.GameOver();
                }
            }
        }
    }

    public void StartWaveTwo()
    {
        _wave = 2;
        StartWave();
    }

    public void StartWaveThree()
    {
        _wave = 3;
        StartWave();
    }

    public void StartBossWave()
    {
        _wave = 4;
        StartWave();
    }

    public void EnemiesSpawned()
    {
        _enemiesSpawned += 1;
    }

    private void StartWave()
    {
        _enemiesSpawned = 0;
        _spawnManager.Wave(_wave);
        _spawnManager.EnemiesSpawned();
        _spawnManager.StartSpawning();
        _uiManager.WaveStartSequence();
        _gameManager.WaveStart();
        _gameManager.WaveLevel();
        _uiManager.WaveLevel();
    }
    
    public void PlayerDead()
    {
        _playerAlive = false;
    }
}
