using System;
using UnityEngine;
using UnityEngine.UI;

namespace minigame002
{
    public class Minigame002UI : MonoBehaviour
    {
        public event Action Jump;
        public event Action Rotate;

        [Header("Buttons")]
        [SerializeField] private Button _jumpButton;
        [SerializeField] private Button _rotateButton;

        public void OnInitialize(Minigame002Setup _setup)
        {
            _setup.GameOverHandler += BlockButtons;
        }

        public void OnJumpButtonPressed() => Jump?.Invoke();

        public void OnRotateButtonPressed() => Rotate?.Invoke();

        private void BlockButtons()
        {
            _jumpButton.enabled = false;
            _rotateButton.enabled = false;
            Debug.Log("оффнул кнопА4444ки");
        }
    }
}
