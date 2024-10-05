using UnityEngine;
public enum HiveTarget
{
    Ally,
    Enemy
}

public class TargetManager : MonoBehaviour
{
    [SerializeField] private Hive m_AllyHive;
    [SerializeField] private Hive m_EnemyHive;

    public static TargetManager Instance; // A static reference to the TargetManager instance

    void Awake()
    {
        if (Instance == null) // If there is no instance already
            Instance = this;
        else if (Instance != this) // If there is already an instance and it's not `this` instance
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
    }

    public Hive GetHive(HiveTarget target)
    {
        return target switch
        {
            HiveTarget.Ally => m_AllyHive,
            HiveTarget.Enemy => m_EnemyHive,
            _ => null,
        };
    }

    public Hive GetOppositeHive(Hive hive)
    {
        if (Utils.Compare.GameObjects(hive.gameObject, GetHive(HiveTarget.Ally).gameObject))
            return GetHive(HiveTarget.Enemy);
        return GetHive(HiveTarget.Ally);
    }
}