using UnityEditor;
using UnityEngine;

public class PrefabFixer : EditorWindow
{
    [MenuItem("Tools/Fix Missing Prefab")]
    private static void FixPrefab()
    {
        string missingGuid = "874e80b8354615e4f93425327dac194a"; // Replace this with the missing GUID
        string prefabPath = AssetDatabase.GUIDToAssetPath(missingGuid);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        if (prefab != null)
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            foreach (GameObject selectedObject in selectedObjects)
            {
                if (PrefabUtility.GetPrefabInstanceStatus(selectedObject) != PrefabInstanceStatus.NotAPrefab)
                {
                    PrefabUtility.ReplacePrefab(selectedObject, prefab, ReplacePrefabOptions.ReplaceNameBased);
                }
            }

            Debug.Log("Prefab instances fixed!");
        }
        else
        {
            Debug.LogError("Prefab with GUID " + missingGuid + " not found!");
        }
    }
}
