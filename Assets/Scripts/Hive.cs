using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    [Header("Sentinel")]
    [SerializeField] ScriptableSpawnType m_SentinelScriptable;
    [SerializeField] GameObject m_SentinelsSpawner;
    [SerializeField] bool m_SentinelsSpawnerActivated;
    private float _lastSentinelSpawnTime;
    private LifeForm _sentinelPrimaryTarget;

    [Header("Other")]
    [SerializeField] bool m_IsSpawnerActivated = true;

    private List<SpawnType> _redefineTarget = new();
    private bool _isAllyHive;

    public LifeForm GetPrimaryTarget(SpawnType type)
    {
        LifeForm o = type switch
        {
            SpawnType.Worker => _workerPrimaryTarget,
            SpawnType.Warrior => _warriorPrimaryTarget,
            SpawnType.Sentinel => _sentinelPrimaryTarget,
            _ => this
        };
        if (o == this) o = SearchOtherPrimaryTarget(type);
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
            case SpawnType.Sentinel:
                _sentinelPrimaryTarget = m_SentinelsSpawner.transform.childCount > 0
                       ? m_SentinelsSpawner.transform.GetChild(0).GetComponent<Minion>()
                       : this;
                break;
        }
    }
    public LifeForm SearchOtherPrimaryTarget(SpawnType type)
    {
        List<LifeForm> entity = new() { _workerPrimaryTarget, _warriorPrimaryTarget, _sentinelPrimaryTarget };
        entity = entity.Where(l => l.SpawnType != type || l.SpawnType != SpawnType.Hive ).ToList();
        return entity.Count > 0 ? entity[0] : this;
    }

    public void AddTargetToRedefine(SpawnType add)
    {
        _redefineTarget.Add(add);
    }

    private new void Start()
    {
        base.Start();

        _isAllyHive = Compare.GameObjects(gameObject, TargetManager.Instance.GetHive(HiveTarget.Ally).gameObject);
        SpawnType = SpawnType.Hive;

        _lastWorkerSpawnTime = Time.time - m_WorkerScriptable.CooldownTime;
        _workerPrimaryTarget = this;

        _lastWarriorSpawnTime = Time.time - m_WarriorScriptable.CooldownTime;
        _warriorPrimaryTarget = this;

        _lastSentinelSpawnTime = Time.time - m_SentinelScriptable.CooldownTime;
        _sentinelPrimaryTarget = this;
    }

    private new void Update()
    {
        base.Update();

        UpdatePrimaryTarget();

        if (_isAllyHive) UpdateUI();

        #region Spawner Related
        if (!m_IsSpawnerActivated || !GameManager.Instance.IsGameActive) return;

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

        if (m_SentinelsSpawnerActivated && Time.time - _lastSentinelSpawnTime >= m_SentinelScriptable.CooldownTime)
        {
            Spawn(m_SentinelScriptable, m_SentinelsSpawner.transform);
            _lastSentinelSpawnTime = Time.time;
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

    private void UpdateUI()
    {
        m_WorkerScriptable.UISelectedImage.gameObject.SetActive(m_WorkersSpawnerActivated);
        UpdateByScriptable(m_WorkerScriptable, Time.time - _lastWorkerSpawnTime);

        m_WarriorScriptable.UISelectedImage.gameObject.SetActive(m_WarriorsSpawnerActivated);
        UpdateByScriptable(m_WarriorScriptable, Time.time - _lastWarriorSpawnTime);

        m_SentinelScriptable.UISelectedImage.gameObject.SetActive(m_SentinelsSpawnerActivated);
        UpdateByScriptable(m_SentinelScriptable, Time.time - _lastSentinelSpawnTime);
    }

    private void UpdateByScriptable(ScriptableSpawnType script, float currentCooldown)
    {
        float val = (script.CooldownTime - currentCooldown) / script.CooldownTime; // To get a percentage of completion of the current cooldown
        script.UICooldownImage.transform.localScale = new Vector3(Mathf.Lerp(0, 1, val), script.UICooldownImage.transform.localScale.y, script.UICooldownImage.transform.localScale.z);
    }

    private void Spawn(ScriptableSpawnType spawn, Transform spawnerLocation)
    {
        Minion newSpawn = Instantiate(spawn.MinionGameObject, spawnerLocation);
        newSpawn.ParentHive = this;
        newSpawn.SpawnType = spawn.Type;
        newSpawn.name = spawn.Type + "" + spawnerLocation.childCount;

        List<Material> mat = _isAllyHive ? spawn.AllyMaterial : spawn.EnemyMaterial;
        for (int i = 0; i < mat.Count; i++) 
        {
            Material[] m = newSpawn.Renderer.materials;
            m[i] = mat[i];
            newSpawn.Renderer.materials = m;
        }

        //Set new Primary Target if there's none or if it's this Hive itself
        LifeForm getCurrentPrimaryTarget = GetPrimaryTarget(spawn.Type);
        if (GetPrimaryTarget(spawn.Type).SpawnType != spawn.Type) 
            DefinePrimaryTarget(spawn.Type);
    }

    #region Controller
    public void ToggleWorkerSpawner()
    {
        m_WarriorsSpawnerActivated = false;
        m_SentinelsSpawnerActivated = false;
        m_WorkersSpawnerActivated = true;
    }
    public void ToggleWarriorSpawner()
    {
        m_WarriorsSpawnerActivated = true;
        m_SentinelsSpawnerActivated = false;
        m_WorkersSpawnerActivated = false;
    }
    public void ToggleSentinelSpawner()
    {
        m_WarriorsSpawnerActivated = false;
        m_SentinelsSpawnerActivated = true;
        m_WorkersSpawnerActivated = false;
    }
    #endregion
}
