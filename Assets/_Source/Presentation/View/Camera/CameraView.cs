using Contracts.Presentation;
using Unity.Cinemachine;
using UnityEngine;

namespace Presentation.View.Camera
{
    public class CameraView : MonoBehaviour, ICameraView
    {
        [SerializeField] private CinemachineCamera _camera;
        [SerializeField] private CinemachineInputAxisController _cinemachineInput;

        public CinemachineCamera CinemachineCamera => _camera;
        public CinemachineInputAxisController CinemachineInput => _cinemachineInput;
        public Vector3 LookDirection => _camera.transform.forward;
    }
}