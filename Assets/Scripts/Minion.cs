using UnityEngine;
using Utils;

public class Minion : LifeForm
{
    [Header("Other Stats")]
    [SerializeField] float m_MovementSpeed;
    [SerializeField] float m_AttackRange;
    [SerializeField] float m_AttackRate;
    [SerializeField] float m_AttackDamage;
    [SerializeField] SpawnType m_PrimaryTarget;

    private LifeForm _currentTarget;
    private Rigidbody _rg;
    private bool _canHitTarget;
    private float _lastAttackTime;

    [HideInInspector] public Hive ParentHive;
    [HideInInspector] public SpawnType SpawnType;

    LifeForm GetTarget()
    {
        Hive oppositeHive = TargetManager.Instance.GetOppositeHive(ParentHive);
        return oppositeHive.GetPrimaryTarget(m_PrimaryTarget);
    }

    private new void Start()
    {
        base.Start();

        _rg = GetComponent<Rigidbody>();
        _currentTarget = GetTarget();
        _lastAttackTime = Time.time;
    }

    private void FixedUpdate()
    {
        // If the target is can be hit, we don't need to move further
        if(!_canHitTarget && GameManager.Instance.IsGameActive) _rg.MovePosition(transform.position + (m_MovementSpeed * Time.deltaTime) * transform.forward);
    }

    private new void Update()
    {
        if (!GameManager.Instance.IsGameActive) return;

        base.Update();
        if (_currentTarget == null) 
            _currentTarget = GetTarget();
        transform.LookAt(_currentTarget.transform);

        _canHitTarget = false;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, m_AttackRange))
        {
            if (hitInfo.transform.TryGetComponent<LifeForm>(out var lifeForm))
            {
                // If it's the ParentHive hive, we don't want to damage it
                if (lifeForm is Hive && Compare.GameObjects(lifeForm.gameObject, ParentHive.gameObject)) 
                    return;
                // If it's an ally minion, we don't want to damage it
                if (lifeForm.gameObject.TryGetComponent<Minion>(out var m) && Compare.GameObjects(ParentHive.gameObject, m.ParentHive.gameObject))
                    return;

                //Debug.Log(gameObject.name + " -> " + lifeForm);

                _canHitTarget = true;
                if (Time.time - _lastAttackTime >= m_AttackRate)
                {
                    _currentTarget.LoseHP(m_AttackDamage);
                    _lastAttackTime = Time.time;
                }
            }
        }
    }

    public void OnDying()
    {
        Destroy(gameObject);

        // Define a new Primary Target if this instance was the current one. We also need to redefine it in the next frame.
        // Otherwise, the Primary target will still be this one
        bool isPrimaryTarget = Compare.GameObjects(gameObject, ParentHive.GetPrimaryTarget(SpawnType).gameObject);
        if (isPrimaryTarget) ParentHive.AddTargetToRedefine(SpawnType);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * m_AttackRange);
    }
}
