using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PrefabNodeCopier : EditorWindow
{
	GameObject oldPrefab;
	GameObject newPrefab;

	string keywordInput = "slot,collider"; // 用户可输入关键字，默认 slot,collider
	Vector2 scroll;
	List<Transform> candidateNodes = new List<Transform>();
	Dictionary<Transform, bool> nodeSelection = new Dictionary<Transform, bool>();

	[MenuItem("Tools/Prefab Node Copier")]
	static void ShowWindow()
	{
		GetWindow<PrefabNodeCopier>("Prefab Node Copier");
	}

	void OnGUI()
	{
		GUILayout.Label("Prefab 节点复制工具", EditorStyles.boldLabel);

		oldPrefab = (GameObject)EditorGUILayout.ObjectField("老 Prefab", oldPrefab, typeof(GameObject), false);
		newPrefab = (GameObject)EditorGUILayout.ObjectField("新 Prefab", newPrefab, typeof(GameObject), false);

		GUILayout.Space(5);
		keywordInput = EditorGUILayout.TextField("关键字 (逗号分隔)", keywordInput);

		if (GUILayout.Button("扫描老 Prefab"))
		{
			ScanOldPrefab();
		}

		if (candidateNodes.Count > 0)
		{
			GUILayout.Space(10);
			GUILayout.Label("可复制的节点", EditorStyles.boldLabel);

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("全选")) SetAllSelections(true);
			if (GUILayout.Button("全不选")) SetAllSelections(false);
			GUILayout.EndHorizontal();

			scroll = GUILayout.BeginScrollView(scroll, false, true, GUILayout.Height(300));
			foreach (var node in candidateNodes)
			{
				string path = GetTransformPath(node);
				nodeSelection[node] = EditorGUILayout.ToggleLeft(path, nodeSelection[node]);
			}
			GUILayout.EndScrollView();

			GUILayout.Space(10);
			if (GUILayout.Button("复制到新 Prefab"))
			{
				CopySelectedNodes();
			}
		}
	}

	void ScanOldPrefab()
	{
		candidateNodes.Clear();
		nodeSelection.Clear();

		if (oldPrefab == null)
		{
			EditorUtility.DisplayDialog("错误", "请先选择老 Prefab", "确定");
			return;
		}

		string[] keywords = keywordInput.ToLower().Split(',');
		for (int i = 0; i < keywords.Length; i++)
		{
			keywords[i] = keywords[i].Trim();
		}

		GameObject temp = Instantiate(oldPrefab);
		foreach (Transform t in temp.GetComponentsInChildren<Transform>(true))
		{
			if (t == temp.transform) continue;

			string lowerName = t.name.ToLower();
			foreach (var keyword in keywords)
			{
				if (!string.IsNullOrEmpty(keyword) && lowerName.Contains(keyword))
				{
					candidateNodes.Add(t);
					nodeSelection[t] = true;
					break;
				}
			}
		}
	}

	void CopySelectedNodes()
	{
		if (newPrefab == null)
		{
			EditorUtility.DisplayDialog("错误", "请先选择新 Prefab", "确定");
			return;
		}

		string prefabPath = AssetDatabase.GetAssetPath(newPrefab);
		GameObject newPrefabRoot = PrefabUtility.LoadPrefabContents(prefabPath);

		foreach (var kv in nodeSelection)
		{
			if (!kv.Value) continue; // 用户没选
			Transform oldNode = kv.Key;

			string path = GetTransformPath(oldNode);
			string parentPath = GetParentPath(oldNode);

			Transform parentInNew = newPrefabRoot.transform.Find(parentPath);
			if (parentInNew == null)
			{
				Debug.LogError($"新 Prefab 缺少路径: {parentPath}，无法复制 {path}");
				continue;
			}

			// 复制节点
			GameObject newNode = Instantiate(oldNode.gameObject, parentInNew);
			newNode.name = oldNode.name;
			Debug.Log($"已复制节点: {path}");
		}

		PrefabUtility.SaveAsPrefabAsset(newPrefabRoot, prefabPath);
		PrefabUtility.UnloadPrefabContents(newPrefabRoot);

		EditorUtility.DisplayDialog("完成", "复制完成！", "确定");
	}

	string GetTransformPath(Transform t)
	{
		string path = t.name;
		while (t.parent != null && t.parent.parent != null)
		{
			t = t.parent;
			path = t.name + "/" + path;
		}
		return path;
	}

	string GetParentPath(Transform t)
	{
		if (t.parent == null) return "";
		return GetTransformPath(t.parent);
	}

	void SetAllSelections(bool value)
	{
		List<Transform> keys = new List<Transform>(nodeSelection.Keys);
		foreach (var key in keys)
		{
			nodeSelection[key] = value;
		}
	}
}
