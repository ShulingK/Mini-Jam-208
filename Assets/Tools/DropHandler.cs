using UnityEngine;
using UnityEditor;
using System;

[InitializeOnLoad]
public class DropHandler 
{
    static DropHandler()
    {
        DragAndDrop.AddDropHandler(OnDropHandler);
    }

    static DragAndDropVisualMode OnDropHandler(int dragID, HierarchyDropFlags dropMode, Transform parent, bool perform)
    {
        MonoScript script = GetScript();
        if (script != null)
        {
            if (perform)
            {
                GameObject go = CreateObj(script.name);
                Component c = go.AddComponent(script.GetClass());
            }
            return DragAndDropVisualMode.Copy;
        }
        return DragAndDropVisualMode.None;

    }

    private static GameObject CreateObj(string name)
    {
        GameObject go = new GameObject(name);
        Selection.activeObject = go;
        return go;
    }

    private static MonoScript GetScript()
    {
        foreach (UnityEngine.Object dragged in DragAndDrop.objectReferences)
        {
            if (dragged is MonoScript monoScript)
            {
                return monoScript;
            }
        }
        return null;
    }
}
