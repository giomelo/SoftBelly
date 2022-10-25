using UnityEditor;
using UnityEngine;
using _Scripts.Enums;

namespace _Scripts.Editor
{
   
    [CustomPropertyDrawer(typeof(EnumFlags))]
    public class EnumFlagsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            _property.intValue = EditorGUI.MaskField( _position, _label, _property.intValue, _property.enumNames );
        }
    }

}