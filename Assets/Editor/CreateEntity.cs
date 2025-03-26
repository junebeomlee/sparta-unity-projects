using UnityEditor;
using UnityEngine;

public static class RightClickPrefab
{
    [MenuItem("Assets/Custom/Make Blue", false, 10)]
    private static void MakeBlue()
    {
        foreach (Object obj in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);

            if (!string.IsNullOrEmpty(path) && path.EndsWith(".prefab"))
            {
                GameObject prefab = PrefabUtility.LoadPrefabContents(path);

                if (prefab != null)
                {
                    Renderer renderer = prefab.GetComponent<Renderer>();

                    if (renderer != null)
                    {
                        Undo.RecordObject(renderer.sharedMaterial, "Change Prefab Color to Blue");
                        renderer.sharedMaterial.color = Color.blue;
                        EditorUtility.SetDirty(renderer.sharedMaterial);
                        PrefabUtility.SaveAsPrefabAsset(prefab, path);
                        PrefabUtility.UnloadPrefabContents(prefab);
                        Debug.Log($"✅ {obj.name} 프리팹의 색상이 파란색으로 변경됨!");
                    }
                    else
                    {
                        Debug.LogWarning($"⚠ {obj.name}에 Renderer가 없습니다!");
                    }
                }
            }
        }
    }

    [MenuItem("Assets/Custom/Make Blue", true)]
    private static bool ValidateMakeBlue()
    {
        foreach (Object obj in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && path.EndsWith(".prefab"))
            {
                return true;
            }
        }
        return false;
    }
}