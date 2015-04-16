﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;


public class UclaExtrusion : MonoBehaviour {

	public Texture2D textureToCreateMeshFrom;
	public string gameObjectName = "Mesh Creator Object";
	public ObjectMeshType meshType = ObjectMeshType.Flat2D;
	public DrawAFigure drawing;
	public Vector3 posIni = new Vector3(0, 0, 0);
	public float size = 1;

	//Récupère la taille donnée par le Slider
	public void setSize(float s){
		size = s;
		//Debug.Log ("Size = " + s);
	}

	//Texture de test
	//private string testTexture = "Assets/textures/test.png";

	//lien pour la suite
	public GameObject placement;
	public FollowObject followObject;

	public void doTheCube(){

		//ajout pour la suite, a placer après la création de l'objet! tout en fin de la fonction, ça foire.
		/*placement.SetActive (true);
		followObject.front();
		followObject.setName (gameObjectName);
		followObject.makeTran();*/

		// create the new object and set the proper variables		
		GameObject newObject = new GameObject(gameObjectName);
		newObject.transform.position = posIni;

		//Utilisation de la texture de test
		//textureToCreateMeshFrom = (Texture2D) Resources.LoadAssetAtPath(testTexture, typeof(Texture2D));

		//Utilisation de la texture rendue par le DrawAFigure
		var bytes = drawing.getTex().EncodeToPNG();
		File.WriteAllBytes("Assets/textures/textureExtruded.png", bytes);
		textureToCreateMeshFrom = (Texture2D) Resources.LoadAssetAtPath("Assets/textures/textureExtruded.png", typeof(Texture2D));

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
		
		mcd.meshDepth = size * 0.25f;
		
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
		Debug.Log ("Objet créé");
		
		//TODO Detruire la texture créée
		//File.Delete("Assets/textures/textureExtruded.png");


		//Close();
	

	}
}
