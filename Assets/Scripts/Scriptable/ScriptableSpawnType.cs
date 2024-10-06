using UnityEngine;
using UnityEngine.UI;
using Utils;

[CreateAssetMenu(fileName = "New Minion", menuName = "Minion/New Minion")]
public class ScriptableSpawnType : ScriptableObject
{
    public Minion MinionGameObject;
    public float CooldownTime;
    public SpawnType Type;

    public Image UICooldownImage => TargetManager.Instance.GetCooldownObject(Type);
    public RawImage UISelectedImage => TargetManager.Instance.GetSelectedObject(Type);
}
