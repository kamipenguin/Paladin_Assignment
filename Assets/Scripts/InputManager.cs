using UnityEngine;

public class InputManager : MonoBehaviour
{
    private MovementController _movementController;
    private GameController _gameController;
    [SerializeField]
    private float _maxJumpButtonHoldTime = 0.5f;
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
        // handles input for walking
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            _storeHorizontal = horizontal;
            _movementController.Move(horizontal);
        }
        else
            _movementController.StopMoving(_storeHorizontal);

        // handles input for jumping
        if (Input.GetAxisRaw("Jump") > 0)
        {
            _jumpButtonHoldTimer += Time.deltaTime;
            if (_jumpButtonHoldTimer < _maxJumpButtonHoldTime)
                _movementController.Jump();
            else
                _movementController.StopJumping();
        }
        else
            _movementController.StopJumping();

        if (_movementController.IsGrounded)
            _jumpButtonHoldTimer = 0;
    }
}
