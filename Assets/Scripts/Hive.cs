using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : LifeForm
{
    [Header("Spawnable")]
    [SerializeField] ScriptableSpawnType m_Minion;

    private new void Start()
    {
        base.Start();

        m_Minion.ResetCooldownTimer();
    }

    private new void Update()
    {
        base.Update();

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
