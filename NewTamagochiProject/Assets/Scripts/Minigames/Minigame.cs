using System;
using UnityEngine;

public abstract class Minigame: MonoBehaviour
{
    protected string _shopDataFile;
    protected string _needsDataFile;
    protected Action _gameOverAction;
    private ShopAndSkinsData _shopData;
    private ShopAndSkinsInputOutput _inputOutputManager;
    private CreatureNeeds _needs;
    private GameDataInputOutput _needsInputOutputManager;

    public virtual void OnInitialize(Action gameOverAction)
    {
        _shopData = new ShopAndSkinsData();
        _inputOutputManager = new ShopAndSkinsInputOutput(_shopDataFile);
        ShopAndSkinsData loadedShopData = new ShopAndSkinsData();
        loadedShopData = _inputOutputManager.LoadData();
        if (loadedShopData == null) throw new NullReferenceException(nameof(loadedShopData));
        _shopData = loadedShopData;
        _needs = new CreatureNeeds();
        _needsInputOutputManager = new GameDataInputOutput(_needsDataFile);
        GameData loadedGameData = new GameData(_needs);
        loadedGameData = _needsInputOutputManager.LoadData();
        if (loadedGameData == null) throw new NullReferenceException(nameof(loadedGameData));
        _needs.SetCreatureNeedsValues(loadedGameData.creatureNeeds);
        _gameOverAction = gameOverAction;
    }

    protected void SaveData(int earnedCoins)
    {
        _shopData.Coins += earnedCoins;
        _inputOutputManager.SaveData(_shopData);
        _needs.happiness += 20f;
        _needsInputOutputManager.SaveData(_needs, false);
    }

    protected virtual void GameOver() => _gameOverAction();
}