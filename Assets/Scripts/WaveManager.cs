using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    

    [SerializeField]
    private int  _enemiesSpawned, _coRoutineUse, _wave;

    public int waveLevel;
    
    [SerializeField]
    private SpriteRenderer[] _enemyArray;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }
        if (_uiManager is null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
        _wave = 1;
    }

    // Update is called once per frame
    void Update()
    {
        _enemyArray = GameObject.Find("Enemy Container").GetComponentsInChildren<SpriteRenderer>();
        _enemiesSpawned = _spawnManager.enemiesSpawned;
        waveLevel = _wave;
        
        if(_enemiesSpawned == 5 && waveLevel == 1)
        {
            _spawnManager.StopEnemySpawn();
            if (_enemyArray.Length  == 0 && _coRoutineUse == 0)
            {
                _uiManager.WaveOverSequence();
                _spawnManager.StopPowerUpSpawn();
                _coRoutineUse += 1;
            }
        }

        if(_enemiesSpawned == 10 && waveLevel == 2)
        {
            _spawnManager.StopEnemySpawn();
            if (_enemyArray.Length == 0 && _coRoutineUse == 1)
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
        _spawnManager.enemiesSpawned = 0;
        _spawnManager.StartSpawning();
        _uiManager.WaveStartSequence();
    }

    public void StartWaveThree()
    {
        _wave = 3;
        _spawnManager.enemiesSpawned = 0;
        _spawnManager.StartSpawning();
        _uiManager.WaveStartSequence();
    }
}
