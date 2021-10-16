using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image _LivesImg;

    [SerializeField]
    private Sprite[] _livesSprites;

    [SerializeField]
    private Text _scoreText, _gameOverText, _restartLevelText, _ammoText, _ammoGLText, _ammoVLText, _ammoOutText, _fireMissileText, _levelOverText, _nextLevelText;

    private Player _player;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private WaveManager _waveManager;

    // Start is called before the first frame update
    void Start()
    {
        //assign text component to handle
        _scoreText.text = "Score: " + 0;
        _gameOverText.enabled = false;
        _restartLevelText.enabled = false;
        _ammoText.text = "Ammo: 15 / 15";
        _ammoGLText.enabled = false;
        _ammoVLText.enabled = false;
        _ammoOutText.enabled = false;
        _fireMissileText.enabled = false;
        _levelOverText.enabled = false;
        _nextLevelText.enabled = false;

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }
        if (_gameManager is null)
        {
            Debug.LogError("The Game Manager is NULL");
        }
        _spawnManager.StartSpawning();
        StartCoroutine(ThrusterUse());
    }

   public void UIScoreUpdate(int playerScore)
    {     
            _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UIAmmoUpdate(int playerAmmo)
    {
        _ammoText.text = "Ammo: " + playerAmmo.ToString() + " / 15";

        if (playerAmmo == 10)
        {
            _ammoGLText.enabled = true;
            StartCoroutine(AmmoGLRoutine());
        }

        else if(playerAmmo == 5)
        {
            _ammoVLText.enabled = true;
            StartCoroutine(AmmoVLRoutine());
        }

        else if(playerAmmo == 0)
        {
            _ammoOutText.enabled = true;
            StartCoroutine(AmmoOutRoutine());
        }
    }

    public void UILivesUpdate(int currentLives)
    {
        _LivesImg.sprite = _livesSprites[currentLives];

        if(currentLives < 1)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.enabled = true;
        _restartLevelText.enabled = true;
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOverText.enabled = false;
            yield return new WaitForSeconds(0.5f);
            _gameOverText.enabled = true;
        }
    }

    IEnumerator AmmoGLRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        _ammoGLText.enabled = false;
    }

    IEnumerator AmmoVLRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        _ammoVLText.enabled = false;
    }

    IEnumerator AmmoOutRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        _ammoOutText.enabled = false;
    }

    public IEnumerator MissileFire()
    {
        _fireMissileText.enabled = true;
        yield return new WaitForSeconds(2.0f);
        _fireMissileText.enabled = false;
    }

    IEnumerator ThrusterUse()
    {
        _fireMissileText.enabled = true;
        _fireMissileText.text = "Press the 'Left Shift' to use Thrusters";
        yield return new WaitForSeconds(2.0f);
        _fireMissileText.enabled = false;
        _fireMissileText.text = "Press 'Left Alt' to Fire Missile";
    }

    IEnumerator WaveOver()
    {
        {
            _levelOverText.enabled = true;
            _levelOverText.text = "Congratulations you have completed Wave " + _waveManager.waveLevel.ToString();
            yield return new WaitForSeconds(4.0f);
            _levelOverText.enabled = false;
        }

        if(_waveManager.waveLevel == 1)
        {
            _nextLevelText.enabled = true;
            _nextLevelText.text = "Nice work on surviving the first wave, as a reward you will magically have any damage repaired, and a full ammo count upon starting the next wave.";
            yield return new WaitForSeconds(7.0f);
            _nextLevelText.text = "Do not expect this from now on.                 Press Enter to Start the next wave.";
        }

        if (_waveManager.waveLevel == 2)
        {
            _nextLevelText.enabled = true;
            _nextLevelText.text = "No way!!!  You got this far, I gave you less credit than you deserve. Well Done.";
            yield return new WaitForSeconds(7.0f);
            _nextLevelText.text = "As promised no refill or repair.                 Press Enter to Start the next wave.";
        }
    }

    public void WaveOverSequence()
    {
            StartCoroutine(WaveOver());
            _gameManager.WaveFinished();
    }

    public void WaveStartSequence()
    {
        _nextLevelText.enabled = false;
        if(_waveManager.waveLevel == 1)
        {
            _player.RestoreHealth();
        }
    }
}
