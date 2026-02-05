using System;
using Contracts.Domain;
using Contracts.Presentation;
using Contracts.Signals;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;

namespace Presentation.View.Item
{
    public class ItemView : MonoBehaviour, IItemView, IMessageDisposable
    {
        private IItem _itemData;
        private int _id;
        
        public IItem ItemData => _itemData;
        public int ItemViewId => _id;
        
        public event Action OnDispose;

        public void Initialize(IItem data, int id, MessageBus messageBus)
        {
            _itemData = data;
            _id = id;
            messageBus.Subscribe((ItemPickupSignal signal) => TryDestroy(signal.Id), this);
        }

        private void TryDestroy(int targetId)
        {
            if(_id != targetId)
                return;
            
            Dispose();
            Destroy(gameObject);
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}