using UnityEngine;

namespace Gisha.DyeTheLevel.Core
{
    public class RotateCameraController : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float rotateAcceleration;

        [Header("Mouse")]
        [SerializeField] private float mouseSensitivity;

        float _currentSpeed;
        Vector2 _rotateStartPos, _rotateCurrentPos;

        private void FixedUpdate()
        {
            var h = Input.GetAxis("Horizontal");
            KeyboardRotate(h);
            MouseRotate();
        }

        private void KeyboardRotate(float horizontal)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, rotateSpeed * Mathf.Abs(horizontal), rotateAcceleration * Time.deltaTime);
            transform.Rotate(-Vector3.up * horizontal, _currentSpeed * Time.deltaTime);
        }

        private void MouseRotate()
        {
            if (Input.GetMouseButtonDown(2))
                _rotateStartPos = Input.mousePosition;

            if (Input.GetMouseButton(2))
            {
                _rotateCurrentPos = Input.mousePosition;

                var diff = _rotateStartPos.x - _rotateCurrentPos.x;
                _rotateStartPos = _rotateCurrentPos;

                var delta = diff * mouseSensitivity;

                transform.rotation *= Quaternion.Euler(-Vector3.up * delta);
            }
        }
    }
}