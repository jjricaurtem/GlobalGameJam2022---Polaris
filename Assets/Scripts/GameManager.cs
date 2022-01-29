using UnityEngine;
using System.Collections;


public enum GameState{//estados del juego
	menu,
	inTheGame,
	gameOver


}

public class GameManager : MonoBehaviour {
	public static GameManager sharedInstance;

	public GameState currentGameState =GameState.menu;

	public Canvas menuCanvas;
	public Canvas gameCanvas;
	public Canvas gameOverCanvas;

	public int collectedCoins = 0;



	void Awake(){
		sharedInstance = this;
	}
	void Start (){//llama el metodo start game
		currentGameState= GameState.menu;
		menuCanvas.enabled = true;
		gameCanvas.enabled = false;
		gameOverCanvas.enabled = false;
			}
	void Update (){
		if(Input.GetButtonDown("Submit")){
		 if (currentGameState != GameState.inTheGame) {
				StartGame ();
			}
		}
	}
		
	// se llama para iniciar la partida
	public void StartGame () {//para empezar la partida
		PlayerController.sharedInstance.StartGame();
		LevelGenerator.sharedInstance.GenerateInitialBlocks ();
		ChangeGameState(GameState.inTheGame);
		ViewInGame.sharedInstance.UpdateHighScoreLabel();
	}
	
	public void GameOver(){//se llama cuando el jugador muere
		ChangeGameState(GameState.gameOver);
		LevelGenerator.sharedInstance.RemoveAllTheBlocks ();
		ViewGameOver.sharedInstance.UpdateUI ();

	}
	public void BackToMainMenu(){//se llama cuando el jugador decide finalizar y volver al menu principal
		ChangeGameState(GameState.menu);
	}
	void ChangeGameState(GameState newGameState){


		if (newGameState == GameState.menu) {
			menuCanvas.enabled = true;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = false;

			//la escena nos debera mostrar el menu principal
		}else if(newGameState==GameState.inTheGame){
		//la escena de unity nos mostrara el juego en si
			menuCanvas.enabled = false;
			gameCanvas.enabled = true;
			gameOverCanvas.enabled = false;
		}
		else if (newGameState==GameState.gameOver){
			//la escena debe mostrar la pantalla de fin de la partida
			menuCanvas.enabled = false;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = true;

		}
		currentGameState = newGameState;
	}

	public void CollectCoin(){

		collectedCoins ++;
		ViewInGame.sharedInstance.UpdateCoinsLabel ();

			}


}
