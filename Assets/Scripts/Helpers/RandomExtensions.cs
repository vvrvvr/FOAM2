
using System;

static class RandomExtensions
{
    public static void Shuffle<T> (this Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1) 
        {
            int k = rng.Next(n--);
            (array[n], array[k]) = (array[k], array[n]);
        }
    }
    public static T GetRandomElement<T>(T[] array)
    {
        int randomIndex = UnityEngine.Random.Range(0, array.Length);
        return array[randomIndex];
    }
}