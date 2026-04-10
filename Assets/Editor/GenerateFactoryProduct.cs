using UnityEditor;
using UnityEngine;
using System.IO;

public class GenerateFactoryProduct : EditorWindow
{
    private string customName = "My";

    [MenuItem("Tools/Generate Factory Pattern")]
    public static void OpenPopup()
    {
        GenerateFactoryProduct window = CreateInstance<GenerateFactoryProduct>();
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
        string folderPathFactories = "Assets/_Script/Factories/" + baseName + "/Factory";
        string folderPathProducts = "Assets/_Script/Factories/" + baseName + "/Product";

        Directory.CreateDirectory(folderPathFactories);
        Directory.CreateDirectory(folderPathProducts);

        string factoryName = $"{baseName}Factory";
        string productName = $"{baseName}Product";

        string factoryPath = Path.Combine(folderPathFactories, $"{factoryName}.cs");
        string productPath = Path.Combine(folderPathProducts, $"{productName}.cs");

        File.WriteAllText(factoryPath, GenerateFactoryScript(factoryName, productName));
        File.WriteAllText(productPath, GenerateProductScript(productName));

        AssetDatabase.Refresh();
        Debug.Log($"✅ Generated {factoryName} in {folderPathFactories} and {productName} in {folderPathProducts}");
    }

    private string GenerateFactoryScript(string factory, string product)
    {
        return
$@"using UnityEngine;

public class {factory} : Factory
{{
    public override IProduct GetProduct()
    {{
        var product = new GameObject(""{product}"").AddComponent<{product}>();
        product.Initialize();
        return product;
    }}
}}";
    }

    private string GenerateProductScript(string product)
    {
        return
$@"using UnityEngine;

public class {product} : MonoBehaviour, IProduct
{{
    [SerializeField] private string _productName;

    public string ProductName {{ get => _productName; set => _productName = value; }}

    public void Initialize()
    {{
        ProductName = ""{product}"";
        gameObject.name = _productName;
        Debug.Log($""Initialized product: {{ProductName}}"");
    }}
}}";
    }
}
