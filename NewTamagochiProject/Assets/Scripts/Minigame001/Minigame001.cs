using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Minigame001 : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _square;
    [Space]
    [Header("Level properties")]
    [SerializeField][Min(2)] private int _numberOfSquares;
    [SerializeField][Min(2)] private float _secondsForLevel;
    private int _numberOfLevels = 0;
    [Header("Color packs")]
    [SerializeField] private ColorPalletsSO _colorPallets;

    private CustomObjectPool _pool;
    private Coroutine _gameCoroutine;

    private RectTransform _squareRectTransform;

    private void Awake()
    {
        OnInitialize();
    }

    public void OnInitialize()
    {
        _squareRectTransform = _square.GetComponent<RectTransform>();
        _pool = new CustomObjectPool(_square, _numberOfSquares * _numberOfSquares);
        _gameCoroutine = StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        SetUpLevel(_numberOfSquares);
        if (_numberOfSquares < 8 && _numberOfSquares == _numberOfLevels)
        {
            _numberOfSquares++;
            _numberOfLevels = 0;
        }
        else _numberOfLevels++;
        yield return new WaitForSeconds(_secondsForLevel);
        Debug.Log("Конец игры!");
    }

    private void StartLevelButton()
    {
        StopCoroutine(_gameCoroutine);
        if (_secondsForLevel > 2) _secondsForLevel -= 0.3f;
        _gameCoroutine = StartCoroutine(StartGame());
    }

    private void EndGameButton()
    {
        StopCoroutine(_gameCoroutine);
        _pool.ReleaseAll();
        Debug.LogWarning("Game Over! Вы выбрали неправильный цвет");
    }

    private void SetUpLevel(int lvlSquareNumber)
    {
        _pool.ReleaseAll(
            (GameObject obj) => obj.GetComponentInChildren<Button>().onClick.RemoveAllListeners());
        int selectedSquare = Random.Range(0, _numberOfSquares * _numberOfSquares);
        Debug.Log(selectedSquare);
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
                gameSquare.name = $"{currentSquare}";
                gameSquare.transform.SetParent(transform, false);
                gameSquare.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPosX, currentPosY);
                if (currentSquare == selectedSquare) {
                    gameSquare.GetComponentInChildren<Image>().color = _colorPallets.Pallets[0].variantColor;
                    gameSquare.GetComponentInChildren<Button>().onClick.AddListener(()=>StartLevelButton());
                } else if (currentSquare != selectedSquare) 
                {
                    gameSquare.GetComponentInChildren<Image>().color = _colorPallets.Pallets[0].baseColor;
                    gameSquare.GetComponentInChildren<Button>().onClick.AddListener(() => EndGameButton());
                }
                currentSquare++;
                currentPosY -= width;
            }
            currentPosX += width;
            currentPosY = topLeftPosY;
        }
    }
}
