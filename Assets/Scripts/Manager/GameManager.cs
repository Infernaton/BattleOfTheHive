using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    InGame,
    EndGame
}

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject m_CanvaEndGame;
    [SerializeField] GameObject m_VictoryTitle;
    [SerializeField] GameObject m_DefeatTitle;

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
        _currentGameState = GameState.InGame;
    }

    public void GameOver()
    {
        _currentGameState = GameState.EndGame;
        Debug.Log("Game over");
    }

    public void Win()
    {
        _currentGameState = GameState.EndGame;
        Debug.Log("You Win");
    }
}
