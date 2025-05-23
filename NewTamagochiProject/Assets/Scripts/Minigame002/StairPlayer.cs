using UnityEngine;

namespace minigame002
{
    public class StairPlayer : MonoBehaviour
    {
        private StairButtons _buttons;

        private float _distanceX;
        private float _distanceY;

        public void OnInitialize(float distanceX, float distanceY, Vector3 startPosition, StairButtons buttons)
        {
            _distanceX = distanceX;
            _distanceY = distanceY;
            transform.position = startPosition;
            _buttons = buttons;
            _buttons.Jump += OnJump;
            _buttons.Rotate += OnRotate;
        }

        private void OnJump()
        {
            Step();
        }

        private void OnRotate()
        {
            _distanceX *= -1;
            Step();
        }

        private void Step()
        {
            Vector3 currentPosition;
            Vector3 newPosition;
            currentPosition = transform.position;
            newPosition = new Vector3(transform.position.x + _distanceX, transform.position.y + _distanceY, transform.position.z);
            transform.position = newPosition;            
        }
    }
}
