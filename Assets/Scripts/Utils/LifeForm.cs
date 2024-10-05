using UnityEngine;
using UnityEngine.Events;

public class LifeForm : MonoBehaviour
{
    [Header("LifeForm")]
    [SerializeField] protected float m_HealthBase;
    [SerializeField] protected UnityEvent m_OnDying;

    protected float _currentHealth;
    private bool _isDead;

    public float GetCurrentHealth() => _currentHealth;

    protected void Start()
    {
        _currentHealth = m_HealthBase;
    }

    protected void Update()
    {
        if ((_currentHealth <= 0 || transform.position.y <= -1) && m_OnDying != null && !_isDead)
        {
            _isDead = true;
            m_OnDying.Invoke();
        }
    }

    public void LoseHP(float damage)
    {
        _currentHealth -= damage;
    }
}
