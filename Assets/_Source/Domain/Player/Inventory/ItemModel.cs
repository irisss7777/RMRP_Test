using Contracts.Domain;
using UnityEngine;

namespace Domain.Player.Inventory
{
    public class ItemModel : IItem
    {
        public string Name { get; }
        public int Id { get; }
        public bool IsStackable { get; }
        public int MaxStackCount { get; }
        public int StackCount { get; }
        public Color Color { get; }

        public ItemModel(string name, int id, bool isStackable, int maxStackCount, Color color, int stackCount = 1)
        {
            Name = name;
            Id = id;
            IsStackable = isStackable;
            MaxStackCount = maxStackCount;
            Color = color;
            StackCount = stackCount;
        }
    }
}