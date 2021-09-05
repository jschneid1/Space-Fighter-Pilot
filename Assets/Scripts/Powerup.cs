using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField] //0 = triple shot, 1 = speed, 2 = shield
    private int powerupID;
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
    }

    // Update is called once per frame
    void Update()
        
    {
        transform.Translate(Vector3.down * _powerUpSpeed * Time.deltaTime);
        if(transform.position.y < -5.6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Player")
        {
                Player player = other.transform.GetComponent<Player>();
                if(player != null)

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
                    default:
                        Debug.Log("Default value");
                        break;
                    }
                _powerUp.Play();

                Destroy(this.gameObject);
        }
    }
}
