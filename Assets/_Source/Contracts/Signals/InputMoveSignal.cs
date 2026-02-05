using UnityEngine;

namespace Contracts.Signals
{
    public struct InputMoveSignal
    {
        public Vector2 MoveDirection;

        public InputMoveSignal(Vector2 moveDirection)
        {
            MoveDirection = moveDirection;
        }
    }
}