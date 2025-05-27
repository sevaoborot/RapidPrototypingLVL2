using System;
using System.Collections;
using UnityEngine;

namespace minigame002
{
    public class Minigame002 : Minigame
    {
        public event Action GameOverHandler;

        [SerializeField] private LevelGeneration _levelGeneration;
        [SerializeField] private Player _player;
        [SerializeField] private MinigameUI _ui;
        [SerializeField] private string _shopDataFileName;
        [SerializeField] private string _needsDataFileName;
        [Space]
        [SerializeField] private Transform _startPosition;
        [SerializeField] private float _distanceX;
        [SerializeField] private float _distanceY;

        private int _earnedCoins = 0;

        public override void OnInitialize(Action gameOverAction)
        {
            _shopDataFile = _shopDataFileName;
            _needsDataFile = _needsDataFileName;
            base.OnInitialize(gameOverAction);
            _levelGeneration.OnInitialize(_distanceX, _distanceY, _startPosition.position, _ui, this);
            _ui.OnInitialize(this);
            _player.OnInitialize(_distanceX, _distanceY, _startPosition.position, _ui);
            _player.PlayerNotOnStairHandler += EndGame;
        }

        private IEnumerator StairTimer()
        {
            yield return null;
            EndGame();
        }

        private void EndGame()
        {
            Debug.Log("Игра окончена, лошок!");
            GameOverHandler?.Invoke();
            SaveData(_earnedCoins);
            GameOver();
        }

        protected override void GameOver()
        {

            base.GameOver();
        }
    }
}
