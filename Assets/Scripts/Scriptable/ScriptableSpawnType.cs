using UnityEngine;
using UnityEngine.UI;
using Utils;

[CreateAssetMenu(fileName = "New SpawnType", menuName = "Hive/New SpawnType")]
public class ScriptableSpawnType : ScriptableObject
{
    public Minion MinionGameObject;
    public float CooldownTime;
    public SpawnType Type;

    public Image UICooldownImage => TargetManager.Instance.GetCooldownObject(Type);
}
