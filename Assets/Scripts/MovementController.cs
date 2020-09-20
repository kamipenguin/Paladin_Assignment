using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float _distance = 0.2f;

    [Header("Walk Settings")]
    [SerializeField]
    private float _moveAcceleration = 1f;
    [SerializeField]
    private float _maxMoveSpeed = 10f;
    [SerializeField]
    private float _moveDeceleration = 5f;

    [Header("Jump Settings")]
    [SerializeField]
    private float _initialJumpForce = 1f;
    [SerializeField]
    private float _jumpHoldVelocity = 1f;
    [SerializeField]
    private float _jumpDeceleration = 1f;
    [SerializeField]
    private float _fallingGravity = 3f;

    public bool IsGrounded { get; set; }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(float horizontal)
    {
        if (Mathf.Abs(_rigidBody.velocity.x) > _maxMoveSpeed)
            _rigidBody.velocity = new Vector2(horizontal * _maxMoveSpeed * Time.deltaTime, _rigidBody.velocity.y);
        else
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x + horizontal * _moveAcceleration * Time.deltaTime, _rigidBody.velocity.y);

        SetWalkingAnimation();
    }

    public void StopMoving()
    {
        _rigidBody.velocity = new Vector2(Mathf.Lerp(_rigidBody.velocity.x, 0, _moveDeceleration), _rigidBody.velocity.y);
        if (_rigidBody.velocity.x == 0)
        {
            _animator.SetBool("IsWalking", false);
            _animator.SetBool("IsIdling", true);
        }
    }


    private void SetWalkingAnimation()
    {
        if (IsGrounded)
        {
            if (_rigidBody.velocity.x > 0)
            {
                _animator.SetBool("IsIdling", false);
                _animator.SetBool("IsWalking", true);
                _spriteRenderer.flipX = false;
            }
            else
            {
                _animator.SetBool("IsIdling", false);
                _animator.SetBool("IsWalking", true);
                _spriteRenderer.flipX = true;
            }
        }
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            IsGrounded = false;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y + _initialJumpForce);

            SetJumpingAnimation();
        }
    }

    public void StopJumping()
    {
        _rigidBody.gravityScale = 3f;
    }

    private void SetJumpingAnimation()
    {
        _animator.SetBool("IsIdling", false);
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsJumping", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _animator.SetBool("IsWalking", false);
            _animator.SetBool("IsJumping", false);
            _animator.SetBool("IsIdling", true);
            _rigidBody.gravityScale = 1f;
            IsGrounded = true;
        }
    }

    /// <summary>
    /// Checks if the player is on the ground.
    /// </summary>
    /// <returns></returns>
    //public bool IsGrounded()
    //{
    //    RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center, transform.localScale, 0, Vector2.down, _distance);
    //    if (!hit)
    //        return false;

    //    if (hit.collider.gameObject.CompareTag("Ground"))
    //    {
    //        _hasJumped = false;
    //        return true;
    //    }
    //    else
    //        return false;
    //}
}
