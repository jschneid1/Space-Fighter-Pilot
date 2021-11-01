using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _player;

    private SpriteRenderer _spriteRenderer;

    private WaveManager _waveManager;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _enemyLaser;

    private Collider2D _enemyCollider;

    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private bool _altMovement = false, _altEnemy = false;
    
    [SerializeField]
    private int _dirChange;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyCollider = GetComponent<Collider2D>();

        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if(_spriteRenderer is null)
        {
            Debug.LogError("The Enemy Sprite Renderer is NULL");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();

       _waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();

       _waveManager.EnemiesSpawned();

        StartCoroutine(FireDualLaser());
    }

    // Update is called once per frame
    void Update()
        
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        if(_altMovement is true)
            {
                float horMovement = _speed * _dirChange;
                float verMovement = -_speed;
                Vector3 direction = new Vector3(horMovement, verMovement, 0);
                transform.Translate(direction  *  Time.deltaTime);
            }

        else
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }

        if (transform.position.y < -5.7f)
            {
            float randomX = Random.Range(-8f, 8.5f);
            transform.position = new Vector3(randomX, 6.2f, 0f);
            }

        if(transform.position.x < -8.5f)
        {
            transform.position = new Vector3(-8.5f, transform.position.y, 0f);
        }

        if (transform.position.x > 9.0f)
        {
            transform.position = new Vector3(9.0f, transform.position.y, 0f);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag is "Player")
        {
            _player.Damage();
            Explosion();
        }

       else if (other.tag is "Laser")
        {
            Destroy(other.gameObject);
            if (_player is null)
            {
                Debug.LogError("There is no Player component for score");
            }
            if (_player != null)
            {
                _player.Score(10);
            }
            Explosion();
        }

        else if (other.tag is "Shield")
        {
            Explosion();
        }
        
    }

    IEnumerator FireDualLaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 4));
            if(_altEnemy is true)
            {
                Instantiate(_enemyLaser, transform.position + new Vector3(0, -0.9f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_enemyLaser, transform.position, Quaternion.identity);
            }
            
        }
    }

    IEnumerator AltMovement()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(1, 3));
            _altMovement = false;
            _dirChange = Random.Range(0, 2) * 2 - 1;
            yield return new WaitForSeconds(Random.Range(1, 3));
            _altMovement = true;
        }
    }

    IEnumerator AltMovement1()
    {
        while (true)
        {
            _dirChange = Random.Range(0, 2) * 2 - 1;
            yield return new WaitForSeconds(Random.Range(1, 3));
            _dirChange = _dirChange * -1;
            yield return new WaitForSeconds(Random.Range(1, 3));
        }
    }


    private void Explosion()
    {
        Instantiate(_explosionPrefab, this.transform.position + new Vector3(0, 0, -0.2f), Quaternion.identity);
        _enemyCollider.enabled = false;
        _spriteRenderer.enabled = false;
        _speed = 0;
        Destroy(gameObject, 0.005f);
    }

    public void AlternativeMovement()
    {
        _altMovement = true;
        StartCoroutine(AltMovement());
    }

    public void AltMoveOne()
    {
        _altMovement = true;
        StartCoroutine(AltMovement1());
    }
}
