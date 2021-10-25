﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private GameManager _gameManager;
    //private Enemy _enemy;
    

    [SerializeField]
    private int _enemiesSpawned, _coRoutineUse, _wave, _enemiesAlive;
        
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
        if (_uiManager is null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
        if (_gameManager is null)
        {
            Debug.LogError("The Game Manager is NULL");
        }

        _wave = 1;
        _spawnManager.Wave(_wave);
    }

    // Update is called once per frame
    void Update()
    {
        _enemyArray = GameObject.Find("SpawnManager").GetComponentsInChildren<SpriteRenderer>();
        _enemiesAlive = _enemyArray.Length;

        if (_enemiesSpawned == 5 && _wave == 1)
        {
            _spawnManager.StopEnemySpawn();
            if (_enemiesAlive  == 0 && _coRoutineUse == 0)
            {
                _uiManager.WaveOverSequence();
                _spawnManager.StopPowerUpSpawn();
                _coRoutineUse += 1;
            }
        }

        if(_enemiesSpawned == 10 && _wave == 2)
        {
            _spawnManager.StopEnemySpawn();
            if (_enemiesAlive == 0 && _coRoutineUse == 1)
            {
                _uiManager.WaveOverSequence();
                _spawnManager.StopPowerUpSpawn();
                _coRoutineUse += 1;
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
        _spawnManager.StartAltEnemySpawn();
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
}
