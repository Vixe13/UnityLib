using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    PlayerControllerInputs _pInputs;

    Vector2 _movementInputs;
    [SerializeField] float _speed;
    [SerializeField] float _maxSpeed;

    Rigidbody _rb;



    void Awake()
    {
        _pInputs = new PlayerControllerInputs();

        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _pInputs.Player.Enable();
    }

    private void OnDisable()
    {
        _pInputs.Player.Disable();
    }

    // Update is called once per frame
    void Update()
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
