using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldBehaviour : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _shieldVisualiser;
    [SerializeField]
    private Collider2D _shieldCollider;
  
    public void EnemyShieldDeactivate()
    {
        _shieldCollider.enabled = false;
        _shieldVisualiser.enabled = false;
    }

    public void EnemyShieldActivate()
    {
        _shieldCollider.enabled = true;
        _shieldVisualiser.enabled = true;
    }
}
