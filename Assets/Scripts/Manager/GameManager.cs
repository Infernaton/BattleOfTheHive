using UnityEngine;

public enum GameState
{
    Menu,
    InGame,
    EndGame
}

public class GameManager : MonoBehaviour
{
    private GameState _currentGameState;

    public static GameManager Instance; // A static reference to the TargetManager instance

    public bool IsGameActive => _currentGameState == GameState.InGame;

    void Awake()
    {
        if (Instance == null) // If there is no instance already
            Instance = this;
        else if (Instance != this) // If there is already an instance and it's not `this` instance
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
    }

    private void Start()
    {
        _currentGameState = GameState.Menu;
    }

    private void Update()
    {
        switch (_currentGameState)
        {
            case GameState.Menu:
                if (Input.GetMouseButtonDown(0)){
                    _currentGameState = GameState.InGame;
                    MenuManager.Instance.LaunchGame();
                }
                break;
            default:
                break;
        }
    }

    public void EndGame()
    {
        _currentGameState = GameState.EndGame;
        MenuManager.Instance.DisplayCanvaEndGame();
    }

    public void GameOver()
    {
        EndGame();
        MenuManager.Instance.DisplayDefeatTitle();
    }

    public void Win()
    {
        EndGame();
        MenuManager.Instance.DisplayWinTitle();
    }
}
