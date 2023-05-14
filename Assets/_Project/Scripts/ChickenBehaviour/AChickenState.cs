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
            module.SetEnabled(_chicken.Data[this.Type][module.Type].IsEnabled);
        }
    }

    public virtual void ExecuteModules()
    {
        float weightSum = 0f;
        Vector3 resultingDirection = Vector3.zero;
        float resultingSpeed = 0f;

        foreach(AChickenModule module in _chicken.ChickenModules)
        {
            if(module.IsEnabled)
            {
                ChickenModuleData moduleData = _chicken.Data[Type][module.Type];
                weightSum += moduleData.Weight;
                module.Execute(moduleData);
                resultingDirection += module.ResultingDirection * moduleData.Weight;
                resultingSpeed = Mathf.Max(resultingSpeed, module.ResultingSpeed);
            }
        }

        _chicken.Movement.CurrentDirection = (resultingDirection / weightSum).normalized;
        _chicken.Movement.CurrentSpeed = resultingSpeed;

        // probabilité de dzin en fonction de la distance du joueur et de la distance des poulets dzinés
    }

    protected abstract void SetState();
}
