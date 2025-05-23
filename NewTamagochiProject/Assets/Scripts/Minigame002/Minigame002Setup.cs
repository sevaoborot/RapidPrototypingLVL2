using System;
using UnityEngine;

namespace minigame002
{
    public class Minigame002Setup : MonoBehaviour
    {
        [SerializeField] private LevelGeneration _levelGeneration;
        [SerializeField] private StairPlayer _player;
        [SerializeField] private StairButtons _buttons;
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
            _levelGeneration.OnInitialize(_distanceX, _distanceY, _startPosition.position);
            _player.OnInitialize(_distanceX, _distanceY, _startPosition.position, _buttons);
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
    }
}
