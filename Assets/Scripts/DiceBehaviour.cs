using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceBehaviour : MonoBehaviour
{
    [Header("General")]
    [SerializeField] float _additionalForce = 2f;

    [Header("Body Type")]
    [SerializeField] bool _isHead;
    [SerializeField] bool _isTrunk;
    [SerializeField] bool _isArm;
    [SerializeField] bool _isLeg;

    [Header("Instance oluşturma")]
    Camera _cam;
    Rigidbody _rb;
    List<Transform> _diceNumbers;
    GameManager _gameManager;
    Trajectory _trajectory;
    [SerializeField] Button _button;
    [SerializeField] AudioClip _audioClip;

    [Header("Zar atma")]
    Vector3 _startPoint;
    Vector3 _endPoint;
    Vector3 _direction;
    float _distance;
    Vector3 _force;
    Vector3 _gap;

    [Header("Gelen sayıyı bulma")]
    int _winnerNumber;
    bool _isHit;
    public bool IsHit { get { return _isHit; } }

    bool _isThrowed;
    private bool _isHitToTarget;

    public bool IsThrowed { get { return _isThrowed; } }

    void Awake()
    {
        //getting component etc.
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _gameManager = FindObjectOfType<GameManager>();
        _trajectory = FindObjectOfType<Trajectory>();

        //getting childs
        _diceNumbers = new List<Transform>();
        foreach (Transform child in gameObject.transform)
        {
            _diceNumbers.Add(child);
        }
    }

    void OnMouseDown()
    {
        if (_isThrowed)
        {
            return;
        }

        //Donduruyor, mouse ile zar arası farkı hesaplıyor, ilk tıklanan yeri alıyor
        _rb.isKinematic = true;
        _gap = Input.mousePosition - _cam.WorldToScreenPoint(transform.position);
        _startPoint = _cam.ScreenToWorldPoint(Input.mousePosition - _gap);

        //trajectory açıyoruz
        _trajectory.Show();
    }

    void OnMouseDrag()
    {
        if (_isThrowed)
        {
            return;
        }
        _endPoint = _cam.ScreenToWorldPoint(Input.mousePosition - _gap);
        Debug.DrawLine(_startPoint, _endPoint);

        CalculateForce();

        //trajectory update ediyoruz
        _trajectory.UpdateDots(transform.position, _force);
    }

    void OnMouseUp()
    {
        if (_isThrowed)
        {
            return;
        }
        RollTheDice();

        //trajectory kapıyoruz
        _trajectory.Hide();
        AudioSource.PlayClipAtPoint(_audioClip, _cam.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SuccesfulThrow"))
        {
            _isHitToTarget = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Dice") && !_isHitToTarget)
        {
            _button.gameObject.SetActive(true);
        }

        //çarpınca gelen sayıyla modify ediyoruz diğer scripti ve dondurma olayı ve tekrar saymasın diye çarpılmamış olması lazım
        if (other.gameObject.CompareTag("BottomEdge") || other.gameObject.CompareTag("Dice") && !_isHit)
        {
            _isHit = true;
            _rb.isKinematic = true;

            if (_isHead || _isTrunk)
            {
                _gameManager.ModifyHealth(GetWinnerNumber());
            }
            else if (_isArm)
            {
                _gameManager.ModifyAttack(GetWinnerNumber());
            }
            else if (_isLeg)
            {
                _gameManager.ModifyMoveSpeed(GetWinnerNumber());
            }
        }

        else if (other.gameObject.CompareTag("DestroyerLine"))
        {
            Destroy(gameObject);
        }
    }

    void RollTheDice()
    {
        //Dondurmayı kaldırıyor, force ve torque hesaplayıp fırlatıyor.
        _rb.isKinematic = false;

        _rb.AddForce(_force, ForceMode.Impulse);
        _rb.AddTorque(GetRandomRotation(), ForceMode.Impulse);

        //fırlatıldı
        _isThrowed = true;
    }

    void CalculateForce()
    {
        _distance = Vector3.Distance(_startPoint, _endPoint);
        _direction = (_startPoint - _endPoint).normalized;
        _force = _distance * _direction * _additionalForce;
    }

    int GetWinnerNumber()
    {
        int winnerNumber = 0;
        if (_isHit)
        {
            float _lowestZ = 0;
            foreach (Transform child in _diceNumbers)
            {
                if (child.position.z <= _lowestZ)
                {
                    _lowestZ = child.position.z;
                    winnerNumber = _diceNumbers.IndexOf(child) + 1;

                    Debug.Log(_lowestZ);
                    Debug.Log(name + " " + winnerNumber);
                }
            }
        }
        return winnerNumber;
    }

    Vector3 GetRandomRotation()
    {
        return new Vector3(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));
    }
}
