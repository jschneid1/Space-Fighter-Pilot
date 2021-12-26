using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private int powerupID, _powerUpRotSpeed;
    [SerializeField]
    private float _powerUpSpeed = 2.5f;
    private AudioSource _powerUp;

    // Start is called before the first frame update
    void Start()
    {
        _powerUp = GameObject.Find("Power_Up_Sound").GetComponent<AudioSource>();
        if(_powerUp == null)
        {
            Debug.LogError("There is no Powerup sound source");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
        
    {
        if(Input.GetKey(KeyCode.C) && Vector2.Distance(transform.position, _player.transform.position) < 6)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _powerUpSpeed * 1.5f * Time.deltaTime);
        }

        else
        {
            transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime, Space.World);
        }
        
        transform.Rotate(Vector3.forward * -_powerUpRotSpeed * Time.deltaTime);
        if (transform.position.y < -5.6f)
        {
            Destroy(this.gameObject);
        }
    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Player")
        {
                Player player = other.transform.GetComponent<Player>();

                switch(powerupID)
                    {
                    case 0:
                        player.TripleShot();
                        break;
                    case 1:
                        player.Boost();
                        break;
                    case 2:
                        player.Shield();                        
                        break;
                    case 3:
                        player.AmmoRefill();
                        break;
                    case 4:
                        player.FirstAid();
                        break;
                    case 5:
                        player.MissileActive();
                        break;
                    case 6:
                        player.ActivateNegMovement();
                        break;
                    default:
                        Debug.Log("Default value");
                        break;
                    }
                _powerUp.Play();

                Destroy(this.gameObject);
        }

        else if (other.tag is "Power_Up_Destroy")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
