using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DamageType
{
    Single,
    ArroundAttacker,
    AOE,
    Line
}

[CreateAssetMenu(fileName = "New AttackPattern", menuName = "Minion/New Attack")]
public class AttackPattern : ScriptableObject
{
    public float Range;
    public float Rate;
    public float Damage;

    public bool isStoppingForAttack;

    public DamageType DamageType;

    public List<LifeForm> GetLifeFormHit(Transform origin, Func<List<Collider>, List<LifeForm>> filter)
    {
        switch(DamageType)
        {
            case DamageType.Single:
                if(Physics.Raycast(origin.position, origin.forward, out RaycastHit hitInfo, Range))
                {
                    return filter(new() { hitInfo.collider });
                }
                return new();
            case DamageType.ArroundAttacker:
                List<Collider> l = Physics.OverlapSphere(origin.position, Range).ToList();
                return filter(l);
            default:
                return new();
        }
    }
}
