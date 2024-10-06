using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject m_CanvaEndGame;
    [SerializeField] GameObject m_VictoryTitle;
    [SerializeField] GameObject m_DefeatTitle;

    public static MenuManager Instance; // A static reference to the TargetManager instance

    void Awake()
    {
        if (Instance == null) // If there is no instance already
            Instance = this;
        else if (Instance != this) // If there is already an instance and it's not `this` instance
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DisplayCanvaEndGame()
    {
        m_CanvaEndGame.SetActive(true);
    }

    public void DisplayDefeatTitle()
    {
        m_DefeatTitle.SetActive(true);
    }
    public void DisplayWinTitle()
    {
        m_VictoryTitle.SetActive(true);
    }
}
