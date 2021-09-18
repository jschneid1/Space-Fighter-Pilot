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
    private Text _scoreText, _gameOverText, _restartLevelText, _ammoText, _ammoGLText, _ammoVLText, _ammoOutText, _fireMissileText;
    
    private GameManager _gameManager;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //assign text component to handle
        _scoreText.text = "Score: " + 0;
        _gameOverText.enabled = false;
        _restartLevelText.enabled = false;
        _ammoText.text = "Ammo: " + 15;
        _ammoGLText.enabled = false;
        _ammoVLText.enabled = false;
        _ammoOutText.enabled = false;
        _fireMissileText.enabled = false;

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

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
        _ammoText.text = "Ammo: " + playerAmmo.ToString();

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
}
