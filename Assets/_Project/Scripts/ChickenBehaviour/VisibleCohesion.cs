using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class VisibleCohesion : AChickenModule
{
    BoxCollider _visionCollider;
    HashSet<GameObject> _chickensinView = new HashSet<GameObject>();

    private void Start()
    {
        _visionCollider = GetComponent<BoxCollider>();
    }

    public override void Initialize(ChickenCore chicken)
    {
        base.Initialize(chicken);

        Type = ChickenModuleType.VisibleCohesion;
    }

    public override void Execute(ChickenModuleData moduleData)
    {
        if(_chickensinView.Count == 0)
        {
            ResultingDirection = Vector3.zero;
        }
        else
        {
            Vector3 averagePosition = Vector3.zero;

            foreach(GameObject chicken in _chickensinView)
            {
                averagePosition += chicken.transform.position;
            }

            averagePosition = averagePosition / _chickensinView.Count;

            ResultingDirection = (averagePosition - transform.position) * moduleData.Multiplier;
        }
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
