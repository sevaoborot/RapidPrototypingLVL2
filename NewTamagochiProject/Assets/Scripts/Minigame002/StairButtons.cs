using System;
using UnityEngine;
using UnityEngine.UI;

namespace minigame002
{
    public class StairButtons : MonoBehaviour
    {
        public event Action Jump;
        public event Action Rotate;

        [Header("Buttons")]
        [SerializeField] private Button _jumpButton;
        [SerializeField] private Button _rotateButton;

        public void OnInitialize()
        {
            
        }

        public void OnJumpButtonPressed() => Jump?.Invoke();

        public void OnRotateButtonPressed() => Rotate?.Invoke();
    }
}
