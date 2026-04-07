using UnityEngine;
using UnityEngine.InputSystem;

public enum Team
{
    None,
    Spy,
    Merc,
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private StartMenu _startMenu;

    // todo - move to ScriptableObject
    [SerializeField]
    private GameObject _spyPrefab;
    [SerializeField]
    private GameObject _mercPrefab;

    private GameObject _currentPlayerModel;
    private PlayerInput _currentPlayerInput;

    // todo - use an event system to communicate between ui and manager
    private void Start()
    {
        // pretty sure there's a better way to to this
        // can += UnityAction, but then you have to -= on disable
        _startMenu.OnJoinSpyTeamClicked = () => JoinTeam(Team.Spy);
        _startMenu.OnJoinMercTeamClicked = () => JoinTeam(Team.Merc);
        _startMenu.SetActive(true);
    }

    // need to receive a signal from start menu to load prefab
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // bring up menu
            _startMenu.SetActive(true);
        }
        else if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SpawnPlayerModel(_spyPrefab);
            CachePlayerInput();
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SpawnPlayerModel(_mercPrefab);
            CachePlayerInput();
        }
    }

    private void JoinTeam(Team team)
    {
        switch(team)
        {
            case Team.Spy:
                SpawnPlayerModel(_spyPrefab);
                CachePlayerInput();
                break;
            case Team.Merc:
                SpawnPlayerModel(_mercPrefab);
                CachePlayerInput();
                break;
            default:
                break;
        }

        StartGame();
    }

    private void StartGame()
    {
        _startMenu.SetActive(false);
    }

    private void SpawnPlayerModel(GameObject prefab)
    {
        DestroyPlayerModel();
        _currentPlayerModel = GameObject.Instantiate(prefab);
    }

    private void DestroyPlayerModel()
    {
        if (_currentPlayerModel != null)
        {
            _currentPlayerInput.actions = null; // avoid false errors thrown with InputSystem 1.4.2
            _currentPlayerInput = null;
            GameObject.Destroy(_currentPlayerModel);
            _currentPlayerModel = null;
        }
    }

    private void CachePlayerInput()
    {
        if (_currentPlayerModel != null)
        {
            _currentPlayerInput = _currentPlayerModel.GetComponentInChildren<PlayerInput>();
        }
    }

    // hack to avoid false errors thrown with InputSystem 1.4.2 when input is destroyed
    private void ClearPlayerInput()
    {
        if (_currentPlayerInput != null)
        {
            _currentPlayerInput.actions = null;
            _currentPlayerInput = null;
        }
    }

    private void OnDisable()
    {
        DestroyPlayerModel();
    }
}
