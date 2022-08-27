using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] int _enemyHealth = 100;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _bulletSpeed = 2f;
    [SerializeField] float _enemyFiringRate = 2f;
    [SerializeField] float _xGapToStartFiring = 15f;

    MainCharacter _mainCharacter;
    Vector3 _direction;
    Vector3 _gap;
    Coroutine _firingCoroutine;
    int _enemyHealthAtStart;

    void Awake()
    {
        _mainCharacter = FindObjectOfType<MainCharacter>();
        _enemyHealthAtStart = _enemyHealth;
    }

    void Update()
    {
        if (_mainCharacter != null)
        {
            _gap = _mainCharacter.transform.position - transform.position;
        }
        _direction = _gap.normalized;

        //sağına soluna geçmeyle değişen gap
        // burası da bozuk içerdeki else if e hiç girmeyebilir.
        if (Mathf.Sign(_gap.x) < 0)
        {
            if (Mathf.Sign(_xGapToStartFiring) > 0)
            {
                _xGapToStartFiring *= -1;
            }

            if (_gap.x > _xGapToStartFiring && _firingCoroutine == null)
            {
                Debug.Log("SOL ateş ediyor");
                _firingCoroutine = StartCoroutine(FireContinuously());
            }
            else if (_gap.x < _xGapToStartFiring && _firingCoroutine != null)
            {
                Debug.Log("SOL Durduruyor");
                StopCoroutine(_firingCoroutine);
                _firingCoroutine = null;
            }
        }
        else
        {
            if (Mathf.Sign(_xGapToStartFiring) < 0)
            {
                _xGapToStartFiring *= -1;
            }

            if (_gap.x < _xGapToStartFiring && _firingCoroutine == null)
            {
                Debug.Log("SAĞ ateş ediyor");
                _firingCoroutine = StartCoroutine(FireContinuously());
            }
            else if (_gap.x > _xGapToStartFiring && _firingCoroutine != null)
            {
                Debug.Log("SAĞ durduruyor");
                StopCoroutine(_firingCoroutine);
                _firingCoroutine = null;
            }
        }

        //oyuncuyla arasında belli bir mesafe olduğunda ateş etmeye başlasın
        //mesafe yaklaştıysa ve coroutine null ise ateş etmeye başla else stop coroutine

        CheckHealth();
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject clone = Instantiate(_bullet, transform.position, _bullet.transform.rotation);
            clone.GetComponent<Rigidbody>().velocity = _direction * _bulletSpeed;
            Destroy(clone, 3f);

            yield return new WaitForSeconds(_enemyFiringRate);
        }
    }

    public void GetHit(int value)
    {
        _enemyHealth -= value;
    }

    void CheckHealth()
    {
        if (_enemyHealth <= 0)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
                return;

            gameManager.IncreaseScore(_enemyHealthAtStart);

            Destroy(gameObject);
        }
    }
}
