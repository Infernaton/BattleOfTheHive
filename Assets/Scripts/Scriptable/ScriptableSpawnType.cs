using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[CreateAssetMenu(fileName = "New SpawnType", menuName = "Hive/New SpawnType")]
public class ScriptableSpawnType : ScriptableObject
{
    public Minion Spawn;
    public float CooldownTime;
    public SpawnType Type;
}
