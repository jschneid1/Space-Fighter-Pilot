using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _asteroidRotSpeed = 50f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
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
    [SerializeField]
    private Text _instructionText;
    private SpriteRenderer _asteroidRenderer;

    // Start is called before the first frame update
    void Start()

    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }

        _asteroidRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (_asteroidRenderer == null)
        {
            Debug.LogError("Asteroid Render doesn't exist");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //rotate asteroid on z axis
        transform.Rotate(0, 0, -_asteroidRotSpeed * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Laser")
        {
            Destroy(other.gameObject);
            Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
                /*_spawnManager.StartSpawning();
                SceneManager.LoadScene(2);
                _LivesImg.enabled = true;
                _instructionText.enabled = false;
                _scoreText.enabled = true;*/
                _asteroidRenderer.enabled = false;
            //Destroy(this.gameObject, 0.2f);
            StartCoroutine(LoadGameScene());
            
        }

       else if (other.tag is "Player")
        {
            Instantiate(_explosionPrefab, other.transform.position, Quaternion.identity);
            StartCoroutine(ExplosionTime());
            Destroy(other.gameObject, 0.1f);
        }
    }

    IEnumerator ExplosionTime()
    {
        yield return new WaitForSeconds(2.7f);
        SceneManager.LoadScene(0);
    }

    IEnumerator LoadGameScene()
    {
        
        yield return new WaitForSeconds(2.7f);
        SceneManager.LoadScene(2);
        _spawnManager.StartSpawning();
    }

}
