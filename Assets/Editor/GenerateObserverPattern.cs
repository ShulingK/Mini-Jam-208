using System.IO;
using UnityEditor;
using UnityEngine;

public class GenerateObserverPattern : EditorWindow
{
    private string customName = "My";

    [MenuItem("Tools/Generate Observer Pattern")]
    public static void OpenPopup()
    {
        GenerateObserverPattern window = CreateInstance<GenerateObserverPattern>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 80);
        window.ShowPopup();
    }


    private void OnGUI()
    {
        GUILayout.Label("Enter base name", EditorStyles.boldLabel);
        customName = EditorGUILayout.TextField("Name", customName);

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate"))
        {
            GenerateScripts(customName);
            Close();
        }

        if (GUILayout.Button("Cancel"))
        {
            Close();
        }
        GUILayout.EndHorizontal();
    }


    private void GenerateScripts(string baseName)
    {
        string folderPathSubject = "Assets/_Script/Observer/" + baseName + "/Subject";
        string folderPathObservers = "Assets/_Script/Observer/" + baseName + "/Observers";

        Directory.CreateDirectory(folderPathSubject);
        Directory.CreateDirectory(folderPathObservers);

        string subjectName = $"{baseName}Subject";
        string observersName = $"{baseName}Observer";

        string subjectPath = Path.Combine(folderPathSubject, $"{subjectName}.cs");
        string observersPath = Path.Combine(folderPathObservers, $"{observersName}.cs");

        File.WriteAllText(subjectPath, GenerateSubjectScript(subjectName));
        File.WriteAllText(observersPath, GenerateObserverScript(observersName));

        AssetDatabase.Refresh();
        Debug.Log($"✅ Generated {subjectName} in {folderPathSubject} and {observersName} in {folderPathObservers}");
    }

    private string GenerateSubjectScript(string subject)
    {
        return
$@"using UnityEngine;

public class {subject} : Subject
{{
    // OnNotifyObserver(); 
}}";
    }

    private string GenerateObserverScript(string observers)
    {
        return
$@"using UnityEngine;

public class {observers} : MonoBehaviour, IObserver
{{
    [SerializeField] Subject _subject;

    #region OnNotify
    
    public void OnNotify() {{ }}
    public void OnNotify(int notification) {{ }}
    public void OnNotify(float notification) {{ }}
    public void OnNotify(bool notification) {{ }}
    
    #endregion

    private void Start()
    {{
        _subject.AddObserver(this);
    }}

    private void OnDestroy()
    {{
        _subject.RemoveObserver(this);
    }}
}}";
    }

}
