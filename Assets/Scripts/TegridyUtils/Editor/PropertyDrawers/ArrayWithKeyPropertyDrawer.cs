using TegridyUtils.Attributes;
using UnityEditor;
using UnityEngine;

namespace TegridyUtils.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ArrayWithKeyAttribute))]
    public class ArrayWithKeyPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
            GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            var newLabel = "";
            var originProperty = property.Copy();

            var nextSiblingProperty = property.Copy();
            nextSiblingProperty.Next(false);

            var originPropertyObject = EditorUtils.GetTargetObjectOfProperty(originProperty);
            var originObjectType = originPropertyObject.GetType();

            while (true)
            {
                var hasNext = property.Next(true);

                if (hasNext == false || SerializedProperty.EqualContents(nextSiblingProperty, property))
                {
                    break;
                }

                var currentField = originObjectType.GetField(property.name);

                if (currentField == null)
                {
                    break;
                }

                var customAttributes = currentField.GetCustomAttributes(typeof(ArrayKeyAttribute), true);

                if (customAttributes.Length != 0)
                {
                    newLabel = EditorUtils.GetTargetObjectOfProperty(property).ToString();
                    break;
                }
            }

            label.text = newLabel;

            EditorGUI.PropertyField(position, originProperty, label, true);
        }
    }
}