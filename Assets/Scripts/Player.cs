using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float _speed = 3.5f;
    [SerializeField]
    private float _boostSpeed = 8.5f;
    [SerializeField]
    private float _thrusterSpeed = 2.5f;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private float _missleFireRate = 1.0f;
    [SerializeField]
    private float _missileCanFire = -1f; 

    private AudioSource _laserSource;
    
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;
    [SerializeField]
    private int _ammoCount = 15;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _playerExplosion;
    [SerializeField]
    private GameObject _leftEngineFire, _rightEngineFire, _rocketLauncher;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private ShieldBehaviour _shieldBehaviour;
    private RocketLauncherBehaviour _rocketLauncherBehaviour;
    private CameraBehaviour _cameraBehaviour;

    private bool _tripleShotActive = false;
    private bool _boostActive;
    [SerializeField]
    private bool _shieldActive = false;
    [SerializeField]
    private bool _missileActive = false;
    [SerializeField]
    private bool _thrusterActive = true;

    private SpriteRenderer _playerSpriteRenderer;
    [SerializeField]
    private SpriteRenderer _thruster, _rocketLauncherSpriteRenderer; 
    
    private PolygonCollider2D _playerCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -3, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _laserSource = GameObject.Find("Laser_Shot").GetComponent<AudioSource>();
        _playerCollider = gameObject.GetComponent<PolygonCollider2D>();
        _playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _shieldBehaviour = gameObject.GetComponentInChildren<ShieldBehaviour>();
        _rocketLauncherBehaviour = gameObject.GetComponentInChildren<RocketLauncherBehaviour>();
        _cameraBehaviour = GameObject.Find("Main Camera").GetComponent<CameraBehaviour>();

        if (_cameraBehaviour is null)
        {
            Debug.LogError("The Camera Behaviour Behaviour is NULL");
        }

        if (_rocketLauncherBehaviour is null)
        {
            Debug.LogError("The Rocket Launcher Behaviour is NULL");
        }
        if (_shieldBehaviour is null)
        {
            Debug.LogError("The Shield Behaviour is NULL");
        }
        
        if (_spawnManager is null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if(_uiManager is null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

        if (_laserSource == null)
        {
            Debug.LogError("Laser audio source is null");
        }

        _rightEngineFire.SetActive(false);
        _leftEngineFire.SetActive(false);
        _rocketLauncherSpriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        if(_ammoCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {
                _canFire = Time.time + _fireRate;
                fireLaser();
                AmmoCount(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) && Time.time > _missileCanFire)
        {
            _missileCanFire = Time.time + _missleFireRate;
            if (_missileActive is true)
            {
                _rocketLauncherBehaviour.FireMissile();
            }
        }
    }

    void playerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 4.1f), 0);

        if(Input.GetKey(KeyCode.LeftShift) && _thrusterActive is true)
            {
                transform.Translate(direction * _thrusterSpeed * Time.deltaTime);
            }

        if (_boostActive is true)
        {
            transform.Translate(direction * _boostSpeed * Time.deltaTime);
        }

        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);   
        }


        if (transform.position.x <= -9.3f)
        {
            transform.position = new Vector3(9.3f, transform.position.y, 0);
        }

        else if (transform.position.x >= 9.3f)
        {
            transform.position = new Vector3(-9.3f, transform.position.y, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Enemy_Weapon" & _shieldActive is false)
        {
            Destroy(other.gameObject);
            Damage();
        }
    }

    void fireLaser()
    {
        if (_tripleShotActive is true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.898f, 0), Quaternion.identity);
        }

        _laserSource.Play();
    }

    public void Damage()
    {
        _lives--;
      
        if (_lives == 2)
        {
            _rightEngineFire.SetActive(true);
        }

        else if(_lives == 1)
        {
            _leftEngineFire.SetActive(true);
        }      
        
        else if(_lives < 1)
        {
            _lives = 0;
            Death();
        }
        _uiManager.UILivesUpdate(_lives);
        _cameraBehaviour.StartShake(0.5f, 0.4f);
    }

    private void Death()
    {
        _playerCollider.enabled = false;
        _spawnManager.onPlayerDeath();
        Instantiate(_playerExplosion, transform.position, Quaternion.identity);
        _playerSpriteRenderer.enabled = false;
        _rightEngineFire.SetActive(false);
        _leftEngineFire.SetActive(false);
        _thruster.enabled = false;
        _rocketLauncherSpriteRenderer.enabled = false;
        _uiManager.UILivesUpdate(_lives);
    }

    public void TripleShot()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotActive());
    }

    IEnumerator TripleShotActive()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }

    public void Boost()
    {
        _boostActive = true;
        StartCoroutine(BoostActive());
    }

    IEnumerator BoostActive()
    {
        yield return new WaitForSeconds(5.0f);
        _boostActive = false;
    }

    public void MissileActive()
    {
        _missileActive = true;
        _rocketLauncherSpriteRenderer.enabled = true;
        StartCoroutine(MissileDeactivate());
        StartCoroutine(_uiManager.MissileFire());
    }

    IEnumerator MissileDeactivate()
    {
        yield return new WaitForSeconds(5.0f);
        _missileActive = false;
        _rocketLauncherSpriteRenderer.enabled = false;
    }

    public void ThrusterAcivate()
    {
        _thrusterActive = true;
    }

    public void ThrusterDeactivate()
    {
        _thrusterActive = false;
    }

    public void Shield()
       
    {
        _shieldBehaviour.ShieldHits();
        _shieldActive = true;
    }
    
    public void Score(int points)
    {
        _score += points;
        _uiManager.UIScoreUpdate(_score);
    }

    public void DeactivateShield()
    {
        _shieldActive = false;
    }

    public void AmmoCount(int ammo)
    {
        _ammoCount -= ammo;
        _uiManager.UIAmmoUpdate(_ammoCount);
    }

    public void AmmoRefill()
    {
        _ammoCount = 15;
        _uiManager.UIAmmoUpdate(_ammoCount);
    }

    public void FirstAid()
    {
        if(_lives < 3)
        {
            _lives++;
            if (_lives == 3)
            {
                _rightEngineFire.SetActive(false);
            }

            else if (_lives == 2)
            {
                _leftEngineFire.SetActive(false);
            }
        }

        else
        {
            _lives = 3;
        }
        
        _uiManager.UILivesUpdate(_lives);   
    }
}


