using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainCharacter : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float grav;
    Vector2 mousePos;
    [SerializeField] GameObject checkGroundObj;
    [SerializeField] bool onGround;
    [SerializeField] GameObject[] vucutParcalari;
    [SerializeField] GameObject[] vucutParcalariParentleri;
    [SerializeField] TextMeshProUGUI _healthTMP;
    [SerializeField] TextMeshProUGUI _canvass;
    GameManager _gameManager;
    float screenMid;
    float _jumpSpeed;

    [Header("DiceMan'imizin özelliklerini doldurma")]

    //buralarla oyna
    int _health;
    int _attack;
    int _moveSpeed;

    int _score;

    [SerializeField] float _gameTime = 120f;



    private void Awake()
    {
        screenMid = Screen.width / 2;
        rb = GetComponent<Rigidbody>();

        //canları geçiriyorum
        _gameManager = FindObjectOfType<GameManager>();

    }
    void Start()
    {
        vucutParcalari[1].transform.parent = vucutParcalariParentleri[1].transform;
        vucutParcalari[2].transform.parent = vucutParcalariParentleri[1].transform;

        _health = _gameManager.Heatlh;
        _attack = _gameManager.Attack;
        _moveSpeed = _gameManager.MoveSpeed;
        _jumpSpeed = _moveSpeed * 2;
    }

    private void Update()
    {
        CheckGameTime();

        if (_gameManager == null)
            return;
        _score = _gameManager.Score;
    }

    void CheckGameTime()
    {
        _gameTime -= Time.deltaTime;
        if (_gameTime < 0)
        {
            _canvass.gameObject.SetActive(true);
            _canvass.text = "Score: " + _score;
            Destroy(_gameManager.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-_moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(_moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpSpeed);
        }
        if (!onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - grav);


            if (transform.position.y < -30)
            {
                GameRestart();
            }
        }
        mousePos = Input.mousePosition;
        if (mousePos.x > screenMid)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        onGround = Physics.CheckSphere(checkGroundObj.transform.position, 0.15f, LayerMask.GetMask("Plane"));

        CheckHealth();
        _healthTMP.text = "Health: " + _health + "\nAttack: " + _attack + "\nSpeed: " + _moveSpeed + "\nTime: " + _gameTime + "\nScore: " + _score;
    }

    public void GetHit(int value)
    {
        _health -= value;
    }

    void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void CheckHealth()
    {
        if (_health <= 0)
        {
            _canvass.gameObject.SetActive(true);
            _canvass.text = "Score: " + _score;
            
            if (_gameManager == null)
                return;
            Destroy(_gameManager.gameObject);
        }
    }

    public void ResetToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}