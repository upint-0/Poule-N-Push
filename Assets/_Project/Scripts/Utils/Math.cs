using UnityEngine;

public static class Math
{
    public static float Remap(float value, float min1, float max1, float min2 = 0f, float max2 = 1f, bool mustClampAtMin = false, bool mustClampAtMax = false)
    {
        float remapping = min2 + (value - min1) * ((max2 - min2) / (max1 - min1));

        if(mustClampAtMin)
        {
            remapping = Mathf.Max(remapping, min2);
        }

        if(mustClampAtMax)
        {
            remapping = Mathf.Min(remapping, max2);
        }

        return remapping;
    }
}
