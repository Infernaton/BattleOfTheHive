using UnityEngine;
public enum Target
{
    AllyHive,
    EnemyHive
}

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject m_AllyHive;
    [SerializeField] private GameObject m_EnemyHive;

    public static TargetManager Instance; // A static reference to the TargetManager instance

    void Awake()
    {
        if (Instance == null) // If there is no instance already
            Instance = this;
        else if (Instance != this) // If there is already an instance and it's not `this` instance
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
    }

    public GameObject GetGameObject(Target target)
    {
        return target switch
        {
            Target.AllyHive => m_AllyHive,
            Target.EnemyHive => m_EnemyHive,
            _ => null,
        };
    }
}