using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class LifeForm : MonoBehaviour
{
    [Header("LifeForm")]
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;
    [SerializeField] protected float m_HealthBase;
    [SerializeField] protected UnityEvent m_OnDying;

    protected float _currentHealth;
    private bool _isDead;

    public MeshRenderer Renderer;

    [HideInInspector] public SpawnType SpawnType;

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
        if (m_TextMeshPro != null)
            m_TextMeshPro.text = _currentHealth + " ♥";
    }

    public void LoseHP(float damage)
    {
        _currentHealth -= damage;
    }
}
