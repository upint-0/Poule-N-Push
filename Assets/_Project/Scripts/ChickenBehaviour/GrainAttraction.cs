using UnityEngine;

public class GrainAttraction : MonoBehaviour
{
    bool _grainIsInAttractionRadius;

    private ChickenData _chickenData;

    public void Initialize(ChickenData data)
    {
        _chickenData = data;
    }

    public Vector3 ComputeDirection(ChickenMultipliers multipliers)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _chickenData.AttractionRadius * multipliers.FoodAttraction, LayerMask.NameToLayer("Grain"));

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
            return _chickenData.AttractionSpeed;
        }

        return 0f;
    }
}
