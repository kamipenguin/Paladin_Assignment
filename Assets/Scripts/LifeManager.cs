using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeManager : MonoBehaviour
{
    private GameController _gameController;
    private SpriteRenderer _spriteRenderer;
    private MovementController _movementController;
    [SerializeField]
    private Transform _playerSpawnPoint;

    [SerializeField]
    private Transform _heartsSpawnPoint;
    [SerializeField]
    private GameObject _heart;

    [SerializeField]
    private int _maxHearts = 3;
    private int _currentHearts;
    [SerializeField]
    private float _offset = 70f;

    [SerializeField]
    private float _respawnCoolDown = 1f;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movementController = GetComponent<MovementController>();
    }

    private void Start()
    {
        _currentHearts = _maxHearts;
        SpawnHearts();
    }

    /// <summary>
    /// Spawns the current amount of life in hearts on the screen.
    /// </summary>
    private void SpawnHearts()
    {
        for (int i = 0; i < _currentHearts; i++)
        {
            GameObject heart = Instantiate(_heart, _heartsSpawnPoint);
            Vector2 heartPos = heart.transform.localPosition;
            float newXPos = heartPos.x + _offset * i;
            heart.transform.localPosition = new Vector2(newXPos, heartPos.y);
        }
    }

    /// <summary>
    /// Removes all the hearts from the screen.
    /// </summary>
    private void DeleteHearts()
    {
        Image[] allHearts = _heartsSpawnPoint.GetComponentsInChildren<Image>();
        foreach (Image heart in allHearts)
            Destroy(heart.gameObject);
    }

    /// <summary>
    /// Updates the hearts by first removing all and then spawning the correct amount again.
    /// </summary>
    public void UpdateHearts()
    {
        _currentHearts--;
        DeleteHearts();

        if (_currentHearts == 0)
        {
            gameObject.SetActive(false);
            _gameController.GameOver();
        }
        else
            SpawnHearts();
    }

    /// <summary>
    /// Respawns the player.
    /// </summary>
    public IEnumerator RespawnPlayer()
    {
        _spriteRenderer.enabled = false;
        _gameController.IsPlaying = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _movementController.CurrentSpeed = 0;
        yield return new WaitForSeconds(_respawnCoolDown);
        gameObject.transform.position = _playerSpawnPoint.position;
        _spriteRenderer.enabled = true;
        _gameController.IsPlaying = true;
    }
}
