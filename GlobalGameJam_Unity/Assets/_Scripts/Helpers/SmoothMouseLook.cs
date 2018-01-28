using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

/* Written by It3ration from https://answers.unity.com/answers/801283/view.html */

public class ReadOnlyInEditorAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(ReadOnlyInEditorAttribute))]
public class ReadOnlyInEditorDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property,
											GUIContent label)
	{
		return EditorGUI.GetPropertyHeight(property, label, true);
	}

	public override void OnGUI(Rect position,
							   SerializedProperty property,
							   GUIContent label)
	{
		if (Application.isEditor && Application.isPlaying) GUI.enabled = false;
		EditorGUI.PropertyField(position, property, label, true);
		GUI.enabled = true;
	}
}

/* Written by AndrewTheLeet from https://forum.unity.com/threads/simple-first-person-camera-script.417611/
   and asteins from http://wiki.unity3d.com/index.php/SmoothMouseLook */

[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class SmoothMouseLook : MonoBehaviour
{

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationX = 0F;
	float rotationY = 0F;
	private float[] rotArrayX;
	float rotAverageX = 0F;
	private float[] rotArrayY;
	float rotAverageY = 0F;
	int ringValue = 0;
	[ReadOnlyInEditor] public int frameCounter = 20;
	Quaternion originalRotation;

	void Update()
	{
		if (axes == RotationAxes.MouseXAndY)
		{
			//Resets the average rotation
			rotAverageY = 0f;
			rotAverageX = 0f;

			//Gets rotational input from the mouse
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;

			//Adds the rotation values to their relative array
			rotArrayY[ringValue] = rotationY;
			rotArrayX[ringValue] = rotationX;
			ringValue = ringValue++ % frameCounter;

			//Adding up all the rotational input values from each array
			for (int j = 0; j < rotArrayY.Length; j++)
			{
				rotAverageY += rotArrayY[j];
			}
			for (int i = 0; i < rotArrayX.Length; i++)
			{
				rotAverageX += rotArrayX[i];
			}

			//Standard maths to find the average
			rotAverageY /= rotArrayY.Length;
			rotAverageX /= rotArrayX.Length;

			//Clamp the rotation average to be within a specific value range
			rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
			rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

			//Get the rotation you will be at next as a Quaternion
			Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
			Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

			//Rotate
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		}
		else if (axes == RotationAxes.MouseX)
		{
			rotAverageX = 0f;
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotArrayX[ringValue] = rotationX;
			ringValue = ringValue++ % frameCounter;
			for (int i = 0; i < rotArrayX.Length; i++)
			{
				rotAverageX += rotArrayX[i];
			}
			rotAverageX /= rotArrayX.Length;
			rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);
			Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		}
		else
		{
			rotAverageY = 0f;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotArrayY[ringValue] = rotationY;
			ringValue = ringValue++ % frameCounter;
			for (int j = 0; j < rotArrayY.Length; j++)
			{
				rotAverageY += rotArrayY[j];
			}
			rotAverageY /= rotArrayY.Length;
			rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
			Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
			transform.localRotation = originalRotation * yQuaternion;
		}
	}
	void Start()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb)
			rb.freezeRotation = true;
		originalRotation = transform.localRotation;

		rotArrayX = new float[frameCounter];
		rotArrayY = new float[frameCounter];
	}
	public static float ClampAngle(float angle, float min, float max)
	{
		angle = angle % 360;
		if ((angle >= -360F) && (angle <= 360F))
		{
			if (angle < -360F)
			{
				angle += 360F;
			}
			if (angle > 360F)
			{
				angle -= 360F;
			}
		}
		return Mathf.Clamp(angle, min, max);
	}
	public void Reset()
	{
		Cutoff();
		transform.localRotation = originalRotation;
	}
	public void Cutoff()
	{
		for (int i = 0; i < frameCounter; i++)
		{
			rotArrayX[i] = 0f;
			rotArrayY[i] = 0f;
		}
	}
}
