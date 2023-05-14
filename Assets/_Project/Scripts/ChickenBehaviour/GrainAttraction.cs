using UnityEngine;

public class GrainAttraction : AChickenModule
{
    public override void Execute(ChickenModuleData moduleData)
    {
        bool grainIsInAttractionRadius = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, _chicken.Data.AttractionRadius * moduleData.Multiplier, LayerMask.NameToLayer("Grain"));

        if(colliders.Length > 0)
        {
            grainIsInAttractionRadius = true;

            ResultingDirection = (colliders[0].transform.position - transform.position).normalized;
        }
        else
        {
            ResultingDirection = Vector3.zero;
        }

        ResultingSpeed = grainIsInAttractionRadius ? _chicken.Data.AttractionSpeed : 0f;
    }
}
