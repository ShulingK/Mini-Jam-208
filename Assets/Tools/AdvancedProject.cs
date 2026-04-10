using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class AdvancedProject
{
    static string[] authors = { "// System", "// ScriptableObject", "// Manager", "// Interface"};

    static Color[] colors = {

        new Color (.235f, .380f, .219f, .2f), 
        new Color (.717f, .2f, .172f, .2f), 
        new Color (.504f, .1f, .799f, .2f),
        new Color (.7f, .2f, .2f, .2f)
    };  

    static AdvancedProject()
    {
        EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemOnGUI;
    }

    private static void OnProjectWindowItemOnGUI(string guid, Rect selectionRect)
    {
        string path = AssetDatabase.GUIDToAssetPath(guid);

        if (path.Contains(".cs"))
        {
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));

            Type type = obj.GetType();

            GUIContent content = EditorGUIUtility.ObjectContent(obj, type);

            string line = File.ReadLines(path).First();

            Rect backgroundRectBg = selectionRect;
            backgroundRectBg.width = 200f;
            //selectionRect.x += 16f;

            for (int i = 0; i < authors.Length; i++)
            {
                if (line.Contains(authors[i]))
                {
                    Color colorBg = colors[i];
                    EditorGUI.DrawRect(backgroundRectBg, colorBg);
                    content.text = " " + content.text + " " + authors[i].Replace("//", "-");
                    EditorGUI.LabelField(selectionRect, content);
                    Debug.Log(colorBg);
                }
            }
        }
    }
}
