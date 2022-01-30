using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string HighScoreKey = "highScore";
    public static PlayerController sharedInstance;
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int IsAlive = Animator.StringToHash("isAlive");

    public float jumpForce = 6.0f;
    public float runningSpeed = 3.0f;
    [SerializeField] private float groundDistanceToJump = 0.2f;
    public LayerMask groundLayerMask; //capa del suelo
    public Animator animator;
    public Rigidbody2D rigidBody;

    private Vector3 _startPosition;
    public bool bounce = false;

    //se cargan los componentes de objeto rigido para el conejo y demas cuestiones del nivel
    private void Awake()
    {
        animator.SetBool(IsAlive, true);
        sharedInstance = this;
        rigidBody = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

    // Update is called once per frame//se actualiza aprox 60 veces por segundo
    private void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
            if (Input.GetKeyDown("space"))
                Jump();


        animator.SetBool(IsGrounded, IsOnTheFloor());
    }

    //velocidad del conejo en el eje x constante para un intervalo equivalente de tiempo
    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState != GameState.inTheGame) return;
        if (_rigidBody.velocity.x < runningSpeed)
            _rigidBody.velocity = new Vector2(runningSpeed, _rigidBody.velocity.y);
        if (transform.position.y > 1.5f) {
            _rigidBody.AddForce(Vector2.down * jumpForce,
                ForceMode2D.Impulse); //le adiciona una fuerza de impulso al cuerpo rigido hacia abajo
        }
    }

    // Use this for initialization
    public void StartGame()
    {
        animator.SetBool(IsAlive, true); //setea si esta vivo
        transform.position = _startPosition;
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
        return Physics2D.Raycast(transform.position, Vector2.down, 
            groundDistanceToJump, groundLayerMask.value);
    }

    public void KillPlayer()
    {
        GameManager.sharedInstance.GameOver();
        animator.SetBool(IsAlive, false);

        if (PlayerPrefs.GetFloat(HighScoreKey, 0) < GetDistance()) PlayerPrefs.SetFloat(HighScoreKey, GetDistance());
    }

    public float GetDistance()
    {
        var distanceTravelled = Vector2.Distance(new Vector2(_startPosition.x, 0),
            new Vector2(transform.position.x, 0));

        return distanceTravelled;
    }
}
