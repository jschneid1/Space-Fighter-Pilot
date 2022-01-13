using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _player;
    private Transform _powerup = null, _laserToDodge = null;
    private EnemyShieldBehaviour _enemyShield;
    private EnemyWeaponBackFire _enemyWeaponBackFire;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private WaveManager _waveManager;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _enemyLaser;
    [SerializeField]
    private GameObject _altEnemyLaser;
    [SerializeField]
    private Collider2D _enemyCollider;

    [SerializeField]
    private float _speed = 2.0f, _playerDistance, _ramSpeed = 2.0f, _dodgeSpeed = 1.5f;

    [SerializeField]
    private bool _altMovement = false, _altEnemy = false, _enemyShieldActive, _enemyRamActive, _enemyRam, _enemyWeaponActive, _powerUpDestroyActive, _dodgeAbility = false;
    
    [SerializeField]
    private int _dirChange, _dodgeDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyCollider = GetComponent<PolygonCollider2D>();
        _enemyShield = gameObject.GetComponentInChildren<EnemyShieldBehaviour>();
        _enemyWeaponBackFire = gameObject.GetComponentInChildren<EnemyWeaponBackFire>();

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
        _playerDistance = Vector2.Distance(_player.transform.position, this.transform.position);

        _powerup = GetPowerUp();

        _laserToDodge = GetLaserToDodge();
        
         EnemyMovement();
    }

    private void EnemyMovement()
    {
        if (_playerDistance < 5 && _enemyRamActive is true && transform.position.y > -1.05f)
        {
            _speed = 0;
            transform.up = transform.position - _player.transform.position;
            EnemyRam();
            _enemyRamActive = false;
        }

        if(_enemyRam is true)
        {
            transform.Translate(Vector3.down * (_ramSpeed * 2) * Time.deltaTime);
        }

        if (_altMovement is true)
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

        if (transform.position.y < -5.65f && _enemyRam is true)
        {
            transform.rotation = Quaternion.identity;
            EnemyRamFalse();
        }
        
            if (transform.position.x < -8.5f)
        {
            transform.position = new Vector3(-8.5f, transform.position.y, 0f);
            transform.rotation = Quaternion.identity;
        }

        if (transform.position.x > 9.0f)
        {
            transform.position = new Vector3(9.0f, transform.position.y, 0f);
            transform.rotation = Quaternion.identity;
        }

        if (_player.transform.position.x > transform.position.x - 0.2 && _player.transform.position.x < transform.position.x + 0.2 && transform.position.y < _player.transform.position.y - 1.0f && _enemyWeaponActive is true)
        {
            _enemyWeaponBackFire.ActivateEnemyTurret();
            _enemyWeaponActive = false;
        }

        if(_powerup != null && _powerUpDestroyActive is true && _powerup.transform.position.y < 2.0f)
        {
            {
                Instantiate(_altEnemyLaser, transform.position, transform.rotation);
                _powerUpDestroyActive = false;
            }
        }

        if(_laserToDodge != null  && _dodgeAbility is true)
        {
            if(_laserToDodge.position.x > transform.position.x - 0.64 && _laserToDodge.position.x <= transform.position.x)
            {
                _dodgeDirection = 1;;
                DodgeMovement();
            }

            else if(_laserToDodge.position.x < transform.position.x + 0.64  && _laserToDodge.position.x >= transform.position.x)
            {
                _dodgeDirection = -1;
                DodgeMovement();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (_enemyShieldActive is true)
            {
                if (other.tag is "Player")
                {
                    _player.Damage();
                    DeactivateShield();
                }

                else if (other.tag is "Laser")
                {
                    Destroy(other.gameObject);
                    DeactivateShield();
                }

                else if (other.tag is "Shield")
                {
                    DeactivateShield();
                }
            }

            else
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
    }

    IEnumerator FireDualLaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 4));
            if(_altEnemy is true)
            {
                Instantiate(_enemyLaser, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(_enemyLaser, transform.position, transform.rotation);
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

    public void EnemyShieldActivate()
    {
        _enemyShieldActive = true;
        _enemyShield.EnemyShieldActivate();
    }

    private void DeactivateShield()
    {
        _enemyShield.EnemyShieldDeactivate();
        _enemyShieldActive = false;
    }

    public void ActivateRam()
    {
        _enemyRamActive = true;
    }

    private void EnemyRam()
    {
        _enemyRam = true;
    }

    private void EnemyRamFalse()
    {
        _enemyRam = false;
        _speed = 2;
    }

    public void ActivateBackTurret()
    {
        _enemyWeaponActive = true;
    }

    public void ActivateDodgeAbility()
    {
        _dodgeAbility = true;
    }

    public void ActivatePowerUpDestroy()
    {
        _powerUpDestroyActive = true;
    }

    private Transform GetPowerUp()
    {
        GameObject[] powerups;
        GameObject _bestTarget = null;
        powerups = GameObject.FindGameObjectsWithTag("Power_Up");
        foreach (GameObject potentialTarget in powerups)
        {
                if (potentialTarget.transform.position.x > transform.position.x - 0.2 && potentialTarget.transform.position.x < transform.position.x + 0.2 && transform.position.y > potentialTarget.transform.position.y + 1.0f)
                {
                    _bestTarget = potentialTarget;
                }
                
        }
            if (_bestTarget != null)
                {
                    return _bestTarget.transform;
                }
        
            else
                {
                     return null;
                }
    }

    private Transform GetLaserToDodge()
    {
        GameObject[] lasers;
        GameObject _bestTarget = null;
        lasers = GameObject.FindGameObjectsWithTag("Laser");
        foreach (GameObject potentialTarget in lasers)
        {
            if (potentialTarget.transform.position.x > transform.position.x - 0.64 && potentialTarget.transform.position.x < transform.position.x + 0.64 && transform.position.y - potentialTarget.transform.position.y < 5.0f)
            {
                _bestTarget = potentialTarget;
            }

        }
        if (_bestTarget != null)
        {
            return _bestTarget.transform;
        }

        else
        {
            return null;
        }
    }

    private void DodgeMovement()
    {
            float horMovement = _dodgeDirection * _dodgeSpeed;
            float verMovement = -_speed;
            Vector3 direction = new Vector3(horMovement, verMovement, 0);
            transform.Translate(direction * Time.deltaTime);
    }
}


