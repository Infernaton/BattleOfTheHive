using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpawnType", menuName = "Hive/New SpawnType")]
public class ScriptableSpawnType : ScriptableObject
{
    public GameObject Spawn;
    public float CooldownTime;

    public bool canSpawn = true;

    public float CurrentTime { get; private set; }

    public void ResetCooldownTimer()
    {
        CurrentTime = Time.time;
    }
}
