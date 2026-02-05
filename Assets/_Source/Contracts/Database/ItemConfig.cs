using System;

namespace Contracts.Database
{
    [Serializable]
    public struct ItemConfig
    {
        public string Name;
        public int Id;
        public bool IsStackable;
        public int MaxStackCount;
        public UnityColor UnityColor;
    }

    [Serializable]
    public struct UnityColor
    {
        public float Red;
        public float Green;
        public float Blue;
    }
}