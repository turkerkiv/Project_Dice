using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<DiceBehaviour> _dices;
    [SerializeField] float _delayForActivate = 2f;
    [SerializeField] float _delayForFreeze = 2f;
    [SerializeField] float _delayForLoading = 5f;
    [SerializeField] TextMeshProUGUI _winnerNumberTMP;
    [SerializeField] TextMeshProUGUI _attributesTMP;
    [SerializeField] TextMeshProUGUI _defaults;

    int _throwedDices;
    bool _canLoadNextScene = true;
    int _succesfulThrowing;

    [Header("DiceMan'imizin özelliklerini doldurma")]
    [SerializeField] int _health = 1;
    [SerializeField] int _attack = 1;
    [SerializeField] int _moveSpeed = 4;

    public int Heatlh { get { return _health; } }
    public int Attack { get { return _attack; } }
    public int MoveSpeed { get { return _moveSpeed; } }

    int _score;

    public int Score { get { return _score; } }

    void Awake()
    {
        ManageSingleton();
    }

    public void ModifyHealth(int value)
    {
        ShowText(value, "Health +++");

        _health += value;
        _succesfulThrowing++;
        Debug.Log("health " + _health);
    }

    public void ModifyAttack(int value)
    {
        ShowText(value, "Attack +++");

        _attack += value;
        _succesfulThrowing++;
        Debug.Log("attack " + _attack);
    }

    public void ModifyMoveSpeed(int value)
    {
        ShowText(value, "Move Speed +++");

        _moveSpeed += value;
        _succesfulThrowing++;
        Debug.Log("movespeed " + _moveSpeed);
    }

    public void IncreaseScore(int value)
    {
        _score += value;
    }

    void ShowText(int winnerNumber, string attributeText)
    {
        _winnerNumberTMP.gameObject.SetActive(true);
        _attributesTMP.gameObject.SetActive(true);

        _winnerNumberTMP.text = winnerNumber.ToString();
        _attributesTMP.text = attributeText;
    }

    void Update()
    {
        //atılmış olanlar ve hepsi atıldıysa loading. bu sadece atma
        if (_throwedDices < _dices.Count)
        {
            if (_dices[_throwedDices].IsThrowed)
            {
                _throwedDices++;
                Invoke("ActivateGameobject", _delayForActivate);
            }
        }

        if (_dices.Count == _throwedDices)
        {
            Invoke("FreezeDices", _delayForFreeze);
            Invoke("CheckSuccesfulThrows", _delayForFreeze);
            Invoke("LoadNextScene", _delayForLoading);

            _throwedDices = 100;
        }

        _defaults.text = "Health: " + _health + "\nAttack: " + _attack + "\nSpeed: " + _moveSpeed;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    //değiştirmeye üşendim bi işe yaramıyor şu an. normalde hiç atamazsa baştan başlatacaktım.
    void CheckSuccesfulThrows()
    {
        if (_succesfulThrowing < 1)
        {
            _canLoadNextScene = true;
        }
    }

    void LoadNextScene()
    {
        if (_canLoadNextScene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    void FreezeDices()
    {
        foreach (DiceBehaviour dice in _dices)
        {
            if (dice != null)
            {
                dice.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    void ActivateGameobject()
    {
        if (_throwedDices < _dices.Count)
        {
            _dices[_throwedDices].gameObject.SetActive(true);
        }
    }

    void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType<GameManager>().Length;
        if (instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(gameObject);
    }
}