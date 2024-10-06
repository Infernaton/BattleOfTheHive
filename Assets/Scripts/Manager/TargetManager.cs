using UnityEngine;
using UnityEngine.UI;
using Utils;

public enum HiveTarget
{
    Ally,
    Enemy
}

public class TargetManager : MonoBehaviour
{
    [Header("HiveTarget")]
    [SerializeField] private Hive m_AllyHive;
    [SerializeField] private Hive m_EnemyHive;

    [Header("UICooldownTarget")]
    [SerializeField] private Image m_WorkerCooldownElement;
    [SerializeField] private Image m_WarriorCooldownElement;
    [SerializeField] private Image m_TitanCooldownElement;

    public static TargetManager Instance; // A static reference to the TargetManager instance

    void Awake()
    {
        if (Instance == null) // If there is no instance already
            Instance = this;
        else if (Instance != this) // If there is already an instance and it's not `this` instance
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
    }

    #region Hive Target
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
        if (Compare.GameObjects(hive.gameObject, GetHive(HiveTarget.Ally).gameObject))
            return GetHive(HiveTarget.Enemy);
        return GetHive(HiveTarget.Ally);
    }
    #endregion

    public Image GetCooldownObject(SpawnType target)
    {
        return target switch
        {
            SpawnType.Worker => m_WorkerCooldownElement,
            SpawnType.Warrior => m_WarriorCooldownElement,
            SpawnType.Titan => m_TitanCooldownElement,
            _ => null,
        };
    }
}