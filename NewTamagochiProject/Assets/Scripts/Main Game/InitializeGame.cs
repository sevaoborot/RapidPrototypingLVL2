using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    [Header("Main game stuff")]
    [SerializeField] private CreatureNeedsUIManager _creatureUI;
    [SerializeField] private CreatureNeedsChange _creatureNeedsChange;
    [SerializeField] private string _creatureDataFileName;

    private TamagochiInputActions _inputActions;
    private CreatureNeeds _needs;
    private GameDataInputOutput _maingameInputOutputManager;

    [Space]
    [Header("Skins stuff")]
    [SerializeField] private CreatureAppearance _creatureAppearance;
    [SerializeField] private string _skinsDataFileName;

    private ShopAndSkinsInputOutput _shopInputOutputManager;
    private ShopAndSkinsData _shopAndSkinsData;

    private bool _isSleeping = false;

    private void Awake()
    {
        _inputActions = new TamagochiInputActions();
        _needs = new CreatureNeeds();
        _maingameInputOutputManager = new GameDataInputOutput(_creatureDataFileName);

        _shopAndSkinsData = new ShopAndSkinsData();
        _shopInputOutputManager = new ShopAndSkinsInputOutput(_skinsDataFileName);

        if (!PlayerPrefs.HasKey("FirstGameRun"))
        {
            Debug.Log("Первый запуск игры!");
            PlayerPrefs.SetInt("FirstGameRun", 1);
            PlayerPrefs.SetString("LastSaveTime", System.DateTime.Now.ToString());
            _needs.SetCreatureNeedsValues(100f, 100f, 100f, 100f);
            _isSleeping = false;
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Уже не первый раз заходите, не так ли?");

            GameData loadedData = new GameData(_needs);
            loadedData = _maingameInputOutputManager.LoadData();
            if (loadedData != null)
            {
                _needs.SetCreatureNeedsValues(loadedData.creatureNeeds);
                _isSleeping = loadedData.isSleeping;
            }
            else _needs.SetCreatureNeedsValues(100f, 100f, 100f, 100f);


            ShopAndSkinsData loadedShopAndSkinsData = new ShopAndSkinsData();
            loadedShopAndSkinsData = _shopInputOutputManager.LoadData();
            if (loadedShopAndSkinsData != null)
            {
                _shopAndSkinsData = loadedShopAndSkinsData;
            } 
        }     
        
        Debug.Log(_needs.ToString());

        _creatureUI.OnInitialize(_needs);
        _needs.InvokeAllNeedsNethods();
        _creatureNeedsChange.OnInitialize(_needs, _creatureUI, _isSleeping);
        _creatureAppearance.OnInitialize(_shopAndSkinsData);

        _creatureUI.OnSleepButtonPressed += SetSleepStatus;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) _maingameInputOutputManager.SaveData(_needs, _isSleeping);
    }

    private void OnApplicationQuit()
    {
        _maingameInputOutputManager.SaveData(_needs, _isSleeping);
        _shopInputOutputManager.SaveData(_shopAndSkinsData);
    }

    private void SetSleepStatus(bool isSleeping) => _isSleeping = isSleeping;
}
