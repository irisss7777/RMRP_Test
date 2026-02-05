using System;
using Contracts.Domain;
using UnityEngine;

namespace Contracts.Presentation
{
    public interface IPlayerView
    {
        public Rigidbody Rigidbody { get; }
        public GameObject GameObject { get; }
        public event Action<IItem, bool, int> OnItemCollision;
    }
}