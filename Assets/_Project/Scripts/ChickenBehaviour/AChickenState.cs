using UnityEngine;

public enum ChickenStateType
{
    Idle = 0,
    Wandering = 1,
    Dzin = 2,
    Eating = 3,
}

public abstract class AChickenState
{
    public ChickenStateType Type { get; set; }
    protected ChickenCore _chicken;

    public AChickenState(ChickenCore chicken)
    {
        _chicken = chicken;
        SetState();

        foreach(AChickenModule module in _chicken.ChickenModules)
        {
            module.gameObject.SetActive(_chicken.Data[this.Type][module.Type].IsEnabled);
        }
    }

    public virtual void ExecuteModules()
    {
        float weightSum = 0f;
        Vector3 resultingDirection = Vector3.zero;
        float resultingSpeed = 0f;

        foreach(AChickenModule module in _chicken.ChickenModules)
        {
            ChickenModuleData moduleData = _chicken.Data[Type][module.Type];
            if(moduleData.IsEnabled)
            {
                weightSum += moduleData.Weight;
                module.Execute(moduleData);
                resultingDirection += module.ResultingDirection * moduleData.Weight;
                resultingSpeed += module.ResultingSpeed * moduleData.Weight;
            }
        }

        _chicken.Movement.CurrentDirection = resultingDirection / weightSum;
        _chicken.Movement.CurrentSpeed = resultingSpeed / weightSum;

        // probabilité de dzin en fonction de la distance du joueur et de la distance des poulets dzinés
    }

    protected abstract void SetState();
}
