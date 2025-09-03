using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public class PrintGameObjectTree { 

	[MenuItem("Tools/Print Selected Hierarchy %#h")] // 快捷键 Ctrl/Cmd+Shift+H
	static void PrintSelectedHierarchy()
	{
		if (Selection.activeTransform == null)
		{
			Debug.LogWarning("请先在 Hierarchy 里选择一个物体。");
			return;
		}

		StringBuilder sb = new StringBuilder();
		BuildHierarchyString(Selection.activeTransform, 0, sb);

		Debug.Log(sb.ToString());
	}


	static void BuildHierarchyString(Transform parent, int indent, StringBuilder sb)
	{
		sb.AppendLine(new string(' ', indent * 2) + "└─ " + parent.name);

		foreach (Transform child in parent)
		{
			BuildHierarchyString(child, indent + 1, sb);
		}
	}
}
