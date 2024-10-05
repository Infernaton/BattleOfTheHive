using UnityEngine;

namespace Utils
{
    public enum SpawnType
    {
        Worker,
        Warrior,
        Titan
    }

    public class Compare
    {
        /**
         * Compare if two gameobjects are the same instance
         */
        public static bool GameObjects(GameObject go1, GameObject go2)
        {
            return go1.GetInstanceID() == go2.GetInstanceID();
        }
    }
}