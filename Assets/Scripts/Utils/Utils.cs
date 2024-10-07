using UnityEngine;

namespace Utils
{
    public enum SpawnType
    {
        Worker,
        Warrior,
        Sentinel
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

    public class Tools
    {
        public static T GetGameObjectComponent<T>(GameObject _gameObject)
        {
            TryGetParentComponent(_gameObject, out T _out);
            return _out;
        }
        public static bool TryGetParentComponent<T>(GameObject _gameObject, out T _out)
        {
            if (_gameObject.TryGetComponent(out T _out2))
            {
                _out = _out2;
                return true;
            }
            else if (_gameObject.transform.parent != null)
                return TryGetParentComponent(_gameObject.transform.parent.gameObject, out _out);
            else
            {
                _out = default;
                return false;
            }
        }

        public static T GetParentComponent<T>(Collider _gameObject)
        {
            TryGetParentComponent(_gameObject, out T _out);
            return _out;
        }
        public static bool TryGetParentComponent<T>(Collider _gameObject, out T _out)
        {
            return TryGetParentComponent(_gameObject.gameObject, out _out);
        }
    }
}