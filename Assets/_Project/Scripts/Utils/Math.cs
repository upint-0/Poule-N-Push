public static class Math
{
    public static float Remap(float value, float min1, float max1, float min2 = 0f, float max2 = 0f)
    {
        return min2 + (value - min1) * ((max2 - min2) / (max1 - min1));
    }
}
