using System;
using UnityEngine;

public static class JsonHelper
{
    [Serializable]
    private class Wrapper<T>
    {
        [SerializeField]
        private T[] array;
        public T[] Array { get { return array; } }
    }

    public static T[] FromJson<T>(string json)
    {
        string newJson = "{\"array\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        // Use the public getter to access the array.
        return wrapper.Array;
    }
}
