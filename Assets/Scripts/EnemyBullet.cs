using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int _bulletDamage = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MainCharacter mainCharacter = other.GetComponent<MainCharacter>();
            mainCharacter.GetHit(_bulletDamage);
            Destroy(gameObject);
        }
        if(other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

    }
}
