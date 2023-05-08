using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class VisibleCohesion : MonoBehaviour
{
    BoxCollider _visionCollider;
    HashSet<GameObject> _chickensinView = new HashSet<GameObject>();

    private void Start()
    {
        _visionCollider = GetComponent<BoxCollider>();
    }

    public Vector3 ComputeDirection(ChickenMultipliers multipliers)
    {
        if(_chickensinView.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 averagePosition = Vector3.zero;

        foreach(GameObject chicken in _chickensinView)
        {
            averagePosition += chicken.transform.position;
        }

        averagePosition = averagePosition / _chickensinView.Count;

        Vector3 direction = (averagePosition - transform.position) * multipliers.OtherChickens;

        return direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Chicken")   // if it lags, look here
        {
            _chickensinView.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Chicken")
        {
            _chickensinView.Remove(other.gameObject);
        }
    }
}
