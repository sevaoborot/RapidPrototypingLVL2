using System;
using System.Collections;
using UnityEngine;

namespace minigame002
{
    public class MinigameSetup : MonoBehaviour
    {
        public event Action GameOverHandler;

        [SerializeField] private LevelGeneration _levelGeneration;
        [SerializeField] private Player _player;
        [SerializeField] private MinigameUI _ui;
        [SerializeField] private string _dataFileName;
        [Space]
        [SerializeField] private Transform _startPosition;
        [SerializeField] private float _distanceX;
        [SerializeField] private float _distanceY;

        private int _earnedCoins = 0;

        private ShopAndSkinsData _data;
        private ShopAndSkinsInputOutput _inputOutputManager;

        private void Awake()
        {
            _data = new ShopAndSkinsData();
            _inputOutputManager = new ShopAndSkinsInputOutput(_dataFileName);
            ShopAndSkinsData loadedData = new ShopAndSkinsData();
            loadedData = _inputOutputManager.LoadData();
            if (loadedData == null) throw new NullReferenceException(nameof(loadedData));
            _data = loadedData;
            _levelGeneration.OnInitialize(_distanceX, _distanceY, _startPosition.position, _ui);
            _ui.OnInitialize(this);
            _player.OnInitialize(_distanceX, _distanceY, _startPosition.position, _ui);
            _player.PlayerNotOnStairHandler += GameOver;
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause) _inputOutputManager.SaveData(_data);
        }

        private void OnApplicationQuit()
        {
            _inputOutputManager.SaveData(_data);
        }

        private void SaveData()
        {
            _data.Coins += _earnedCoins;
            _inputOutputManager.SaveData(_data);
        }

        private IEnumerator StairTimer()
        {
            yield return null;
            GameOver();
        }

        private void GameOver()
        {
            Debug.Log("Игра окончена, лошок!");
            GameOverHandler?.Invoke();
        }
    }
}
