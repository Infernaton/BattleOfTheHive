using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Hive : LifeForm
{
    [Header("Worker")]
    [SerializeField] ScriptableSpawnType m_WorkerScriptable;
    [SerializeField] GameObject m_WorkersSpawner;
    private float _lastWorkerSpawnTime;
    private GameObject _workerPrimaryTarget;

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
            SpawnType.Worker => _workerPrimaryTarget,
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
            case SpawnType.Worker:
                if (m_WorkersSpawner.transform.childCount > 0) _workerPrimaryTarget = m_WorkersSpawner.transform.GetChild(0).gameObject;
                else _workerPrimaryTarget = gameObject;
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

        _lastWorkerSpawnTime = Time.time;
        _workerPrimaryTarget = gameObject;

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

        if (Time.time - _lastWorkerSpawnTime >= 1f / m_WorkerScriptable.CooldownTime) 
        {
            Spawn(m_WorkerScriptable, m_WorkersSpawner.transform);
            _lastWorkerSpawnTime = Time.time;
        }
        if (Time.time - _lastWarriorSpawnTime >= 1f / m_WarriorScriptable.CooldownTime)
        {
            Spawn(m_WarriorScriptable, m_WarriorsSpawner.transform);
            _lastWarriorSpawnTime = Time.time;
        }
        if (Time.time - _lastTitanSpawnTime >= 1f / m_TitanScriptable.CooldownTime)
        {
            Spawn(m_TitanScriptable, m_TitansSpawner.transform);
            _lastTitanSpawnTime = Time.time;
        }
        #endregion
    }

    private void Spawn(ScriptableSpawnType spawn, Transform spawnerLocation)
    {
        Minion newSpawn = Instantiate(spawn.MinionGameObject, spawnerLocation);
        newSpawn.ParentHive = this;
        newSpawn.SpawnType = spawn.Type;

        //Set new Primary Target if there's none or if it's this Hive itself
        GameObject getCurrentPrimaryTarget = GetTarget(spawn.Type);
        if (Compare.GameObjects(getCurrentPrimaryTarget, gameObject)) 
            DefinePrimaryTarget(spawn.Type);
    }
}
