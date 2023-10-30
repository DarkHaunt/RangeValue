using System.Globalization;
using UnityEditor;
using UnityEngine;


namespace UnityRangeValue.Editor
{
    [CustomPropertyDrawer(typeof(RangeValue))]
    public class RangeValueDrawer : PropertyDrawer
    {
        private const int InspectorBorderSpace = 1;
        private const int MinMaxFieldWidth = 50;
        private const int RectSpace = 7;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects)
                return;
            
            // Data Get

            EditorGUI.BeginChangeCheck();

            var controlRect = EditorGUI.PrefixLabel(position, label);
            var contentRect = GetContentRect(controlRect);

            var minField = property.FindPropertyRelative(nameof(RangeValue.minLimitEditor));
            var maxField = property.FindPropertyRelative(nameof(RangeValue.maxLimitEditor));

            var currentMinField = property.FindPropertyRelative(GetSerializedProperty(nameof(RangeValue.Min)));
            var currentMaxField = property.FindPropertyRelative(GetSerializedProperty(nameof(RangeValue.Max)));
            
            // Values Get & Content Drawing 
            
            var minVal = EditorGUI.FloatField(contentRect.minField, minField.floatValue);
            var maxVal = EditorGUI.FloatField(contentRect.maxField, maxField.floatValue);

            var currentMinVal = currentMinField.floatValue;
            var currentMaxVal = currentMaxField.floatValue;

            EditorGUI.MinMaxSlider(contentRect.slider, ref currentMinVal, ref currentMaxVal,
                minVal, maxVal);
            
            // Validation

            if (minVal > maxVal)
                minVal = maxVal;

            if (currentMaxVal > maxVal)
                currentMaxVal = maxVal;

            if (currentMinVal < minVal)
                currentMinVal = minVal;
            
            if (currentMinVal > currentMaxVal)
                currentMinVal = currentMaxVal;
            
            // Results
            
            var formattedRange = string.Format(CultureInfo.InvariantCulture, "[{0:0.00} : {1:0.00}]", currentMinVal,
                currentMaxVal);

            EditorGUI.LabelField(contentRect.rangeLabel, formattedRange);

            if (EditorGUI.EndChangeCheck())
            {
                currentMinField.floatValue = currentMinVal;
                currentMaxField.floatValue = currentMaxVal;
                maxField.floatValue = maxVal;
                minField.floatValue = minVal;
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        private string GetSerializedProperty(string propName)
            => $"<{propName}>k__BackingField";

        private ContentRect GetContentRect(Rect originRect)
        {
            float labelWidth = originRect.width * 0.25f;

            var label = new Rect(originRect.x, originRect.y, labelWidth, originRect.height);

            var min = new Rect(label.xMax + RectSpace, originRect.y, MinMaxFieldWidth, originRect.height);

            var max = new Rect(originRect.xMax - MinMaxFieldWidth - InspectorBorderSpace, originRect.y,
                MinMaxFieldWidth, originRect.height);

            var slider = new Rect(min.xMax + RectSpace / 2f, originRect.y,
                max.xMin - min.xMax - RectSpace - InspectorBorderSpace, originRect.height);

            return new ContentRect(label, min, max, slider);
        }


        private struct ContentRect
        {
            public readonly Rect minField;
            public readonly Rect maxField;

            public readonly Rect rangeLabel;
            public readonly Rect slider;

            public ContentRect(Rect rangeLabel, Rect minField, Rect maxField, Rect slider)
            {
                this.rangeLabel = rangeLabel;
                this.minField = minField;
                this.maxField = maxField;
                this.slider = slider;
            }
        }
    }
}