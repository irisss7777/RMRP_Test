using Contracts.Presentation;
using UnityEngine;

namespace Contracts.Database
{
    public interface IItemsConfig
    {
        public Vector3 DefaultPosition { get; }
        public IItemView ViewPrefab { get; }
        public float SpawnNoiseZone { get; }
    }
}