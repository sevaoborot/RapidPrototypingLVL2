﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace minigame003
{
    public class Minigame003 : Minigame
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
        [SerializeField] private string _shopDataFileName;
        [SerializeField] private string _needsDataFileName;

        private int _numberOfLevels = 0;
        private int _earnedCoins = 0;
        private int _coloredByPlayerSquares = 0;

        private int _startNumberOfSquares;

        private CustomObjectPool _pool;
        private Coroutine _gameCoroutine;

        private RectTransform _squareRectTransform;

        public override void OnInitialize(Action gameOverAction)
        {
            _startNumberOfSquares = _numberOfSquares;
            _shopDataFile = _shopDataFileName;
            _needsDataFile = _needsDataFileName;
            base.OnInitialize(gameOverAction);
            _squareRectTransform = _square.GetComponent<RectTransform>();
            if (_pool == null)
                _pool = new CustomObjectPool(_square, _numberOfSquares * _numberOfSquares);
            _gameCoroutine = StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            //выбираем рандомную палетку
            SetUpLevel();
            if (_numberOfSquares < 8 && _numberOfSquares == _numberOfLevels)
            {
                _numberOfSquares++;
                _numberOfLevels = 0;
            }
            else _numberOfLevels++;
            yield return new WaitForSeconds(_secondsForLevel);
            _pool.ReleaseAll();
            Debug.LogWarning("Конец игры!");
            GameOver();
        }

        private void SetUpLevel()
        {
            Debug.Log(_numberOfSquares *  _numberOfLevels);
            _pool.ReleaseAll(
                        (GameObject obj) => obj.GetComponentInChildren<Button>().onClick.RemoveAllListeners());
            int selectedSquares = UnityEngine.Random.Range(1, _numberOfSquares * _numberOfSquares - 1);
            _coloredByPlayerSquares = selectedSquares;
            Debug.Log(selectedSquares);
            List<int> selectedSquaresIndexes = new List<int>();
            for (int i = 0; i < _numberOfSquares * _numberOfSquares; i++) selectedSquaresIndexes.Add(i);
            for (int i = selectedSquaresIndexes.Count - 1; i > 0 ; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (selectedSquaresIndexes[i], selectedSquaresIndexes[j]) = (selectedSquaresIndexes[j], selectedSquaresIndexes[i]);
            }
            selectedSquaresIndexes = selectedSquaresIndexes.GetRange(0, selectedSquares);
            float width = _squareRectTransform.rect.width;
            float lvlSquareLength = (_numberOfSquares - 1) * width;
            float topLeftPosX = -lvlSquareLength / 2;
            float topLeftPosY = -topLeftPosX;
            float currentPosX = topLeftPosX;
            float currentPosY = topLeftPosY;
            int currentSquareIndex = 0;
            for (int i = 0; i < _numberOfSquares; i++)
            {
                for (int j = 0; j < _numberOfSquares; j++)
                {
                    GameObject gameSquare = _pool.Get();
                    gameSquare.transform.SetParent(transform, false);
                    gameSquare.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPosX, currentPosY);
                    Image gameSquareImage = gameSquare.GetComponentInChildren<Image>();
                    Button gameSquareButton = gameSquare.GetComponentInChildren<Button>();
                    if (selectedSquaresIndexes.Contains(currentSquareIndex))
                    {
                        gameSquareImage.color = Color.white;
                        gameSquareButton.onClick.AddListener(() => Color1Button(gameSquareImage, gameSquareButton));
                    }
                    else
                    {
                        gameSquareImage.color = Color.red;
                        gameSquareButton.onClick.AddListener(() => Color2Button(gameSquareImage, gameSquareButton));
                    }
                    currentPosY -= width;
                    currentSquareIndex++; 
                }
                currentPosX += width;
                currentPosY = topLeftPosY;
            }
        }

        private void Color1Button(Image image, Button button)
        {
            image.color = Color.red;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => Color2Button(image, button));
            _coloredByPlayerSquares--;
            CheckIfAllSquaresTheSameColor();
        }

        private void Color2Button(Image image, Button button)
        {
            image.color = Color.white;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => Color1Button(image, button));
            _coloredByPlayerSquares++;
            CheckIfAllSquaresTheSameColor();
        }

        private void CheckIfAllSquaresTheSameColor()
        {
            Debug.Log(_coloredByPlayerSquares);
            if (_coloredByPlayerSquares == 0 || _coloredByPlayerSquares == _numberOfSquares * _numberOfSquares)
            {
                if (_gameCoroutine != null) StopCoroutine(_gameCoroutine);
                _gameCoroutine = StartCoroutine(StartGame());
                _earnedCoins++;
            }
        }

        protected override void GameOver()
        {
            SaveData(_earnedCoins);
            _numberOfSquares = _startNumberOfSquares;
            _numberOfLevels = 0;
            _earnedCoins = 0;
            base.GameOver();
        }
    }
}
