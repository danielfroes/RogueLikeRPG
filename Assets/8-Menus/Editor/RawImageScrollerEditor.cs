using System;
using Scripts;
using Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace TurboRace.Editor
{
    
    [CustomEditor(typeof(RawImageScroller))]
    public class RawImageScrollerEditor : UnityEditor.Editor
    {
        const string ImagePropertyName = "_image";
        const string HorizontalTogglePropertyName = "_scrollHorizontally";
        const string HorizontalSpeedPropertyName = "_horizontalSpeed";
        const string VerticalTogglePropertyName = "_scrollVertically";
        const string VerticalSpeedPropertyName = "_verticalSpeed";

        SerializedObject _scrollerObject;
        SerializedProperty _imageProperty;
        SerializedProperty _horizontalToggleProperty;
        SerializedProperty _horizontalSpeedProperty;
        SerializedProperty _verticalToggleProperty;
        SerializedProperty _verticalSpeedProperty;

        void OnEnable()
        {
            _scrollerObject = serializedObject;
            
            _imageProperty = serializedObject.FindProperty(ImagePropertyName);
            _horizontalToggleProperty = _scrollerObject.FindProperty(HorizontalTogglePropertyName);
            _horizontalSpeedProperty = _scrollerObject.FindProperty(HorizontalSpeedPropertyName);
            _verticalToggleProperty = _scrollerObject.FindProperty(VerticalTogglePropertyName);
            _verticalSpeedProperty = _scrollerObject.FindProperty(VerticalSpeedPropertyName); 
            
            _imageProperty.objectReferenceValue = (target as MonoBehaviour).GetComponent<RawImage>(); 
            
            _scrollerObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            _scrollerObject.Update();
            
            EditorGUILayout.PropertyField(_horizontalToggleProperty);
            if (_horizontalToggleProperty.boolValue)
            {
                EditorGUILayout.PropertyField(_horizontalSpeedProperty);
            }

            EditorGUILayout.PropertyField(_verticalToggleProperty);
            if (_verticalToggleProperty.boolValue)
            {
                EditorGUILayout.PropertyField(_verticalSpeedProperty);
            }
            
            _scrollerObject.ApplyModifiedProperties();
        }
    }
}