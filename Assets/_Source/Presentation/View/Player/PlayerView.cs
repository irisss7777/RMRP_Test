using System;
using Contracts.Domain;
using Contracts.Presentation;
using Presentation.View.Item;
using UnityEngine;

namespace Presentation.View.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public Rigidbody Rigidbody { get; private set; }
        public GameObject GameObject => gameObject;
        
        public event Action<IItem, bool, int> OnItemCollision;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ItemView itemView))
            {
                OnItemCollision?.Invoke(itemView.ItemData, true, itemView.ItemViewId);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ItemView itemView))
            {
                OnItemCollision?.Invoke(itemView.ItemData, false, itemView.ItemViewId);
            }
        }
    }
}