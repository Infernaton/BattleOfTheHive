using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : LifeForm
{
    [Header("Spawnable")]
    [SerializeField] ScriptableSpawnType m_Minion;

    // Start is called before the first frame update
    void Start()
    {
        _startLifeForm();

        m_Minion.ResetCooldownTimer();
    }

    // Update is called once per frame
    void Update()
    {
        _updateLifeForm();

        if (m_Minion.canSpawn) Spawn(m_Minion);
    }

    void Spawn(ScriptableSpawnType spawn)
    {
        if (Time.time - spawn.CurrentTime >= 1f / spawn.CooldownTime)
        {
            Instantiate(spawn.Spawn);
            spawn.ResetCooldownTimer();
        }
    }
}
