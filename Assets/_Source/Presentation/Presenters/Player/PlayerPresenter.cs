using System;
using Contracts.Domain;
using Contracts.Presentation;
using Contracts.Signals;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;

namespace Presentation.Presenters.Player
{
    public class PlayerPresenter : IPlayerPresenter, IDisposable
    {
        private readonly ICameraView _cameraView;
        private readonly IPlayerView _view;
        private readonly MessageBus _messageBus;

        public Vector3 Position => _view.GameObject.transform.position;

        public PlayerPresenter(IPlayerView view, ICameraView cameraView, MessageBus messageBus)
        {
            _view = view;
            _cameraView = cameraView;
            _messageBus = messageBus;

            _view.OnItemCollision += CollisionItem;
        }

        public void Move(Vector2 moveDirection, float speed)
        {
            var cameraForward = _cameraView.LookDirection;

            cameraForward.y = 0;
            cameraForward.Normalize();

            var cameraRight = Vector3.Cross(Vector3.up, cameraForward).normalized;

            var worldDirection = cameraForward * moveDirection.y + cameraRight * moveDirection.x;

            worldDirection.Normalize();

            var velocity = new Vector3(
                worldDirection.x * speed,
                _view.Rigidbody.linearVelocity.y,
                worldDirection.z * speed
            );

            _view.Rigidbody.linearVelocity = velocity;
        }

        public void SetCameraActive(bool active)
        {
            _cameraView.CinemachineInput.enabled = active;
        }

        private void CollisionItem(IItem item, bool collision, int itemViewId)
        {
            _messageBus.Publish(new SelectForPickupSignal(collision, item, itemViewId));
        }

        public void Dispose()
        {
            _view.OnItemCollision -= CollisionItem;
        }
    }
}