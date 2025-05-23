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

        private Vector3 _startPosition;
        private float _distanceX;
        private float _distanceY;

        private CustomObjectPool _stairsPool;
        private CustomObjectPool _coinsPool;

        public void OnInitialize(float distanceX, float distanceY, Vector3 startPosition)
        {
            _distanceX = distanceX;
            _distanceY = distanceY;
            _startPosition = startPosition;
            _stairsPool = new CustomObjectPool(_stair, _maxStairsRendered);
            StartGenerating();
        }

        private void StartGenerating()
        {
            GameObject currentStair = _stairsPool.Get();
            currentStair.transform.position = _startPosition;
            for (int i = 0; i < _maxStairsRendered; i++)
            {
                GameObject newStair;
                if (Random.Range(-1, 1) == 0) newStair = StairSpawn(_distanceX, currentStair);
                else newStair = StairSpawn(-_distanceX, currentStair);
                currentStair = newStair;
            }
        }

        private void StairsGenerator() //если какая-то ступенька более не видна в кадре 
        {
            //_pool.Release
            //_pool.Get
        }

        private GameObject StairSpawn(float distanceX, GameObject previousStair)
        {
            GameObject stair = _stairsPool.Get();
            stair.transform.position = new Vector3(previousStair.transform.position.x + distanceX,
                previousStair.transform.position.y + _distanceY,
                previousStair.transform.position.z);
            //тут же если надо спавним монетку
            return stair;
        }

        private GameObject CoinSpawn()
        {
            GameObject coin = _coinsPool.Get();
            return coin;
        }
    }

}