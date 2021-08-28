using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _shieldHits;
    [SerializeField]
    private SpriteRenderer _shieldVisualiser;
    private Collider2D _shieldCollider;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _shieldCollider = gameObject.GetComponent<Collider2D>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void ShieldHits()
    {
        _shieldHits = 3;
        _shieldVisualiser.color = new Color(1, 0.9858491f, 0.9863342f, 1);
        _shieldVisualiser.enabled = true;
        _shieldCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag is "Enemy" | other.tag is "Enemy_Weapon")
        {
            Destroy(other.gameObject);
            if (_shieldHits == 3)
            {
                _shieldHits--;
                _shieldVisualiser.color = new Color(0.8679245f, 0.2684373f, 0.1678533f, 1);
                return;
            }

            if (_shieldHits == 2)
            {
                _shieldHits--;
                _shieldVisualiser.color = new Color(0.7830189f, 0.09972411f, 0.1211983f, 1);
                return;
            }

            if (_shieldHits == 1)
            {
                if (other.tag is "Enemy")
                    {
                    _shieldHits--;
                    _shieldVisualiser.enabled = false;
                    _shieldCollider.enabled = false;
                    _player.DeactivateShield();
                    }
                else
                    {
                    _shieldHits--;
                    _shieldVisualiser.enabled = false;
                    }
                return;
             }

            if (_shieldHits < 1)
                {
                _shieldCollider.enabled = false;
                _player.DeactivateShield();
                return;
            }
        }
    }
}
