using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    private int _speedHash = Animator.StringToHash("Speed");
    private Vector2 _forceJump;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private float _horizontalMove;

    private void Start()
    {
        _forceJump = new Vector2(0, 500);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _speed;
        _rigidbody.AddForce(new Vector2(_horizontalMove, 0));

        if (_horizontalMove < 0)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }

        if (_horizontalMove > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        _animator.SetFloat(_speedHash, Mathf.Abs(_horizontalMove));

        if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
        {
            _isGrounded = false;
            _rigidbody.AddForce(_forceJump);
        }
    }

    private void OnCollisionEnter2D()
    {
        _isGrounded = true;
    }
}
