using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver, _waveFinished;

    private WaveManager _waveManager;

    private void Start()
    {
        _waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.R) && _isGameOver is true)
        {
            SceneManager.LoadScene(2); //Start Game Scene
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.Return) && _waveFinished is true)
        {
            if(_waveManager.waveLevel == 1)
            {
                _waveManager.StartWaveTwo();
            }

            else if(_waveManager.waveLevel == 2)
            {
                _waveManager.StartWaveThree();
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
}
