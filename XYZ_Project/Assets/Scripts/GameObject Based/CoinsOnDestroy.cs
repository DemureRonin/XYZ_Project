using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Scripts
{
    public class CoinsOnDestroy : MonoBehaviour
    {
        [SerializeField] private GameObject _silverCoinOnDestroyPrefab;
        [SerializeField] private GameObject _goldCoinOnDestroyPrefab;

        [SerializeField] private int _coinOnDestroyAmount;
        [SerializeField] private Transform _coinPosition;
        [SerializeField][Range(0, 100)] private float _silverCoinChancePercent;

        public void StartSpawningCoins()
        {
            StartCoroutine(DropCoins());
        }
        private void SpawnCoins()
        {
            if (_silverCoinChancePercent > Random.value*100)
            {
                Instantiate(_silverCoinOnDestroyPrefab, _coinPosition.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_goldCoinOnDestroyPrefab, _coinPosition.position, Quaternion.identity);
            }
        }
        IEnumerator DropCoins()
        {
            for (int i = 0; i < _coinOnDestroyAmount; i++)
            {
                SpawnCoins();
            }
            yield return null;
        }

    }
}

