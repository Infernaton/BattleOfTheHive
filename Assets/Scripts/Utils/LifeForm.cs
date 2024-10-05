using UnityEngine;
using UnityEngine.Events;

public class LifeForm : MonoBehaviour
{
    [Header("LifeForm")]
    [SerializeField] protected float m_HealthBase;

    protected float _currentHealth;
    protected float _currentMaxHealth;
    private bool _isDead;

    public float GetMaxHealth() => _currentMaxHealth;
    public float GetCurrentHealth() => _currentHealth;

    protected void _startLifeForm()
    {
        _currentHealth = m_HealthBase;
        _currentMaxHealth = m_HealthBase;
    }

    protected void _updateLifeForm()
    {
        if ((_currentHealth <= 0 || transform.position.y <= -1) && !_isDead)
        {
            _isDead = true;
        }
    }

    public void LoseHP(float damage)
    {
        _currentHealth -= damage;
    }
}
