﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

 /*this class is used to get the nameInput in the "Load" menu*/
public class getNameInputLoad : MonoBehaviour {
	public GameObject textfield;
	public static string name = "";
	
	void Start(){
		name = textfield.GetComponent<InputField>().text; //getting the textfield content
	}
}
