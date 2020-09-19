using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameController _gameController;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            _gameController.LevelCompleted();
    }
}
