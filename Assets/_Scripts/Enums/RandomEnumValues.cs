using System;

namespace _Scripts.Enums
{
    public static class RandomEnumValues
    {
        static Random _R = new();
        public static T RandomEnumValue<T> ()
        {
            var v = Enum.GetValues (typeof (T));
            return (T) v.GetValue (_R.Next(v.Length - 1));
        }
    }
}