using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace minigame001
{
    public class Minigame001 : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject _square;
        [Space]
        [Header("Level properties")]
        [SerializeField][Min(2)] private int _numberOfSquares;
        [SerializeField][Min(2)] private float _secondsForLevel;
        [Header("Color packs")]
        [SerializeField] private ColorPalletsSO _colorPallets;
        [Header("Other")]
        [SerializeField] private string _dataFileName;

        private int _numberOfLevels = 0;
        private int _numberOfDiffChange = 0;
        private int _earnedCoins = 0;

        private CustomObjectPool _pool;
        private Coroutine _gameCoroutine;

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
            _gameCoroutine = StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            ColorPallete[] currentPallete = new ColorPallete[0];
            if (_numberOfDiffChange < 10) currentPallete = _colorPallets.easyPallets;
            else currentPallete = _colorPallets.midPallets;
            _numberOfDiffChange++;
            SetUpLevel(_numberOfSquares, currentPallete);
            if (_numberOfSquares < 8 && _numberOfSquares == _numberOfLevels)
            {
                _numberOfSquares++;
                _numberOfLevels = 0;
            }
            else _numberOfLevels++;
            yield return new WaitForSeconds(_secondsForLevel);
            _pool.ReleaseAll();
            Debug.LogWarning("Конец игры!");
            SaveData();
        }

        private void CorrectButton()
        {
            StopCoroutine(_gameCoroutine);
            if (_secondsForLevel > 2) _secondsForLevel -= 0.3f;
            _gameCoroutine = StartCoroutine(StartGame());
            _earnedCoins++;
        }

        private void IncorrectButon()
        {
            StopCoroutine(_gameCoroutine);
            _pool.ReleaseAll();
            Debug.LogWarning("Game Over! Вы выбрали неправильный цвет");
            SaveData();
        }

        private void SetUpLevel(int lvlSquareNumber, ColorPallete[] currentPallete)
        {
            _pool.ReleaseAll(
                (GameObject obj) => obj.GetComponentInChildren<Button>().onClick.RemoveAllListeners());
            int selectedSquare = UnityEngine.Random.Range(0, _numberOfSquares * _numberOfSquares);
            int selectedPallete = UnityEngine.Random.Range(0, _colorPallets.midPallets.Length);
            float width = _squareRectTransform.rect.width;
            float lvlSquareLength = (lvlSquareNumber - 1) * width;
            float topLeftPosX = -lvlSquareLength / 2;
            float topLeftPosY = -topLeftPosX;
            float currentPosX = topLeftPosX;
            float currentPosY = topLeftPosY;
            int currentSquare = 0;
            for (int i = 0; i < _numberOfSquares; i++)
            {
                for (int j = 0; j < _numberOfSquares; j++)
                {
                    GameObject gameSquare = _pool.Get();
                    //gameSquare.name = $"{currentSquare}";
                    gameSquare.transform.SetParent(transform, false);
                    gameSquare.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPosX, currentPosY);
                    if (currentSquare == selectedSquare)
                    {
                        gameSquare.GetComponentInChildren<Image>().color = currentPallete[selectedPallete].variantColor;
                        gameSquare.GetComponentInChildren<Button>().onClick.AddListener(() => CorrectButton());
                    }
                    else if (currentSquare != selectedSquare)
                    {
                        gameSquare.GetComponentInChildren<Image>().color = currentPallete[selectedPallete].baseColor;
                        gameSquare.GetComponentInChildren<Button>().onClick.AddListener(() => IncorrectButon());
                    }
                    currentSquare++;
                    currentPosY -= width;
                }
                currentPosX += width;
                currentPosY = topLeftPosY;
            }
        }

        private void SaveData()
        {
            _data.Coins += _earnedCoins;
            _inputOutputManager.SaveData(_data);
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause) _inputOutputManager.SaveData(_data);
        }

        private void OnApplicationQuit() => _inputOutputManager.SaveData(_data);
    }
}

