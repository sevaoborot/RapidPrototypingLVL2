using UnityEngine;

namespace minigame002
{
    public class LevelGeneration : MonoBehaviour
    {
        [Header("Stair settings")]
        [SerializeField] private GameObject _stair;
        [SerializeField] private int _maxStairsRendered;
        [Header("Coins settings")]
        [SerializeField] private GameObject _coin;

        private MinigameUI _ui;

        private Vector3 _startPosition;
        private float _distanceX;
        private float _distanceY;

        private CustomObjectPool _stairsPool;
        private CustomObjectPool _coinsPool;
        private GameObject _currentStair;

        public void OnInitialize(float distanceX, float distanceY, Vector3 startPosition, MinigameUI ui)
        {
            _ui = ui;
            _ui.Jump += StairsGenerator;
            _ui.Rotate += StairsGenerator;
            _distanceX = distanceX;
            _distanceY = distanceY;
            _startPosition = startPosition;
            _stairsPool = new CustomObjectPool(_stair, _maxStairsRendered);
            StartGenerating();
        }

        private void StartGenerating()
        {
            _currentStair = _stairsPool.Get();
            _currentStair.transform.position = _startPosition;
            for (int i = 0; i < _maxStairsRendered; i++) StairSpawn();
        }

        private void StairsGenerator()
        { 
            _stairsPool.ReleaseBy(obj => Camera.main.WorldToViewportPoint(obj.transform.position).y < 0f);
            StairSpawn();
        }

        private void StairSpawn()
        {
            GameObject stair = _stairsPool.Get();
            stair.transform.SetParent(transform, false);
            if (Random.Range(-1, 1) == 0) stair.transform.position = new Vector3(_currentStair.transform.position.x + _distanceX,
                _currentStair.transform.position.y + _distanceY,
                _currentStair.transform.position.z);
            else stair.transform.position = new Vector3(_currentStair.transform.position.x + -_distanceX,
                _currentStair.transform.position.y + _distanceY,
                _currentStair.transform.position.z);
            //тут же если надо спавним монетку
            _currentStair = stair;
        }

        private GameObject CoinSpawn()
        {
            GameObject coin = _coinsPool.Get();
            return coin;
        }
    }

}