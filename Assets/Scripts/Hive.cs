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
    [SerializeField] bool m_WorkersSpawnerActivated;
    private float _lastWorkerSpawnTime;
    private LifeForm _workerPrimaryTarget;

    [Header("Warrior")]
    [SerializeField] ScriptableSpawnType m_WarriorScriptable;
    [SerializeField] GameObject m_WarriorsSpawner;
    [SerializeField] bool m_WarriorsSpawnerActivated;
    private float _lastWarriorSpawnTime;
    private LifeForm _warriorPrimaryTarget;

    [Header("Titan")]
    [SerializeField] ScriptableSpawnType m_TitanScriptable;
    [SerializeField] GameObject m_TitansSpawner;
    [SerializeField] bool m_TitansSpawnerActivated;
    private float _lastTitanSpawnTime;
    private LifeForm _titanPrimaryTarget;

    [Header("Other")]
    [SerializeField] bool m_IsSpawnerActivated = true;

    private List<SpawnType> _redefineTarget = new();

    public LifeForm GetPrimaryTarget(SpawnType type)
    {
        LifeForm o = type switch
        {
            SpawnType.Worker => _workerPrimaryTarget,
            SpawnType.Warrior => _warriorPrimaryTarget,
            SpawnType.Titan => _titanPrimaryTarget,
            _ => this
        };
        return o;
    }

    public void DefinePrimaryTarget(SpawnType type)
    {
        switch (type)
        {
            // If the spawner has children, set the first one as PrimaryTarget
            // else, this Hive become the PrimaryTarget
            case SpawnType.Worker:
                _workerPrimaryTarget = m_WorkersSpawner.transform.childCount > 0
                       ? m_WorkersSpawner.transform.GetChild(0).GetComponent<Minion>()
                       : this;
                break;
            case SpawnType.Warrior:
                _warriorPrimaryTarget = m_WarriorsSpawner.transform.childCount > 0
                       ? m_WarriorsSpawner.transform.GetChild(0).GetComponent<Minion>()
                       : this;
                break;
            case SpawnType.Titan:
                _titanPrimaryTarget = m_TitansSpawner.transform.childCount > 0
                       ? m_TitansSpawner.transform.GetChild(0).GetComponent<Minion>()
                       : this;
                break;
        }
    }

    public void AddTargetToRedefine(SpawnType add)
    {
        _redefineTarget.Add(add);
    }

    private new void Start()
    {
        base.Start();

        _lastWorkerSpawnTime = Time.time;
        _workerPrimaryTarget = this;

        _lastWarriorSpawnTime = Time.time;
        _warriorPrimaryTarget = this;

        _lastTitanSpawnTime = Time.time;
        _titanPrimaryTarget = this;
    }

    private new void Update()
    {
        base.Update();

        UpdatePrimaryTarget();

        #region Spawner Related
        if (!m_IsSpawnerActivated) return;

        if (m_WorkersSpawnerActivated && Time.time - _lastWorkerSpawnTime >= m_WorkerScriptable.CooldownTime) 
        {
            Spawn(m_WorkerScriptable, m_WorkersSpawner.transform);
            _lastWorkerSpawnTime = Time.time;
        }

        if (m_WarriorsSpawnerActivated && Time.time - _lastWarriorSpawnTime >= m_WarriorScriptable.CooldownTime)
        {
            Spawn(m_WarriorScriptable, m_WarriorsSpawner.transform);
            _lastWarriorSpawnTime = Time.time;
        }

        if (m_TitansSpawnerActivated && Time.time - _lastTitanSpawnTime >= m_TitanScriptable.CooldownTime)
        {
            Spawn(m_TitanScriptable, m_TitansSpawner.transform);
            _lastTitanSpawnTime = Time.time;
        }
        #endregion
    }

    private void UpdatePrimaryTarget()
    {
        for (int i = 0; i < _redefineTarget.Count; i++)
        {
            var target = _redefineTarget[i];
            DefinePrimaryTarget(target);
            _redefineTarget.Remove(target);
        }
    }

    private void Spawn(ScriptableSpawnType spawn, Transform spawnerLocation)
    {
        Minion newSpawn = Instantiate(spawn.MinionGameObject, spawnerLocation);
        newSpawn.ParentHive = this;
        newSpawn.SpawnType = spawn.Type;
        newSpawn.name = spawn.Type + "" + spawnerLocation.childCount;

        //Set new Primary Target if there's none or if it's this Hive itself
        LifeForm getCurrentPrimaryTarget = GetPrimaryTarget(spawn.Type);
        if (Compare.GameObjects(getCurrentPrimaryTarget.gameObject, gameObject)) 
            DefinePrimaryTarget(spawn.Type);
    }

    #region Controller
    public void ToggleWorkerSpawner()
    {
        m_WarriorsSpawnerActivated = false ;
        m_TitansSpawnerActivated = false ;
        m_WorkersSpawnerActivated = true ;
    }
    public void ToggleWarriorSpawner()
    {
        m_WarriorsSpawnerActivated = true ;
        m_TitansSpawnerActivated = false ;
        m_WorkersSpawnerActivated = false ;
    }
    public void ToggleTitanSpawner()
    {
        m_WarriorsSpawnerActivated = false ;
        m_TitansSpawnerActivated = true ;
        m_WorkersSpawnerActivated = false ;
    }
    #endregion
}
