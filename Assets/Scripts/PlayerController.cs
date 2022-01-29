﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string HighScoreKey = "highScore";
    public static PlayerController sharedInstance;
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int IsAlive = Animator.StringToHash("isAlive");

    public float jumpForce = 6.0f; //fuerza de salto
    public float runningSpeed = 3.0f; //vector para correr en direccion x
    public LayerMask groundLayerMask; //capa del suelo
    public Animator animator;
    private Rigidbody2D _rigidBody;

    private Vector3 _startPosition;

    //se cargan los componentes de objeto rigido para el conejo y demas cuestiones del nivel
    private void Awake()
    {
        animator.SetBool(IsAlive, true);
        sharedInstance = this;
        _rigidBody = GetComponent<Rigidbody2D>();
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
    }

    // Use this for initialization
    public void StartGame()
    {
        animator.SetBool(IsAlive, true); //setea si esta vivo
        transform.position = _startPosition;
        _rigidBody.velocity = new Vector2(0, 0);
    }

    private void Jump() //permite saltar
    {
        if (IsOnTheFloor())
            _rigidBody.AddForce(Vector2.up * jumpForce,
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