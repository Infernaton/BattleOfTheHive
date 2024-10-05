using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Hive : LifeForm
{
    [Header("Minion")]
    [SerializeField] ScriptableSpawnType m_MinionScriptable;
    [SerializeField] GameObject m_MinionsSpawner;
    private float _lastMinionSpawnTime;
    private GameObject _minionPrimaryTarget;

    [Header("Warrior")]
    [SerializeField] ScriptableSpawnType m_WarriorScriptable;
    [SerializeField] GameObject m_WarriorsSpawner;
    private float _lastWarriorSpawnTime;
    private GameObject _warriorPrimaryTarget;

    [Header("Titan")]
    [SerializeField] ScriptableSpawnType m_TitanScriptable;
    [SerializeField] GameObject m_TitansSpawner;
    private float _lastTitanSpawnTime;
    private GameObject _titanPrimaryTarget;

    [Header("Other")]
    [SerializeField] bool m_IsSpawnerActivated = true;

    public GameObject GetTarget(SpawnType type)
    {
        return type switch
        {
            SpawnType.Minion => _minionPrimaryTarget,
            SpawnType.Warrior => _warriorPrimaryTarget,
            SpawnType.Titan => _titanPrimaryTarget,
            _ => gameObject
        };
    }

    public void DefinePrimaryTarget(SpawnType type)
    {
        switch (type)
        {
            // If the spawner has children, set the first one as PrimaryTarget
            // else, this Hive become the PrimaryTarget
            case SpawnType.Minion:
                if (m_MinionsSpawner.transform.childCount > 0) _minionPrimaryTarget = m_MinionsSpawner.transform.GetChild(0).gameObject;
                else _minionPrimaryTarget = gameObject;
                break;
            case SpawnType.Warrior:
                if (m_WarriorsSpawner.transform.childCount > 0) _warriorPrimaryTarget = m_WarriorsSpawner.transform.GetChild(0).gameObject;
                else _warriorPrimaryTarget = gameObject;
                break;
            case SpawnType.Titan:
                if (m_TitansSpawner.transform.childCount > 0) _titanPrimaryTarget = m_TitansSpawner.transform.GetChild(0).gameObject;
                else _titanPrimaryTarget = gameObject;
                break;
        }
    }

    private new void Start()
    {
        base.Start();

        _lastMinionSpawnTime = Time.time;
        _minionPrimaryTarget = gameObject;

        _lastWarriorSpawnTime = Time.time;
        _warriorPrimaryTarget = gameObject;

        _lastTitanSpawnTime = Time.time;
        _titanPrimaryTarget = gameObject;
    }

    private new void Update()
    {
        base.Update();

        #region Spawner Related
        if (!m_IsSpawnerActivated) return;
        if (Time.time - _lastMinionSpawnTime >= 1f / m_MinionScriptable.CooldownTime) 
        {
            Spawn(m_MinionScriptable, m_MinionsSpawner.transform);
            _lastMinionSpawnTime = Time.time;
        }
        #endregion
    }

    private void Spawn(ScriptableSpawnType spawn, Transform spawnerLocation)
    {
        Minion newSpawn = Instantiate(spawn.Spawn, spawnerLocation);
        newSpawn.ParentHive = this;
        newSpawn.SpawnType = spawn.Type;

        //Set new Primary Target if there's none or if it's this Hive itself
        GameObject getCurrentPrimaryTarget = GetTarget(spawn.Type);
        if (Compare.GameObjects(getCurrentPrimaryTarget, gameObject)) 
            DefinePrimaryTarget(spawn.Type);
    }

    #region Debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_MinionsSpawner.transform.position, 0.2f);
    }
    #endregion
}
