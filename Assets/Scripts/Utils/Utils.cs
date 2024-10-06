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

    public class Random
    {
        /**
         * Return -1 or 1
         */
        public static float Sign()
        {
            return UnityEngine.Random.value * 2 - 1;
        }
    }
}