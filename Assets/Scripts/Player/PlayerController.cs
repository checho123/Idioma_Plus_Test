using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Varibles Controller Player")]
    [SerializeField]
    private Player player;
    [SerializeField]
    private LayerMask whatIsGround;
    // CharacterController
    private CharacterController controller;
    [SerializeField]
    private Animator animPlayer;

    // Camera Player and check ground
    private Transform camPlayer, whatIsCheck;

    // float
    private float moveX, moveZ, turnSmoothVelocity, fixedSpeed;
    [SerializeField, Range(100, 300)]
    private int lifeMax;

    // bool
    public bool isMoveCharacter;
    private bool isGrounded;

    // Vectores
    private Vector3 velocity;

    // GameManager
    [SerializeField]
    private GameManager manager;

    void Start()
    {
        //Character Controller
        controller = GetComponent<CharacterController>();

        //Camera the player
        camPlayer = GameObject.FindGameObjectWithTag("MainCamera").transform;
        whatIsCheck = transform.GetChild(1);

        // Manager Controller
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        animPlayer = GetComponentInChildren<Animator>();
        fixedSpeed = player.speed;
    }

    void Update()
    {
        // Cursor Logic
        MoveCursor();
        // Attack Character
        AttackCharacter();

        DefenceCharacter();
    }

    void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        // Move Inputs
        if (!isMoveCharacter)
        {
            MoveCharacter(moveX, moveZ);
        }
        // Controller Gravity Character
        GravityCharacter();
    }

    private void MoveCursor()
    {
        // Mouse Cursor Loked or confined
        Cursor.lockState = (isMoveCharacter) ? CursorLockMode.Confined : CursorLockMode.Locked;
        
        if (manager.state == StateGame.starGame)
        {
            isMoveCharacter = false;
        }
    }

    private void MoveCharacter(float horizontal, float vertical)
    {
        // Is Controller Move
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        animPlayer.SetFloat("Speed", 0f);

        if (direction.magnitude >= 0.1f && !isMoveCharacter)
        {
            // Angle rotate Charater
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camPlayer.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, player.turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            // calculate camera angle
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            // Character Controller Move
            controller.Move(moveDir.normalized * player.speed * Time.deltaTime);
            animPlayer.SetFloat("Speed", player.speed);
        }


    }

    private void GravityCharacter()
    {
        isGrounded = Physics.CheckSphere(whatIsCheck.position, player.groundDistance, whatIsGround);
        
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        velocity.y -= player.weight * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void AttackCharacter()
    {
        animPlayer.SetBool("Attack", false);
        if (Input.GetMouseButtonDown(0))
        {
            player.speed = 0;
            animPlayer.SetBool("Attack", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            player.speed = fixedSpeed;
        }
    }

    private void DefenceCharacter()
    {
        if (Input.GetMouseButton(1))
        {
            animPlayer.SetBool("Defence", true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            animPlayer.SetBool("Defence", false);
        }
    }

    public void TakeDamage(int damage)
    {
        player.health -= damage;
        if (player.health <= 0)
        {
            DeadCharacter();
        }
    }

    public void PotionLife(int lifeUp)
    {
        if (player.health < lifeMax)
        {
            player.health += lifeUp;
        }
        else
        {
            manager.msg = true;
            manager.msgText.text = "Ya tienes la vida al Maximo";
        }
    }


    public void PotionLevelUp(int levelUp)
    {
        //Code Power Up
    }


    public void DeadCharacter()
    {

    }

}
