using System;
using UnityEngine;


namespace UnityRangeValue
{
    [Serializable]
    public struct RangeValue
    {
        [field: SerializeField] public float Min { get; private set; }
        [field: SerializeField] public float Max { get; private set; }
    

        public RangeValue(float min, float max)
        {
#if UNITY_EDITOR
        minLimitEditor = min;
        maxLimitEditor = max;
#endif
            if (min > max)
                throw new ArgumentException($"{nameof(RangeValue)} can't have min value bugger than max!\n{min} > {max}");
        
            Min = min;
            Max = max;
        }
    

        public float Rand()
            => UnityEngine.Random.Range(Min, Max);

        public float Clamp(float value)
            => Mathf.Clamp(value, Min, Max);

        public float Lerp(float t)
            => Mathf.Lerp(Min, Max, t);


#if UNITY_EDITOR
    public float minLimitEditor; 
    public float maxLimitEditor; 
#endif
    }
}
