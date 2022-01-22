using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform _closestEnemy = null;
    [SerializeField]
    private GameObject _missile;

    // Update is called once per frame
    void Update()
    {
        _closestEnemy = GetClosestEnemy();

        if (_closestEnemy != null)
        {
            transform.up = _closestEnemy.position - transform.position;
        }
        else
        {
            transform.rotation =  Quaternion.LookRotation(transform.forward);
        }
    }

    public void FireMissile()
        {
            Instantiate(_missile, transform.position + new Vector3(0.017f, -0.14f, 0f), Quaternion.identity);
        }

    public Transform GetClosestEnemy()
    {
        GameObject[] enemies;
        GameObject _bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
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
}
