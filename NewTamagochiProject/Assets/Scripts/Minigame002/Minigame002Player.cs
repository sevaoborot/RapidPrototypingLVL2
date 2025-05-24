using System;
using System.Collections;
using UnityEngine;

namespace minigame002
{
    public class Minigame002Player : MonoBehaviour
    {
        public event Action PlayerNotOnStairHandler;

        private Minigame002UI _buttons;
        private Rigidbody2D _rb;

        private float _distanceX;
        private float _distanceY;

        private bool _isOnStair = false;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(GetComponent<Collider2D>().bounds.center, GetComponent<Collider2D>().bounds.size);
        }

        public void OnInitialize(float distanceX, float distanceY, Vector3 startPosition, Minigame002UI buttons)
        {
            _rb = GetComponent<Rigidbody2D>();
            _distanceX = distanceX;
            _distanceY = distanceY;
            transform.position = startPosition;
            _buttons = buttons;
            _buttons.Jump += OnJump;
            _buttons.Rotate += OnRotate;
        }

        private void OnJump()
        {
            StartCoroutine(Step());
        }

        private void OnRotate()
        {
            _distanceX *= -1;
            StartCoroutine(Step());
        }

        private IEnumerator Step()
        {
            Vector3 currentPosition;
            Vector3 newPosition;
            currentPosition = transform.position;
            newPosition = new Vector3(transform.position.x + _distanceX, transform.position.y + _distanceY, transform.position.z);
            _rb.MovePosition(newPosition);
            yield return new WaitForFixedUpdate();
            Debug.Log(_isOnStair);
            if (!_isOnStair) PlayerNotOnStairHandler?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision) => _isOnStair = true;

        private void OnTriggerExit2D(Collider2D other) => _isOnStair = false;
    }
}
