using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] Hive m_hive;
    [SerializeField] float m_update_time;
    private float _last_update_time; 
    void Start()
    {
        _last_update_time = Time.time;
        int rand_value = UnityEngine.Random.Range(0, 3);
            switch (rand_value)
            {
                case 0:
                    m_hive.ToggleWorkerSpawner();
                    break;
                case 1:
                    m_hive.ToggleWarriorSpawner();
                    break;
                case 2: 
                    m_hive.ToggleTitanSpawner();
                    break;
            }
    }
    
    void Update()
    {
        if ( Time.time - _last_update_time > m_update_time && GameManager.Instance.IsGameActive)
        {
            _last_update_time = Time.time;
            int rand_value = UnityEngine.Random.Range(0, 3);
            switch (rand_value)
            {
                case 0:
                    m_hive.ToggleWorkerSpawner();
                    break;
                case 1:
                    m_hive.ToggleWarriorSpawner();
                    break;
                case 2: 
                    m_hive.ToggleTitanSpawner();
                    break;
            }
        }
    }
}
