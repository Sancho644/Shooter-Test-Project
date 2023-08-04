using UnityEngine;

namespace Player
{
    public class PlayerMouseLook : MonoBehaviour
    {
        [SerializeField] private float xSensitivity = 30f;
        [SerializeField] private float ySensitivity = 30f;

        private Camera _camera;
        private float xRotation;

        private const float MIN_ROTATION = -80F;
        private const float MAX_ROTATION = 80F;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void MouseLook(Vector2 input)
        {
            var mouseX = input.x;
            var mouseY = input.y;

            xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
            xRotation = Mathf.Clamp(xRotation, MIN_ROTATION, MAX_ROTATION);

            _camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
        }
    }
}