using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController sharedInstance;

    public float jumpForce = 6.0f; //fuerza de salto
    public float runningSpeed = 3.0f; //vector para correr en direccion x
    public LayerMask groundLayerMask; //capa del suelo
    public Animator animator;
    private readonly string highScoreKey = "highScore";
    private Rigidbody2D rigidBody;

    private Vector3 startPosition;

    //se cargan los componentes de objeto rigido para el conejo y demas cuestiones del nivel
    private void Awake()
    {
        animator.SetBool("isAlive", true);
        sharedInstance = this;
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame//se actualiza aprox 60 veces por segundo
    private void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Boton izquierdo del raton pulsado"); //comprueba que si se oprime el mouse click salta
                Jump();
            }

        animator.SetBool("isGrounded", IsOnTheFloor());
    }

    //velocidad del conejo en el eje x constante para un intervalo equivalente de tiempo
    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
            if (rigidBody.velocity.x < runningSpeed)
                rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
    }

    // Use this for initialization
    public void StartGame()
    {
        animator.SetBool("isAlive", true); //setea si esta vivo
        transform.position = startPosition;
        rigidBody.velocity = new Vector2(0, 0);
    }

    private void Jump() //permite saltar
    {
        if (IsOnTheFloor())
            rigidBody.AddForce(Vector2.up * jumpForce,
                ForceMode2D.Impulse); //le adiciona una fuerza de impulso al cuerpo rigido hacia arriba
    }

    private bool IsOnTheFloor()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 1.0f, groundLayerMask.value))
            return true; //comprueba si hay un vector de magnitud 1 desde el conejo al suelo para dejarlo saltar
        return false;
    }

    public void KillPlayer()
    {
        GameManager.sharedInstance.GameOver();
        animator.SetBool("isAlive", false);

        if (PlayerPrefs.GetFloat(highScoreKey, 0) < GetDistance()) PlayerPrefs.SetFloat(highScoreKey, GetDistance());
    }

    public float GetDistance()
    {
        var distanceTravelled = Vector2.Distance(new Vector2(startPosition.x, 0),
            new Vector2(transform.position.x, 0));

        return distanceTravelled;
    }
}