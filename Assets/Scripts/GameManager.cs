using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver, _waveFinished;

    private WaveManager _waveManager;

    private int _wave;

    private void Start()
    {
        _waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        _wave = 1;
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(2); //Start Game Scene
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.Return) && _waveFinished == true)
        {
            if(_wave == 1)
            {
                _waveManager.StartWaveTwo();
            }

            else if(_wave == 2)
            {
                _waveManager.StartWaveThree();
            }

            else if (_wave == 3)
            {
                _waveManager.StartBossWave();
            }
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void WaveFinished()
    {
        _waveFinished = true;
    }

    public void WaveLevel()
    {
        _wave += 1;
    }

    public void WaveStart()
    {
        _waveFinished = false;
    }
}
