using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    Rigidbody _rb;

    Vector2 _movementInputs;
    PlayerControllerInputs _inputs;

    MultiplayerManager _pManager;
    [SerializeField] GameObject _skin1;
    [SerializeField] GameObject _skin2;

    [SerializeField]
    [Tooltip("Defines the speed in which the character will get to his max speed.")]
    float _speed;

    [SerializeField]
    float _maxSpeed;



    void Awake()
    {
        _pManager = FindObjectOfType<MultiplayerManager>();

        if (_pManager.Player1IsReady && !_pManager.Player2IsReady)
        {
            _skin1.SetActive(true);
        }
        else if (_pManager.Player2IsReady)
        {
            _skin2.SetActive(true);
        }

        _inputs = new PlayerControllerInputs();

        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _inputs.Player.Enable();
    }

    private void OnDisable()
    {
        _inputs.Player.Disable();
    }

    void FixedUpdate()
    {
        Vector3 _movement = Vector3.zero;

        _movement = new Vector3(_movementInputs.y, 0, -_movementInputs.x);


        _movement = _movement.normalized;

        if (_rb.velocity.magnitude <= _maxSpeed)
        {
            _rb.velocity += _movement * _speed * Time.deltaTime;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementInputs = movementVector;
    }
}
