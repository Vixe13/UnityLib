using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Asset required for component operation
using UnityEngine.InputSystem;
using UnityEngine.UI;


/// <summary>
/// Manages a local multiplayer system
/// </summary>
[HelpURL("https://github.com/Vixe13/UnityLib/blob/develop/Multiplayer/Manager/README.md")]
public class MultiplayerManager : MonoBehaviour
{
    /// <summary>
    /// This Component manages the arrival of players in the game <br/>
    /// </summary>
    PlayerInputManager _pManager;

    // Check the status of the players, whether they are ready or not
    #region PlayersIsReady
    // Check if each player is ready
    //bool[] _playersIsReady;
    bool _player1IsReady = false;
    bool _player2IsReady = false;

    // These accessors will be integrated in "PlayerController" script of the projects
    #region  Accessors
    public bool Player1IsReady
    {
        get { return _player1IsReady; }
    }

    public bool Player2IsReady
    {
        get { return _player2IsReady; }
    }

    /*
    public bool[] PlayersIsReady
    {
        get
        {
            
        }
    }


    */
    #endregion
    #endregion

    [Header("Spawns")]
    [Tooltip("Players' spawn points at the beginning of the game")]
    [SerializeField] Transform[] _spawnsPlayer;

    // Screen showing which players are ready
    #region JoiningScreen

    [Header("Joining Screen")]
    [SerializeField] GameObject _joiningScreen;

    [Header("Player's Controller Sprite")]
    // Controller sprite couple for each player who will be replaced when a player has joined the game
    [SerializeField] Image[] _playerControllers;

    // We will use this sprite when the player who joined the game is ready
    [SerializeField] Sprite _playerControllerReady;

    [Header("Character's Sprite")]
    // Images presenting the character that each player will control
    [SerializeField] Image[] _imageCharacters;

    // Sprite corresponding to the characters that will be controlled
    [SerializeField] Sprite[] _characters;
    #endregion

    // Timer before game started
    #region Timer before game

    [Header("TimerBeforeGame Screen")]
    // GameObject which will display the timer
    [SerializeField] GameObject _gameStartTimeScreen;

    // Will display the time left
    [SerializeField] Text _timerGameStart;

    // Max value timer
    [SerializeField] int _timeBeforeGameStart;

    // Time left
    float _currentTimeBeforeStart;
    #endregion

    
    [Tooltip("List of Monobehavior scripts that will be disabled")]
    [SerializeField] MonoBehaviour[] _scriptsBeforeStart;

    /// <inheritdoc cref="_scriptsBeforeStart">
    public MonoBehaviour[] ScriptsBeforeStart { get { return _scriptsBeforeStart; } set { _scriptsBeforeStart = value; } }



    void Awake()
    {
        // Display the joining screen
        _joiningScreen.SetActive(true);

        // Makes invisible the images of the characters that the players will control
        for (int i = 0; i < _imageCharacters.Length; i++)
        {
            _imageCharacters[i].enabled = false;
        }

        // Get the "PlayerInputManager" present on the current object to be able, manage the arrival of players in the game
        _pManager = GetComponent<PlayerInputManager>();

        // Disables all scripts that are related to characters
        for (int i = 0; i < _scriptsBeforeStart.Length; i++)
        {
            _scriptsBeforeStart[i].enabled = false;
        }
    }

    void Update()
    {
        // If everyone is ready, the game is getting ready to launch
        if (_player1IsReady && _player2IsReady)
        {
            Invoke("ReadyToLaunch", 1);
        }
    }

    // Display and update the timer in game
    private void TimerBeforeStart(float currentTime)
    {
        _timerGameStart.text = $"{_timeBeforeGameStart - (int)currentTime}";
    }

    // Reactivates all character scripts
    private void LaunchPlay()
    {
        for (int i = 0; i < _scriptsBeforeStart.Length; i++)
        {
            _scriptsBeforeStart[i].enabled = true;
        }
    }

    // Look for each player when someone is ready
    public void PlayerIsReady()
    {
        if (_pManager.playerCount == 1 && !_player1IsReady)
        {
            _player1IsReady = true;

            _playerControllers[_pManager.playerCount - 1].sprite = _playerControllerReady;
            _imageCharacters[_pManager.playerCount - 1].enabled = true;
            _imageCharacters[_pManager.playerCount - 1].sprite = _characters[_pManager.playerCount - 1];
        }
        else if (_pManager.playerCount == 2 && !_player2IsReady)
        {
            _player2IsReady = true;

            _playerControllers[_pManager.playerCount - 1].sprite = _playerControllerReady;
            _imageCharacters[_pManager.playerCount - 1].enabled = true;
            _imageCharacters[_pManager.playerCount - 1].sprite = _characters[_pManager.playerCount - 1];
        }
    }

    // Disable the joining screen and set a timer to indicate that the game will begin
    private void ReadyToLaunch()
    {
        _pManager.joinAction.action.Disable();

        _gameStartTimeScreen.SetActive(true);
        _joiningScreen.SetActive(false);

        _currentTimeBeforeStart += Time.deltaTime;
        TimerBeforeStart(_currentTimeBeforeStart);

        if (_currentTimeBeforeStart >= _timeBeforeGameStart)
        {
            _gameStartTimeScreen.SetActive(false);
            LaunchPlay();
        }
    }

    /// <summary>
    /// Spawn players on the scene
    /// </summary>
    /// <param name="pInput"> This PlayerInput is specific to the player who has just joined the game </param>
    public void NewPlayerSpawn(PlayerInput pInput)
    {
        if (_pManager.playerCount == 1)
        {
            pInput.gameObject.transform.position = _spawnsPlayer[0].position;
            pInput.gameObject.transform.rotation = _spawnsPlayer[0].rotation;
        }
        else if (_pManager.playerCount == 2)
        {
            pInput.gameObject.transform.position = _spawnsPlayer[1].position;
            pInput.gameObject.transform.rotation = _spawnsPlayer[1].rotation;
        }
    }

}
