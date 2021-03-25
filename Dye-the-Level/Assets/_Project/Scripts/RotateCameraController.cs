using UnityEngine;

namespace Gisha.DyeTheWorld
{
    public class RotateCameraController : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float rotateAcceleration;

        float _currentSpeed;

        private void FixedUpdate()
        {
            var h = Input.GetAxis("Horizontal");
            Rotate(h);
        }

        private void Rotate(float horizontal)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, rotateSpeed * Mathf.Abs(horizontal), rotateAcceleration * Time.deltaTime);
            transform.Rotate(-Vector3.up * horizontal, _currentSpeed * Time.deltaTime);
        }
    }
}