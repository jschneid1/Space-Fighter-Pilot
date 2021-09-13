﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _missileSpeed = 5.0f;
    [SerializeField]
    private Transform _closestEnemy = null;
    private RocketLauncherBehaviour _rocketLauncherBehaviour;
    // Start is called before the first frame update
    void Start()
    {
         _rocketLauncherBehaviour = GameObject.Find("RocketLauncher").GetComponent<RocketLauncherBehaviour>();
         if(_rocketLauncherBehaviour is null)
             {
             Debug.LogError("There is no Rocket Launcher Behaviour");
             }
         _closestEnemy = _rocketLauncherBehaviour.GetClosestEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (_closestEnemy != null)
        {
            transform.up = _closestEnemy.position - transform.position;
        }

        MissileMovement();
    }

    private void MissileMovement()
    {
        transform.Translate(Vector3.up * _missileSpeed * Time.deltaTime);
        if (transform.position.y < -5.4f | transform.position.y > 5.8f | transform.position.x < -9.5f | transform.position.x > 9.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
