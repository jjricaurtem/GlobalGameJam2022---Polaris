using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public static PlayerController sharedInstance;

	public float jumpForce = 6.0f;//fuerza de salto
	public float runningSpeed =3.0f;//vector para correr en direccion x
	private Rigidbody2D rigidBody;
	public LayerMask groundLayerMask;//capa del suelo
	public Animator animator;
	private Vector3 startPosition;
	private string highScoreKey = "highScore";
	//se cargan los componentes de objeto rigido para el conejo y demas cuestiones del nivel
	void Awake (){
		animator.SetBool ("isAlive", true);
		sharedInstance = this;
		rigidBody = GetComponent<Rigidbody2D> ();
		startPosition = this.transform.position;
	}
		// Use this for initialization
	public void StartGame () {
		animator.SetBool ("isAlive", true);//setea si esta vivo
		this.transform.position = startPosition;
		rigidBody.velocity = new Vector2 (0, 0);
	}
	
	// Update is called once per frame//se actualiza aprox 60 veces por segundo
	void Update () {
		if (GameManager.sharedInstance.currentGameState == GameState.inTheGame) {
			if (Input.GetMouseButtonDown (0)) {
				Debug.Log ("Boton izquierdo del raton pulsado");//comprueba que si se oprime el mouse click salta
				Jump ();
			}
		}
		animator.SetBool ("isGrounded", IsOnTheFloor ());
							
	}
	//velocidad del conejo en el eje x constante para un intervalo equivalente de tiempo
	void FixedUpdate(){
		if (GameManager.sharedInstance.currentGameState== GameState.inTheGame){
			if (rigidBody.velocity.x < runningSpeed) {

				rigidBody.velocity	= new Vector2 (runningSpeed, rigidBody.velocity.y);
			}		}
	}
		void Jump ()//permite saltar
		{
		if (IsOnTheFloor ()) {
			rigidBody.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);//le adiciona una fuerza de impulso al cuerpo rigido hacia arriba
		}
		}
	bool IsOnTheFloor(){
		if (Physics2D.Raycast (this.transform.position, Vector2.down, 1.0f, groundLayerMask.value)) {
			return  true;//comprueba si hay un vector de magnitud 1 desde el conejo al suelo para dejarlo saltar
		}
			else{
			return false;
		}
			
	}

	public void KillPlayer (){
		GameManager.sharedInstance.GameOver ();
		animator.SetBool ("isAlive", false);

		if (PlayerPrefs.GetFloat (highScoreKey, 0) < this.GetDistance ()) {

			PlayerPrefs.SetFloat (highScoreKey, this.GetDistance ());

		}
	}

	public float GetDistance(){

		float distanceTravelled = Vector2.Distance (new Vector2 (startPosition.x, 0),
			                          new Vector2 (this.transform.position.x, 0));

		return distanceTravelled;


	}


}
