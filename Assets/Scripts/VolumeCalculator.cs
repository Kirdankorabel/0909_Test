using UnityEngine;

public static class VolumeCalculator
{
    public static float RadiusToVolume(float radius)
    {
        return (4f / 3f) * Mathf.PI * Mathf.Pow(radius, 3f);
    }

    public static float VolumeToRadius(float volume)
    {
        return Mathf.Pow((3f * volume) / (4f * Mathf.PI), 1f / 3f);
    }

    public static float AddVolume(float currentRadius, float addedVolume)
    {
        float totalVolume = RadiusToVolume(currentRadius) + addedVolume;
        return VolumeToRadius(totalVolume);
    }

    public static float RemoveVolume(float currentRadius, float removedVolume)
    {
        float currentVolume = RadiusToVolume(currentRadius);
        float newVolume = Mathf.Max(currentVolume - removedVolume, 0f);
        return VolumeToRadius(newVolume);
    }

    public static float GetVolume(float radius)
    {
        return RadiusToVolume(radius);
    }

    public static float GetRemainingPercentage(float startRadius, float currentRadius)
    {
        float startVolume = RadiusToVolume(startRadius);
        float currentVolume = RadiusToVolume(currentRadius);

        if (startVolume <= 0f) return 0f;

        return currentVolume / startVolume;
    }
}