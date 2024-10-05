using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : LifeForm
{
    [Header("Other Stats")]
    [SerializeField] float m_MovementSpeed;
    [SerializeField] float m_AttackRate;
    [SerializeField] float m_AttackDamage;
    [SerializeField] GameObject m_PrimaryTarget;

    private GameObject _currentTarget;
    private Rigidbody _rg;

    GameObject GetTarget()
    {
        // Todo define which target to focus
        return m_PrimaryTarget;
    }

    // Start is called before the first frame update
    void Start()
    {
        _startLifeForm();
        _currentTarget = GetTarget();

        _rg = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rg.AddForce(new Vector3(m_MovementSpeed, _rg.velocity.y, m_MovementSpeed), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        _updateLifeForm();
    }
}
