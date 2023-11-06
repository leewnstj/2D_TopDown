using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColorInHierarchy : MonoBehaviour
{
#if UNITY_EDITOR

    private static Dictionary<Object, ColorInHierarchy> _coloredObjects = new Dictionary<Object, ColorInHierarchy>();

    static ColorInHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int instanceID, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceID); //�ν��Ͻ� ���̵� �ָ� ������ ������Ʈ ��ȯ

        if (obj != null && _coloredObjects.ContainsKey(obj))
        {
            GameObject gameObj = obj as GameObject;
            ColorInHierarchy cih = gameObj.GetComponent<ColorInHierarchy>();
            if (cih != null)
            {
                //Debug.Log("AAA");
                PaintObject(obj, selectionRect, cih);
            }
            else
            {
                _coloredObjects.Remove(obj); //����ڰ� ������Ʈ ������ ��
            }
        }
    }

    public static void PaintObject(Object obj, Rect selectionRect, ColorInHierarchy cih)
    {
        Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height);

        if (Selection.activeObject != obj)
        {
            EditorGUI.DrawRect(bgRect, cih.BackColor);

            string name = $"{cih.prefix}{obj.name}";

            EditorGUI.LabelField(bgRect, name, new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = cih.FrontColor },
                fontStyle = FontStyle.Bold
            });
        }
    }

    public string prefix;
    public Color BackColor;
    public Color FrontColor;
    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        if (_coloredObjects.ContainsKey(this.gameObject) == false)
        {
            _coloredObjects.Add(this.gameObject, this);
        }
    }

#endif
}
