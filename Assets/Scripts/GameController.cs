using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;
    [SerializeField]
    private Text _title;

    public bool IsPlaying { get; set; }
    
    private void Start()
    {
        _panel.SetActive(false);
        IsPlaying = true;
    }

    public void LevelCompleted()
    {
        _title.text = "Level Completed!";
        _panel.SetActive(true);
        IsPlaying = false;
    }

    public void GameOver()
    {
        _title.text = "Game Over";
        _panel.SetActive(true);
        IsPlaying = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
