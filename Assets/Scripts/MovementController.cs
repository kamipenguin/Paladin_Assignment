using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private float _moveSpeed = 15f;
    [SerializeField]
    private float _jumpSpeed = 10f;
    private bool _hasJumped;
    private bool _isGrounded;
    private float _rayXOffset = 0.9f;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.DrawRay(new Vector2(transform.position.x - _rayXOffset, transform.position.y), Vector2.down, Color.red);
    }

    public void Walk(float horizontal)
    {
        Vector2 direction = new Vector2(horizontal, 0);
        _rigidBody.AddForce(direction * _moveSpeed);
    }

    public void Jump()
    {
        if (IsGrounded() && !_hasJumped)
        {
            _hasJumped = true;
            _rigidBody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - _rayXOffset, transform.position.y), Vector2.down, 1);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                _hasJumped = false;
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
}
