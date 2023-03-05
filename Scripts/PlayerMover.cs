using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    private Vector2 _forceJump;
    private Rigidbody2D _rigidbody;
    private bool _inAir;
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

        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

        if (Input.GetKeyDown(KeyCode.W) && !_inAir)
        {
            _inAir = true;
            _rigidbody.AddForce(_forceJump);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _inAir = false;
        }
    }
}
