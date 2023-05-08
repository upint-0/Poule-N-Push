using System;
using System.Collections.Generic;
using UnityEngine;

public class ChickensManager : MonoBehaviour
{
    [SerializeField] private int _chickenCount;
    [SerializeField] private ChickenCore _chickenPrefab;
    [SerializeField] private float Radius;

    private HashSet<ChickenCore> _chickens = new HashSet<ChickenCore>();

    private void Start()
    {
        SpawnChickens();
        RoundManager.Instance.RestartRound.AddListener( ResetChickens );
    }

    private void ResetChickens()
    {
        foreach ( var chicken in _chickens )
        {
            if(chicken)
                Destroy( chicken.gameObject );
        }

        SpawnChickens();
    }

    private void SpawnChickens()
    {
        for(int i = 0; i < _chickenCount; i++)
        {
            SpawnChicken();
        }
    }

    private void SpawnChicken()
    {
        Vector2 random2dPosition = UnityEngine.Random.insideUnitCircle * Radius;
        Vector3 randomPosition = new Vector3(random2dPosition.x, 0.5f, random2dPosition.y);
        ChickenCore chicken = Instantiate(_chickenPrefab, randomPosition, UnityEngine.Random.rotation, transform);
        _chickens.Add(chicken);
    }

    private void RemoveChicken(ChickenCore chicken)
    {
        Destroy(chicken.gameObject);
        _chickens.Remove(chicken);
    }
}
