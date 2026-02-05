using Unity.Cinemachine;
using UnityEngine;

namespace Contracts.Presentation
{
    public interface ICameraView
    {
        public CinemachineCamera CinemachineCamera { get; }
        public CinemachineInputAxisController CinemachineInput { get; }
        public Vector3 LookDirection { get; }
    }
}