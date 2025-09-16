using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PrefabNodeCopier : EditorWindow
{
	GameObject oldPrefab;
	GameObject newPrefab;

	string keywordInput = "slot,collider"; // �û�������ؼ��֣�Ĭ�� slot,collider
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
		GUILayout.Label("Prefab �ڵ㸴�ƹ���", EditorStyles.boldLabel);

		oldPrefab = (GameObject)EditorGUILayout.ObjectField("�� Prefab", oldPrefab, typeof(GameObject), false);
		newPrefab = (GameObject)EditorGUILayout.ObjectField("�� Prefab", newPrefab, typeof(GameObject), false);

		GUILayout.Space(5);
		keywordInput = EditorGUILayout.TextField("�ؼ��� (���ŷָ�)", keywordInput);

		if (GUILayout.Button("ɨ���� Prefab"))
		{
			ScanOldPrefab();
		}

		if (candidateNodes.Count > 0)
		{
			GUILayout.Space(10);
			GUILayout.Label("�ɸ��ƵĽڵ�", EditorStyles.boldLabel);

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("ȫѡ")) SetAllSelections(true);
			if (GUILayout.Button("ȫ��ѡ")) SetAllSelections(false);
			GUILayout.EndHorizontal();

			scroll = GUILayout.BeginScrollView(scroll, false, true, GUILayout.Height(300));
			foreach (var node in candidateNodes)
			{
				string path = GetTransformPath(node);
				nodeSelection[node] = EditorGUILayout.ToggleLeft(path, nodeSelection[node]);
			}
			GUILayout.EndScrollView();

			GUILayout.Space(10);
			if (GUILayout.Button("���Ƶ��� Prefab"))
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
			EditorUtility.DisplayDialog("����", "����ѡ���� Prefab", "ȷ��");
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
			EditorUtility.DisplayDialog("����", "����ѡ���� Prefab", "ȷ��");
			return;
		}

		string prefabPath = AssetDatabase.GetAssetPath(newPrefab);
		GameObject newPrefabRoot = PrefabUtility.LoadPrefabContents(prefabPath);

		foreach (var kv in nodeSelection)
		{
			if (!kv.Value) continue; // �û�ûѡ
			Transform oldNode = kv.Key;

			string path = GetTransformPath(oldNode);
			string parentPath = GetParentPath(oldNode);

			Transform parentInNew = newPrefabRoot.transform.Find(parentPath);
			if (parentInNew == null)
			{
				Debug.LogError($"�� Prefab ȱ��·��: {parentPath}���޷����� {path}");
				continue;
			}

			// ���ƽڵ�
			GameObject newNode = Instantiate(oldNode.gameObject, parentInNew);
			newNode.name = oldNode.name;
			Debug.Log($"�Ѹ��ƽڵ�: {path}");
		}

		PrefabUtility.SaveAsPrefabAsset(newPrefabRoot, prefabPath);
		PrefabUtility.UnloadPrefabContents(newPrefabRoot);

		EditorUtility.DisplayDialog("���", "������ɣ�", "ȷ��");
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
