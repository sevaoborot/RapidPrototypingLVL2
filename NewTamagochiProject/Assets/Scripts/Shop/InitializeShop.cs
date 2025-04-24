using UnityEngine;

public class InitializeShop : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private string _dataFileName;

    private ShopAndSkinsData _data;
    private ShopAndSkinsInputOutput _inputOutputManager;

    private void Awake()
    {
        _data = new ShopAndSkinsData();
        _inputOutputManager = new ShopAndSkinsInputOutput(_dataFileName);

        //тут надо как-то прописать инициализацию при первом заапуске и при дальнейших запусках 
        _shop.OnInitialize();
    }
}
