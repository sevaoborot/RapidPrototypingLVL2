using System;
using UnityEngine;
using UnityEngine.UI;

namespace minigame003
{
    public class Minigame003 : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject _square;
        [Space]
        [Header("Level properties")]
        [SerializeField][Min(2)] private int _numberOfSquares;
        [SerializeField][Min(2)] private float _secondsForLevel;
        [Header("Color packs")]
        //[SerializeField] private ColorPalletsSO _colorPallets; //сделать отдельный паллетс СО для миниигры
        [Header("Other")]
        [SerializeField] private string _dataFileName;

        //private int _numberOfColoredSquares;

        private CustomObjectPool _pool;
        private RectTransform _squareRectTransform;

        private ShopAndSkinsData _data;
        private ShopAndSkinsInputOutput _inputOutputManager;

        private void Awake()
        {
            OnInitialize();
        }

        public void OnInitialize()
        {
            _squareRectTransform = _square.GetComponent<RectTransform>();
            _pool = new CustomObjectPool(_square, _numberOfSquares * _numberOfSquares);
            _data = new ShopAndSkinsData();
            _inputOutputManager = new ShopAndSkinsInputOutput(_dataFileName);
            ShopAndSkinsData loadedData = new ShopAndSkinsData();
            loadedData = _inputOutputManager.LoadData();
            if (loadedData == null) throw new NullReferenceException(nameof(loadedData));
            _data = loadedData;
            SetUpLevel(); //потом в корутин убрать 
        }

        private void SetUpLevel()
        {
            _pool.ReleaseAll();
            int selectedSquares = UnityEngine.Random.Range(1, _numberOfSquares * _numberOfSquares);
            Debug.Log(selectedSquares);
            //тут рандомим палетку, но это позже
            float width = _squareRectTransform.rect.width;
            float lvlSquareLength = (_numberOfSquares - 1) * width;
            float topLeftPosX = -lvlSquareLength / 2;
            float topLeftPosY = -topLeftPosX;
            float currentPosX = topLeftPosX;
            float currentPosY = topLeftPosY;
            int coloredSquares = 0;
            for (int i = 0; i < _numberOfSquares; i++)
            {
                for (int j = 0; j < _numberOfSquares; j++)
                {
                    GameObject gameSquare = _pool.Get();
                    gameSquare.transform.SetParent(transform, false);
                    gameSquare.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPosX, currentPosY);
                    if (coloredSquares != selectedSquares && UnityEngine.Random.Range(-1,1) == 0)
                    {
                        gameSquare.GetComponentInChildren<Image>().color = Color.white;
                        coloredSquares++;
                    }
                    currentPosY -= width;
                    //продолжить тута прописывать логику генерации
                }
                currentPosX += width;
                currentPosY = topLeftPosY;
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause) _inputOutputManager.SaveData(_data);
        }

        private void OnApplicationQuit() => _inputOutputManager.SaveData(_data);

        private void SaveData()
        {
            //_data.Coins += _earnedCoins;
            _inputOutputManager.SaveData(_data);
        }
    }
}
