using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : LifeForm
{
    [Header("Spawnable")]
    [SerializeField] ScriptableSpawnType m_Minion;

    [Header("Spawner")]
    [SerializeField] GameObject m_MinionsSpawner;

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

    private void Spawn(ScriptableSpawnType spawn)
    {
        if (Time.time - spawn.CurrentTime >= 1f / spawn.CooldownTime)
        {
            Instantiate(spawn.Spawn, m_MinionsSpawner.transform);
            spawn.ResetCooldownTimer();
        }
    }

    public void OnDying()
    {
        // Todo gameover manager
        Debug.Log("GameOver");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_MinionsSpawner.transform.position, 0.2f);
    }
}
