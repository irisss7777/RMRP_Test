using UnityEngine;

namespace Contracts.Presentation
{
    public interface IPlayerPresenter
    {
        public Vector3 Position { get; }
        public void Move(Vector2 moveDirection, float speed);
        public void SetCameraActive(bool active);
    }
}