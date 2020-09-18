using UnityEngine;

public class InputManager : MonoBehaviour
{
    private MovementController _movementController;

    private void Awake()
    {
        _movementController = FindObjectOfType<MovementController>();
    }

    private void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// Handles the input.
    /// </summary>
    private void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _movementController.Walk(horizontal);

        //if (Input.GetAxis("Jump") > 0)
        //    _movementController.Jump();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            _movementController.Jump();
    }
}
