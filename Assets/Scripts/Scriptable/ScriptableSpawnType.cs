using UnityEngine;
using Utils;

[CreateAssetMenu(fileName = "New SpawnType", menuName = "Hive/New SpawnType")]
public class ScriptableSpawnType : ScriptableObject
{
    public Minion MinionGameObject;
    public float CooldownTime;
    public SpawnType Type;
}
