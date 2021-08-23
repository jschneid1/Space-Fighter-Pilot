using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartLevelText;

    private GameManager _gameManager;
    private SpawnManager _spawnManager;



    // Start is called before the first frame update
    void Start()
    {
        //assign text component to handle
        _scoreText.text = "Score: " + 0;
        _gameOverText.enabled = false;
        _restartLevelText.enabled = false;

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
    }

   
   public void UIScoreUpdate(int playerScore)
    {     
            _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UILivesUpdate(int currentLives)
    {
        _LivesImg.sprite = _livesSprites[currentLives];

        if(currentLives == 0)
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
    
}
