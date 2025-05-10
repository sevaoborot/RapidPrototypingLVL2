using System;
using UnityEngine;

public class InitializeShop : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private string _dataFileName;

    private ShopAndSkinsData _data;
    private ShopAndSkinsInputOutput _inputOutputManager;

    private OpenedSkinsChecker _openedSkinsChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    private void Awake()
    {
        _data = new ShopAndSkinsData();
        _inputOutputManager = new ShopAndSkinsInputOutput(_dataFileName);
        ShopAndSkinsData loadedData = new ShopAndSkinsData();
        loadedData = _inputOutputManager.LoadData();
        if (loadedData == null) throw new NullReferenceException(nameof(loadedData));
        _data = loadedData;
        _openedSkinsChecker = new OpenedSkinsChecker(_data);
        _selectedSkinChecker = new SelectedSkinChecker(_data);        
        _shop.OnInitialize(_openedSkinsChecker, _selectedSkinChecker);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) _inputOutputManager.SaveData(_data);
    }

    private void OnApplicationQuit()
    {
        _inputOutputManager.SaveData(_data);
    }
}
