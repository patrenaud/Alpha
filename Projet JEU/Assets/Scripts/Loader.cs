using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour 
{
	private void Start()
	{
		LevelManager.Instance.ChangeLevel("Main");
	}
}
