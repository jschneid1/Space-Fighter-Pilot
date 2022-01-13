using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Collider2D _enemyCollider;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private int _bossHealth = 20;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (_spriteRenderer is null)
        {
            Debug.LogError("The Enemy Sprite Renderer is NULL");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemyCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0f, 0f) , _speed * Time.deltaTime);

        if(_bossHealth <= 0)
        {
            BossDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag is "Laser")
        {
            Destroy(other.gameObject);
            _player.Score(20);
            Damage();
        }

        else if (other.tag is "Player")
        {
            _player.Damage();
            Damage();
        }

        else if (other.tag is "Shield")
        {
            Damage();
        }
    }
   
    private void Damage()
    {
        _bossHealth -= 1;
    }

    private void BossDeath()
    {
        Instantiate(_explosionPrefab, this.transform.position + new Vector3(0, 0, -0.2f), Quaternion.identity);
        _enemyCollider.enabled = false;
        _spriteRenderer.enabled = false;
        Destroy(gameObject, 0.005f);
    }
}
