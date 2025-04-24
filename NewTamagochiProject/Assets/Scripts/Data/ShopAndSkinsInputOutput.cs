public class ShopAndSkinsInputOutput 
{
    private InputOutputManager _ioManager;
    private string _fileName;

    public ShopAndSkinsInputOutput(string fileName)
    {
        _ioManager = new InputOutputManager(fileName);
    }

    public void SaveData(ShopAndSkinsData data) => _ioManager.SaveToFile<ShopAndSkinsData>(data);

    public ShopAndSkinsData LoadData() => _ioManager.LoadFromFile<ShopAndSkinsData>();
}
