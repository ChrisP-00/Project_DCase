#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FontReplacer : EditorWindow
{
    private TMP_FontAsset tmpFont;

    [MenuItem("Tools/Replace TMP Font In All Scenes")]
    public static void ShowWindow()
    {
        GetWindow<FontReplacer>("Font Replacer");
    }

    private void OnGUI()
    {
        GUILayout.Label("TMP 텍스트 폰트 일괄 교체 도구", EditorStyles.boldLabel);

        tmpFont = (TMP_FontAsset)EditorGUILayout.ObjectField("교체할 TMP Font", tmpFont, typeof(TMP_FontAsset), false);

        if (GUILayout.Button("모든 씬의 TMP 폰트 일괄 변경", GUILayout.Height(40)))
        {
            if (tmpFont == null)
            {
                EditorUtility.DisplayDialog("오류", "먼저 변경할 TMP 폰트를 선택해주세요.", "확인");
                return;
            }

            ReplaceTMPFontsInAllScenes();
        }
    }

    private void ReplaceTMPFontsInAllScenes()
    {
        // Assets 폴더 안의 씬만 대상으로 제한
        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets" });

        foreach (string guid in sceneGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var scene = EditorSceneManager.OpenScene(path);

            ReplaceFontsInScene();

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            Debug.Log($" TMP 폰트 교체 완료: {path}");
        }

        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("완료", "모든 씬의 TMP 텍스트 폰트가 변경되었습니다.", "확인");
    }

    private void ReplaceFontsInScene()
    {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (!go.scene.isLoaded) continue;

            var tmpText = go.GetComponent<TextMeshProUGUI>();
            if (tmpText != null && tmpFont != null)
            {
                Undo.RecordObject(tmpText, "Replace TMP Font");
                tmpText.font = tmpFont;
                EditorUtility.SetDirty(tmpText);
            }
        }
    }
}
#endif