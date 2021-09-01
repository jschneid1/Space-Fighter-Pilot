using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _player;

    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _enemyLaser;

    private Collider2D _enemyCollider;

    [SerializeField]
    private float _speed = 2.0f;
     
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

        StartCoroutine(FireDualLaser());
    }

    // Update is called once per frame
    void Update()
        
        {
        EnemyMovement();
        }
    private void EnemyMovement()
        {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.5f)
            {
            float randomX = Random.Range(-8f, 8.5f);
            transform.position = new Vector3(randomX, 6.2f, 0f);
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

        if (other.tag is "Shield")
        {
            Collider2D shieldCollider = other.transform.GetComponent<Collider2D>();

            if (shieldCollider is null)
            {
                Debug.LogError("There is no Shield Collider component.");
            }
            Explosion();
        }
    }

    IEnumerator FireDualLaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 4));
            Instantiate(_enemyLaser, transform.position, Quaternion.identity);
        }
    }

    private void Explosion()
    {
        Instantiate(_explosionPrefab, this.transform.position + new Vector3(0, 0, -0.2f), Quaternion.identity);
        _enemyCollider.enabled = false;
        _spriteRenderer.enabled = false;
        _speed = 0;
        Destroy(gameObject, 0.2f);
    }
}
