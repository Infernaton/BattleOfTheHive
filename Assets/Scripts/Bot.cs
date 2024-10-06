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
                m_Hive.ToggleTitanSpawner();
                break;
        }
    }
    
    void Update()
    {
        if (GameManager.Instance.IsGameActive && Time.time - _lastUpdateTime >= m_UpdateTime)
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
                    m_Hive.ToggleTitanSpawner();
                    break;
            }
        }
    }
}
