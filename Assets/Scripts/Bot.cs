using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] Hive m_Hive;
    [SerializeField] float m_UpdateTime;
    private float _lastUpdateTime; 
    void Start()
    {
        _lastUpdateTime = Time.time;
        int rand_value = Random.Range(0, 3);
        switch (rand_value)
        {
            case 0:
                m_Hive.ToggleWorkerSpawner();
                break;
            case 1:
                m_Hive.ToggleWarriorSpawner();
                break;
            case 2:
                m_Hive.ToggleSentinelSpawner();
                break;
        }
    }
    
    void Update()
    {
        if (GameManager.Instance.IsGameActive && Time.time - _lastUpdateTime >= m_UpdateTime)
        {
            _lastUpdateTime = Time.time;
            Hive ally_hive = TargetManager.Instance.GetOppositeHive(m_Hive);
            int nb_worker = ally_hive.m_WorkersSpawner.transform.childCount;
            int nb_warrior = 3 * ally_hive.m_WarriorsSpawner.transform.childCount;
            int nb_sentinel = 5 * ally_hive.m_SentinelsSpawner.transform.childCount;

            if (nb_worker >= nb_warrior && nb_worker >= nb_sentinel)
                {m_Hive.ToggleSentinelSpawner();}
            else if (nb_warrior >= nb_worker && nb_warrior >= nb_sentinel)
                {m_Hive.ToggleWorkerSpawner();} 
            else
                {m_Hive.ToggleWarriorSpawner();}
        }
    }
}
