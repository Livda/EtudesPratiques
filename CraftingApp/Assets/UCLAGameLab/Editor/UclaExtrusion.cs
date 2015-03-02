﻿using UnityEngine;
using UnityEditor;
using System.Collections;

public enum ObjectMeshType
{
	Flat2D = 0,
	Full3D = 1
}
public class UclaExtrusion : MonoBehaviour {

	public Texture2D textureToCreateMeshFrom;
	public string gameObjectName = "Mesh Creator Object";
	public ObjectMeshType meshType = ObjectMeshType.Flat2D;
	void function(){
		
		// register the Undo
		Undo.RegisterSceneUndo("Create New Mesh Object");
		
		// create the new object and set the proper variables		
		GameObject newObject = new GameObject(gameObjectName);
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
		
		/*// set up the collider options
		if (colliderType == ObjectColliderType.Boxes)
		{
			mcd.generateCollider = true;
			mcd.usePrimitiveCollider = true;
			mcd.useAABBCollider = false;
			mcd.maxNumberBoxes = 20;
			mcd.usePhysicMaterial = false;
			//mcd.addRigidBody = false;
		}*/
		/*else if (colliderType == ObjectColliderType.Mesh)
		{
			mcd.generateCollider = true;
			mcd.usePrimitiveCollider = false;
			mcd.useAABBCollider = false;
			mcd.maxNumberBoxes = 20;
			mcd.usePhysicMaterial = false;
			//mcd.addRigidBody = false;
		}
		else if (colliderType == ObjectColliderType.BoundingBox)
		{
			mcd.generateCollider = true;
			mcd.usePrimitiveCollider = false;
			mcd.useAABBCollider = true;
			mcd.maxNumberBoxes = 20;
			mcd.usePhysicMaterial = false;
			//mcd.addRigidBody = false;
		}*/
		else // default to none
		{
			mcd.generateCollider = false;
			mcd.usePrimitiveCollider = false;
			mcd.maxNumberBoxes = 20;
			mcd.usePhysicMaterial = false;
			//mcd.addRigidBody = false;
		}
		
		// update the mesh
		MeshCreator.UpdateMesh(newObject);
		Close();
	}
	}
}
