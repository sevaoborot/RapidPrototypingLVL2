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

        private ShopAndSkinsData _shopData;
        private ShopAndSkinsInputOutput _inputOutputManager;
        private CreatureNeeds _needs;
        private GameDataInputOutput _needsInputOutputManager;

        public override void OnInitialize()
        {
            _shopData = new ShopAndSkinsData();
            _inputOutputManager = new ShopAndSkinsInputOutput(_shopDataFileName);
            ShopAndSkinsData loadedShopData = new ShopAndSkinsData();
            loadedShopData = _inputOutputManager.LoadData();
            if (loadedShopData == null) throw new NullReferenceException(nameof(loadedShopData));
            _shopData = loadedShopData;

            _needs = new CreatureNeeds();
            _needsInputOutputManager = new GameDataInputOutput(_needsDataFileName);
            GameData loadedGameData = new GameData(_needs);
            loadedGameData = _needsInputOutputManager.LoadData();
            if (loadedGameData == null) throw new NullReferenceException(nameof(loadedGameData));
            _needs.SetCreatureNeedsValues(loadedGameData.creatureNeeds);

            _levelGeneration.OnInitialize(_distanceX, _distanceY, _startPosition.position, _ui);
            _ui.OnInitialize(this);
            _player.OnInitialize(_distanceX, _distanceY, _startPosition.position, _ui);
            _player.PlayerNotOnStairHandler += GameOver;
        }

        private void SaveData()
        {
            _shopData.Coins += _earnedCoins;
            _inputOutputManager.SaveData(_shopData);
            _needs.happiness += 20f;
            _needsInputOutputManager.SaveData(_needs, false);
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
            SaveData();
        }
    }
}
