using UnityEngine;

namespace Contracts.Factory
{
    public interface IItemFactory
    {
        public void CreateItem(Vector3 position, string name, int id, bool isStackable, int maxStackCount, Color color, int itemCount = 1);
        public Vector3 GetRandomPosition();
    }
}