using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBackFire : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _enemyWeaponSpriteRenderer;

    [SerializeField]
    private GameObject _laser;

    private Coroutine _activateTurretCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        _enemyWeaponSpriteRenderer.enabled = false;
    }

    public void ActivateEnemyTurret()
    {
        StartCoroutine(EnemyTurretActive());
        FireLaser();
    }

    private void FireLaser()
    {
        Instantiate(_laser, transform.position, transform.rotation);

    }

    IEnumerator EnemyTurretActive()
    {
        _enemyWeaponSpriteRenderer.enabled = true;
        yield return new WaitForSeconds(2.0f);
        _enemyWeaponSpriteRenderer.enabled = false;
    }
}
