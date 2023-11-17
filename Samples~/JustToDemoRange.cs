using UnityEngine.UI;
using UnityEngine;


namespace UnityRangeValue.Demo
{
    public class JustToDemoRange : MonoBehaviour
    {
        [SerializeField] private RangeValue _range;
        
        [Header("--- Demo Stuff---")]
        [SerializeField] private Transform _target;
        [SerializeField] private Button _button;

        private void Start()
        {
            Debug.Log($"<color=magenta>Min - {_range.Min}</color>");
            Debug.Log($"<color=magenta>Max - {_range.Max}</color>");
            
            Debug.Log("<color=white>=== Random Range Test ===</color>");
            
            for (int i = 0; i < 5; i++)
                Debug.Log($"<color=white>{_range.Rand()}</color>");

            Debug.Log("<color=yellow>=== Random Interpolation Test ===</color>");

            for (int i = 0; i < 5; i++)
            {
                var lerp = Random.value;
                Debug.Log($"<color=yellow>Lerp - {lerp} || Value - {_range.Lerp(lerp)}</color>");
            }
            
            Debug.Log("<color=cyan>=== Range Overflow Clamp Test ===</color>");

            var actualMin = _range.Min - 1f;
            var actualMax = _range.Max + 1f;
            
            Debug.Log($"<color=cyan>Actual Min - {actualMin} || Clamped Min - {_range.Clamp(actualMin)}</color>");
            Debug.Log($"<color=cyan>Actual Max - {actualMax} || Clamped Max - {_range.Clamp(actualMax)}</color>");
            
            _button.onClick.AddListener(RandomSetXPos);
        }

        private void RandomSetXPos()
        {
            var pos = Vector3.one * _range.Rand();
            Debug.Log($"<color=cyan>Set in pos - {pos}</color>");
            _target.position = pos;
        }
    }
}