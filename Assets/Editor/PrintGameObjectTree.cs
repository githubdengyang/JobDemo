using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public class PrintGameObjectTree { 

	[MenuItem("Tools/Print Selected Hierarchy %#h")] // ��ݼ� Ctrl/Cmd+Shift+H
	static void PrintSelectedHierarchy()
	{
		if (Selection.activeTransform == null)
		{
			Debug.LogWarning("������ Hierarchy ��ѡ��һ�����塣");
			return;
		}

		StringBuilder sb = new StringBuilder();
		BuildHierarchyString(Selection.activeTransform, 0, sb);

		Debug.Log(sb.ToString());
	}


	static void BuildHierarchyString(Transform parent, int indent, StringBuilder sb)
	{
		sb.AppendLine(new string(' ', indent * 2) + "���� " + parent.name);

		foreach (Transform child in parent)
		{
			BuildHierarchyString(child, indent + 1, sb);
		}
	}
}
