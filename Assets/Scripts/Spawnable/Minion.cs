using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Minion : LifeForm
{
    [Header("Other Stats")]
    [SerializeField] float m_MovementSpeed;
    [SerializeField] float m_AttackRate;
    [SerializeField] float m_AttackDamage;
    [SerializeField] SpawnType m_PrimaryTarget;

    private GameObject _currentTarget;
    private Rigidbody _rg;

    [HideInInspector] public Hive ParentHive;
    [HideInInspector] public SpawnType SpawnType;

    GameObject GetTarget()
    {
        Hive oppositeHive = TargetManager.Instance.GetOppositeHive(ParentHive);
        return oppositeHive.GetTarget(m_PrimaryTarget);
    }

    private new void Start()
    {
        base.Start();

        _rg = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rg.velocity = m_MovementSpeed * transform.forward;
    }

    private new void Update()
    {
        base.Update();
        _currentTarget = GetTarget();
        transform.LookAt(_currentTarget.transform);
    }

    public void OnDying()
    {
        Destroy(gameObject);
        bool isPrimaryTarget = Compare.GameObjects(gameObject, ParentHive.GetTarget(SpawnType));
        // Define a new Primary Target if this instance was the current one
        if (isPrimaryTarget) ParentHive.DefinePrimaryTarget(SpawnType);
    }
}
