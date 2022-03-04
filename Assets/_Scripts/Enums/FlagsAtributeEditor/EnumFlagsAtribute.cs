using UnityEditor;
using UnityEngine;

namespace _Scripts.Editor.FlagsAtributeEditor
{
    public class EnumFlagsAtribute : PropertyAttribute
    {
        public EnumFlagsAtribute() { }
    }
 
    [CustomPropertyDrawer(typeof(EnumFlagsAtribute))]
    public class EnumFlagsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            _property.intValue = EditorGUI.MaskField( _position, _label, _property.intValue, _property.enumNames );
        }
    }

}