using UnityEngine;

public class GrainAttraction : MonoBehaviour
{
    [SerializeField] private float _attractionRadius;
    [SerializeField] private float _attractionSpeed;

    bool _grainIsInAttractionRadius;

    public Vector3 ComputeDirection(ChickenMultipliers multipliers)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _attractionRadius * multipliers.FoodAttraction, LayerMask.NameToLayer("Grain"));

        if(colliders.Length > 0)
        {
            _grainIsInAttractionRadius = true;

            return (colliders[0].transform.position - transform.position).normalized;
        }

        return Vector3.zero;
    }

    public float ComputeSpeed()
    {
        if(_grainIsInAttractionRadius)
        {
            return _attractionSpeed;
        }

        return 0f;
    }
}
