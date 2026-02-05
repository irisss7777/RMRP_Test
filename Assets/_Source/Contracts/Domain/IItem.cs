using UnityEngine;

namespace Contracts.Domain
{
    public interface IItem
    {
        public string Name { get; }
        public int Id { get; }
        public bool IsStackable { get; }
        public int MaxStackCount { get; }
        public int StackCount { get; }
        public Color Color { get; }
    }
}