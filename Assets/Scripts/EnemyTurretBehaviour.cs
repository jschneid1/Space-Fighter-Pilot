using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _laser;
    //private MissileBehaviour _missileBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireLaser());
    }

    // Update is called once per frame
    void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player != null)
        {
            transform.up = transform.position -_player.transform.position;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(transform.forward);
        }
    }

    IEnumerator FireLaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 4));
            Instantiate(_laser, transform.position, transform.rotation);
        }
    }
}
