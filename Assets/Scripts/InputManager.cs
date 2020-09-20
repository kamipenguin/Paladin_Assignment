using UnityEngine;

public class InputManager : MonoBehaviour
{
    private MovementController _movementController;
    private GameController _gameController;
    [SerializeField]
    private float _maxJumpButtonHoldTime = 1f;
    private float _jumpButtonHoldTimer = 0f;

    private float _storeHorizontal;

    private void Awake()
    {
        _movementController = FindObjectOfType<MovementController>();
        _gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if (_gameController.IsPlaying)
            HandleInput();
    }

    /// <summary>
    /// Handles the input.
    /// </summary>
    private void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            _storeHorizontal = horizontal;
            _movementController.Move(horizontal);
        }
        else
            _movementController.StopMoving(_storeHorizontal);

        if (Input.GetAxisRaw("Jump") > 0)
            _movementController.Jump();
        else if (!_movementController.IsGrounded)
            _movementController.StopJumping();
    }
}
