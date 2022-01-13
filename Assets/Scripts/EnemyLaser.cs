using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]
    private float _enemyLaserSpeed = 6.0f;

    // Update is called once per frame
    void Update()
    {
            transform.Translate(Vector3.down * _enemyLaserSpeed * Time.deltaTime);
        
        if (transform.position.y < -5.5f || transform.position.x < -8.5f || transform.position.x > 9.0f || transform.position.y > 6.5f)
        {
            if (transform.parent == true)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}