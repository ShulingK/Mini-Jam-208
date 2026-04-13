using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] List<Sprite> _heads;

    [Header("Player")]
    [SerializeField] public PlayerController _player;
    [SerializeField] SpriteRenderer _playerHead;
    [SerializeField] List<AllyController> _allies;
    [SerializeField] GameObject _ally;

    List<int> _headsRemaining = new List<int>();
    public List<AllyController> GetAllies() => _allies;

    [Header("Cinemachine")]
    [SerializeField] CinemachineCamera _cinemachineCamera;

    [Header("UI")]
    [SerializeField] Sprite _noSpellSprite;
    [SerializeField] Image _spell1Image;
    [SerializeField] Sprite _spell1Sprite;
    [SerializeField] TextMeshProUGUI _spell1Text;

    [SerializeField] Image _spell2Image;
    [SerializeField] Sprite _spell2Sprite;
    [SerializeField] TextMeshProUGUI _spell2Text;

    [SerializeField] Image _spell3Image;
    [SerializeField] Sprite _spell3Sprite;
    [SerializeField] TextMeshProUGUI _spell3Text;

    [SerializeField] Image _spell4Image;
    [SerializeField] Sprite _spell4Sprite;
    [SerializeField] TextMeshProUGUI _spell4Text;

    [SerializeField] Sprite _spell5noSprite;
    [SerializeField] Image _spell5Image;
    [SerializeField] Sprite _spell5Sprite;
    [SerializeField] TextMeshProUGUI _spell5Text;



    private void Start()
    {
        for (int i = 0; i < _heads.Count; i++) {
            _headsRemaining.Add(i);
        }

        _playerHead.sprite = GetRandomHead();
        UpdateUI();
    }

    public Sprite GetRandomHead()
    {
        int random = Random.Range(0, _heads.Count);

        _headsRemaining.Remove(random);

        return _heads[random];
    }

    public void CreateNewAlly(Vector2 pos)
    {
        GameObject ally = Instantiate(_ally, pos, Quaternion.identity);
    
        _allies.Add(ally.GetComponent<AllyController>());

        _allies.Last()._head.sprite = GetRandomHead();

        _allies.Last().Init(_player.transform, _allies.Count);

        UpdateUI();
    }


    public void Kill(PlayerController player)
    {
        if (player == _player)
        {
            _player = null;

            if (_allies.Count == 0)
            {
                StartCoroutine(Lose());
                return;
            }

            // Récupérer une seule fois
            GameObject nextAlly = _allies[0].gameObject;
            _allies.RemoveAt(0);

            // Assignations sécurisées
            _player = nextAlly.GetComponent<PlayerController>();

            _player.GetComponent<AllyController>().enabled = false;
            _player.GetComponent<SpellCaster>().enabled = true;
            _player.GetComponent<PlayerController>().enabled = true;


            if (_player == null)
            {
                Debug.LogError("Le nouvel allié n'a pas de PlayerController !");
                return;
            }

            _cinemachineCamera.Target.TrackingTarget = _player.transform;

            for (int i = 0; i < _allies.Count; i++)
            {
                _allies[i].Init(_player.transform, i);
            }
        }
        UpdateUI();
    }

    public void Kill(AllyController ally)
    {
        _allies.Remove(ally);

        Debug.Log(_allies.Count);

        for (int i = 0; i < _allies.Count; i++)
        {
            _allies[i].Init(_player.transform, i);
        }

        UpdateUI();
    }

    private IEnumerator Lose()
    {
        yield return new WaitForSeconds(3f);

        GoMainMenu();
    }


    private void UpdateUI()
    { 
        if (_allies.Count + 1 >= 14)
        {
            _spell5Image.sprite = _spell5Sprite;
            _spell5Text.text = "L";
            _spell5Image.GetComponent<Button>().interactable = true;
        }
        else if (_allies.Count + 1 >= 10)
        {
            _spell5Image.sprite = _spell5noSprite;
            _spell5Text.text = (14 - (_allies.Count + 1)).ToString();
            _spell5Image.GetComponent<Button>().interactable = false;

            _spell4Image.sprite = _spell4Sprite;
            _spell4Text.text = "K";
            _spell4Image.GetComponent<Button>().interactable = true;
        }
        else if (_allies.Count + 1 >= 6)
        {
            _spell5Image.sprite = _spell5noSprite;
            _spell5Text.text = (14 - (_allies.Count + 1)).ToString();
            _spell5Image.GetComponent<Button>().interactable = false;

            _spell4Image.sprite = _noSpellSprite;
            _spell4Text.text = (10 - (_allies.Count + 1)).ToString();
            _spell4Image.GetComponent<Button>().interactable = false;


            _spell3Image.sprite = _spell3Sprite;
            _spell3Text.text = "H";
            _spell3Image.GetComponent<Button>().interactable = true;
        }
        else if (_allies.Count + 1 >= 2)
        {
            _spell5Image.sprite = _spell5noSprite;
            _spell5Text.text = (14 - (_allies.Count + 1)).ToString();
            _spell5Image.GetComponent<Button>().interactable = true;

            _spell4Image.sprite = _noSpellSprite;
            _spell4Text.text = (10 - (_allies.Count + 1)).ToString();
            _spell4Image.GetComponent<Button>().interactable = false;

            _spell3Image.sprite = _noSpellSprite;
            _spell3Text.text = (6 - (_allies.Count + 1)).ToString();
            _spell3Image.GetComponent<Button>().interactable = false;

            _spell2Image.sprite = _spell2Sprite;
            _spell2Text.text = "J";
            _spell2Image.GetComponent<Button>().interactable = true;
        }
        else 
        {
            _spell5Image.sprite = _spell5noSprite;
            _spell5Text.text = (14 - (_allies.Count + 1)).ToString();
            _spell5Image.GetComponent<Button>().interactable = false;

            _spell4Image.sprite = _noSpellSprite;
            _spell4Text.text = (10 - (_allies.Count + 1)).ToString();
            _spell4Image.GetComponent<Button>().interactable = false;

            _spell3Image.sprite = _noSpellSprite;
            _spell3Text.text = (6 - (_allies.Count + 1)).ToString();
            _spell3Image.GetComponent<Button>().interactable = false;

            _spell2Image.sprite = _noSpellSprite;
            _spell2Text.text = (2 - (_allies.Count + 1)).ToString();
            _spell2Image.GetComponent<Button>().interactable = false;

            _spell1Image.sprite = _spell1Sprite;
            _spell1Text.text = "G";
            _spell1Image.GetComponent<Button>().interactable = true;

        }
    }

    public void CastSpell1()
    {
        _player.CastSpell1();
    }
    public void CastSpell2()
    {
        _player.CastSpell2();

    }
    public void CastSpell3()
    {
        _player.CastSpell3();

    }
    public void CastSpell4()
    {
        _player.CastSpell4();

    }
    public void CastSpell5()
    {
        _player.CastSpell5();

    }

    private void Update()
    {
        if (_player == null) return;

        if (!_player.CanCastSpell1()) _spell1Image.GetComponent<Button>().interactable = false;
        else _spell1Image.GetComponent<Button>().interactable = true;
        if (!_player.CanCastSpell2()) _spell2Image.GetComponent<Button>().interactable = false;
        else _spell2Image.GetComponent<Button>().interactable = true;
        if (!_player.CanCastSpell3()) _spell3Image.GetComponent<Button>().interactable = false;
        else _spell3Image.GetComponent<Button>().interactable = true;
        if (!_player.CanCastSpell4()) _spell4Image.GetComponent<Button>().interactable = false;
        else _spell4Image.GetComponent<Button>().interactable = true;
        if (!_player.CanCastSpell5()) _spell5Image.GetComponent<Button>().interactable = false;
        else _spell5Image.GetComponent<Button>().interactable = true;


    }

    [SerializeField] SceneLoader _sceneLoader;

    public void GoMainMenu()
    {
        _sceneLoader.LoadScene(0);
    }
}
