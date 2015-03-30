﻿using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.IO;


public class UclaExtrusion : MonoBehaviour {

	public Texture2D textureToCreateMeshFrom;
	public string gameObjectName = "Mesh Creator Object";
	public ObjectMeshType meshType = ObjectMeshType.Flat2D;
	public DrawAFigure drawing;
	public Vector3 posIni = new Vector3(3829, 50 ,30);
	private string testTexture = "Assets/textures/test.png";

	public void doTheCube(){
		
		// create the new object and set the proper variables		
		GameObject newObject = new GameObject(gameObjectName);
		newObject.transform.position = posIni;
		
		//textureToCreateMeshFrom = (Texture2D) Resources.LoadAssetAtPath(testTexture, typeof(Texture2D));
		//textureToCreateMeshFrom = drawing.getTex();
		var bytes = drawing.getTex().EncodeToPNG();
		File.WriteAllBytes("Assets/textures/test2.png", bytes);
		textureToCreateMeshFrom = (Texture2D) Resources.LoadAssetAtPath("Assets/textures/test2.png", typeof(Texture2D));

		MeshCreatorData mcd = newObject.AddComponent("MeshCreatorData") as MeshCreatorData;
		
		// set up mesh creator data
		mcd.outlineTexture = textureToCreateMeshFrom;
		mcd.useAutoGeneratedMaterial = true;
		
		// for height and width, maintain the image's aspect ratio
		if (textureToCreateMeshFrom.height != textureToCreateMeshFrom.width)
		{
			float height = textureToCreateMeshFrom.height;
			float width = textureToCreateMeshFrom.width;
			Debug.LogWarning("MeshCreatorWizard:: image " + textureToCreateMeshFrom.name + " has non-square size " + width + "x" + height + ", adjusting scale to match.");
			if (height > width)
			{
				mcd.meshHeight = 1.0f;
				mcd.meshWidth = width / height;
			}
			else
			{
				mcd.meshHeight = height / width;
				mcd.meshWidth = 1.0f;
			}
		}
		else
		{
			mcd.meshHeight = 1.0f;
			mcd.meshWidth = 1.0f;
		}
		
		mcd.meshDepth = 1.0f;
		
		// set up the depth options
		if (meshType == ObjectMeshType.Full3D)
		{
			mcd.uvWrapMesh = true;
			mcd.createEdges = false;
			mcd.createBacksidePlane = false;
		}
		else
		{
			mcd.uvWrapMesh = false;
			mcd.createEdges = false;
			mcd.createBacksidePlane = false;
		}
		// update the mesh
		MeshCreator.UpdateMesh(newObject);
		//Close();
	}
}