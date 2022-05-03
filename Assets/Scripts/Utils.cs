using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }

	public static Vector3 WorldToScreen(Camera camera, Vector3 position)
	{
		return camera.WorldToScreenPoint(position);
	}

	public static Transform GetChildByName(this GameObject go, string name)
	{
		Transform child = null;
		foreach (Transform t in go.GetComponentsInChildren<Transform>())
		{
			if (t.name == name)
			{
				child = t;
				break;
			}
		}
		return child;
	}
}