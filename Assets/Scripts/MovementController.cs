using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    //[SerializeField]
    //private float _distance = 0.2f;

    [Header("Walk Settings")]
    [SerializeField]
    private float _moveAcceleration = 1f;
    private float _currentSpeed;
    [SerializeField]
    private float _maxMoveSpeed = 10f;
    [SerializeField]
    private float _moveDeceleration = 5f;

    [Header("Jump Settings")]
    [SerializeField]
    private float _initialJumpForce = 100f;
    [SerializeField]
    private float _jumpDeceleration = 10f;
    [SerializeField]
    private float _minJumpVelocity = 20f;
    [SerializeField]
    private float _terminalVelocity = 2f;
    [SerializeField]
    private float _fallingGravity = 3f;
    private float _currentJumpSpeed;
    private bool _stoppedJumping;

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
        // when player changes direction, set speed to 0.
        if ((_rigidBody.velocity.x > 0 && horizontal < 0) || (_rigidBody.velocity.x < 0 && horizontal > 0))
            _currentSpeed = 0;
        _currentSpeed += _moveAcceleration * Time.deltaTime;
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxMoveSpeed);
        _rigidBody.velocity = new Vector2(horizontal * _currentSpeed, _rigidBody.velocity.y);

        SetWalkingAnimation();
    }

    public void StopMoving(float lastHorizontal)
    {
        _currentSpeed -= _moveDeceleration * Time.deltaTime;
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxMoveSpeed);
        _rigidBody.velocity = new Vector2(lastHorizontal * _currentSpeed, _rigidBody.velocity.y);

        SetIdleAnimation();
    }

    private void SetIdleAnimation()
    {
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
            _stoppedJumping = false;
            _currentJumpSpeed = _initialJumpForce;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _currentJumpSpeed);

            SetJumpingAnimation();
        }
        else
        {
            _currentJumpSpeed -= _jumpDeceleration;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _currentJumpSpeed);
        }
    }

    public void StopJumping()
    {
        if (!IsGrounded)
        {
            if (!_stoppedJumping)
            {
                _stoppedJumping = true;
                float minVelocity = Mathf.Clamp(_rigidBody.velocity.y, 0, _minJumpVelocity);
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, minVelocity);
            }
            else
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y - _jumpDeceleration);
            }
        }
    }

    private void SetFallGravity()
    {
        _rigidBody.gravityScale = _fallingGravity;
    }

    private void SetJumpingAnimation()
    {
        _animator.SetBool("IsIdling", false);
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsJumping", true);
    }

    private void Update()
    {
        if (!IsGrounded)
            if (_rigidBody.velocity.y <= 0)
                SetFallGravity();
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
