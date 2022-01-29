using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {
	/*
	 * 
	 * 
	 * variables publicas del generador de niveles
	 * 
	*/

	public static LevelGenerator sharedInstance;//instancia compartida que permite tener un unico generador de niveles

	public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock> ();//lista que contiene todos los niveles creados
	public List<LevelBlock> currentLevelBlocks =new List<LevelBlock> (); //lista de bloques actuales
	public Transform levelInitialPoint;//punto inicial donde se genera el primer nivel de todos

	private bool isGeneratingInitialBlocks = false;

	void Awake(){

		sharedInstance = this;


	}



	// Use this for initialization
	void Start () {
		GenerateInitialBlocks ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateInitialBlocks (){
		isGeneratingInitialBlocks = true;
		for (int i = 0; i < 3; i++) {
			AddNewBlock ();

		}
		isGeneratingInitialBlocks = false;
	}

	public void AddNewBlock () {
		//seleccionar un bloque aleatorio dentro de los disponibles 
		int randomIndex =Random.Range(0,allTheLevelBlocks.Count);

		if (isGeneratingInitialBlocks) {
			randomIndex = 0;


		}

		LevelBlock block = (LevelBlock)Instantiate (allTheLevelBlocks [randomIndex]);
		block.transform.SetParent (this.transform, false);

		//posicion del bloque
		Vector3 blockPosition = Vector3.zero;
		if (currentLevelBlocks.Count == 0) {
			//vamos a colocar el primer elemento en pantalla
			blockPosition = levelInitialPoint.position;

		} else {
			//empalme de bloques en pantalla
			blockPosition = currentLevelBlocks [currentLevelBlocks.Count - 1].exitPoint.position;


		}
		block.transform.position = blockPosition;
		currentLevelBlocks.Add (block);

	}

	public void RemoveOldBlock () {

		LevelBlock block = currentLevelBlocks [0];
		currentLevelBlocks.Remove (block);
		Destroy (block.gameObject);
	}

	public void RemoveAllTheBlocks(){
		while (currentLevelBlocks.Count > 0) {
			RemoveOldBlock ();
		}


	}
}
