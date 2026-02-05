using Contracts.Presentation;

namespace Contracts.Database
{
    public interface IPlayerConfig
    {
        public float Speed { get; }
        public IPlayerView PlayerViewPrefab { get; }
        public int InventorySlotCount { get; }
    }
}