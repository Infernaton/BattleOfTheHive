using System;
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

[CreateAssetMenu(fileName = "New Stats", menuName = "Minion/New Stats")]
public class MinionStats : ScriptableObject
{
    [Header("Attack")]
    public float Range;
    public float Rate;
    public float Damage;

    public bool isStoppingForAttack;

    public DamageType DamageType;

    [Header("Other")]
    public float MvtSpeed;

    public List<LifeForm> GetLifeFormHit(Transform origin, Func<List<Collider>, List<LifeForm>> filter)
    {
        switch(DamageType)
        {
            case DamageType.Single:
                if(Physics.Raycast(origin.position, origin.forward, out RaycastHit hitInfo, Range))
                    return filter(new() { hitInfo.collider });
                return new();
            case DamageType.ArroundAttacker:
                List<Collider> l = Physics.OverlapSphere(origin.position, Range).ToList();
                return filter(l);
            default:
                return new();
        }
    }
}
