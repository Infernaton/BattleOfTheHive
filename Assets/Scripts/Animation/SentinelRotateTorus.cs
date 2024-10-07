using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RotationOrientation
{
    public Transform Transform;
    public float RotationSpeed;
    public Vector3 RotationDirection;

    public RotationOrientation(Transform t, float speed, Vector3 rotationDirection)
    {
        Transform = t;
        RotationSpeed = speed;
        RotationDirection = rotationDirection;
    }
}

public class SentinelRotateTorus : MonoBehaviour
{
    [SerializeField] Transform m_LargeTorus;
    [SerializeField] List<Transform> m_RandomRotationTorus;
    [SerializeField] float m_RotationSpeed;
    [SerializeField] float m_OffsetSpeed;

    private List<RotationOrientation> _torusRotation = new();

    private void Start()
    {
        Init();
    }

    [ContextMenu("RandomRotation")]
    void Init()
    {
        for (int i = 0; i < m_RandomRotationTorus.Count; i++)
        {
            m_RandomRotationTorus[i].rotation = Random.rotation;
            float speed = Random.Range(m_RotationSpeed - m_OffsetSpeed, m_RotationSpeed + m_OffsetSpeed) * Utils.Random.Sign();
            Vector3 direction = m_RandomRotationTorus[i].right;
            _torusRotation.Add(new RotationOrientation(m_RandomRotationTorus[i], speed, direction));
        }

        _torusRotation.Add(new RotationOrientation(m_LargeTorus, m_RotationSpeed, Vector3.forward));
    }

    private void Update()
    {
        for (int i = 0; i < _torusRotation.Count; i++)
        {
            RotationOrientation r = _torusRotation[i];
            r.Transform.Rotate(r.RotationDirection, r.RotationSpeed);
        }
    }
}
