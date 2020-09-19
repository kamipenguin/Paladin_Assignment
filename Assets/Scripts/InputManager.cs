using UnityEngine;

public class InputManager : MonoBehaviour
{
    private MovementController _movementController;
    private GameController _gameController;

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
        _movementController.Walk(horizontal);

        if (Input.GetAxisRaw("Jump") > 0)
            _movementController.Jump();
    }
}
