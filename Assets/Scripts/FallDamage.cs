using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    [SerializeField] int _damage = 20;
    [SerializeField] float _delay = 0.5f;
    bool _isHit;
    Collider _other;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isHit)
        {
            _other = other;
            _isHit = true;
            Invoke("GiveDamage", _delay);
        }
    }

    void GiveDamage()
    {
        _other.GetComponent<MainCharacter>().GetHit(_damage);
    }
}
