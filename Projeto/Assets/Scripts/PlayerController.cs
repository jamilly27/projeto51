using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float maxvelocity;
    public float rayDistance;
    public LayerMask isgroundLayer;
    public float jumpForce;

    private GameControl _gameControl;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Rigidbody _rigidbody;

    private Vector2 _moverInput;
    [SerializeField] private bool _isGrounded;

    private void OnEnable()
    {
        //inicializacao de variavel 
        _gameControl = new GameControl();

        //referencias dos componentes no mesmo projeto da unity
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();

        //referencias para a camera main guardar na classe camera
        _mainCamera = Camera.main;


        //Articuando ao delegate do action triggerd do player input
        _playerInput.onActionTriggered += OnActionTiggered;


    }

    private void OnDisable()
    {
        _playerInput.onActionTriggered -= OnActionTiggered;

    }

    private void OnActionTiggered(InputAction.CallbackContext obj)
    {
        //comecando o nome do action que esta chegando com o  nome do action no moviment 
        if (obj.action.name.CompareTo(_gameControl.Gameplay.Moviment.name) == 0)
        {
            // atribuir ao moveinput do jogador com um vector2
            _moverInput = obj.ReadValue<Vector2>();

        }

        if (obj.action.name.CompareTo(_gameControl.Gameplay.Jump.name) == 0)
        {
            if(obj.performed) Jump();
        }
    }

    private void move()

    {
        //calcula o movimento do eixo da camera para movimento frente/tras
        Vector3 moveVertical = _mainCamera.transform.forward * _moverInput.y;


        //calcula o movimento no eixo da camera para movimento esquerdo/direita
        Vector3 moveHorizontal = _mainCamera.transform.right * _moverInput.x;

        //adiciona a forca no objeto pelo rigidbody
        _rigidbody.AddForce((moveVertical + moveHorizontal) * moveSpeed * Time.fixedDeltaTime);
    }
    
    private void FixedUpdate()
    {
        move();
    }
    
    private void LimiteVelocity()
    {
        //pegar a velocidade do player 
        Vector3 Velocity = _rigidbody.velocity;
    
        //checar se a velocidade esta dentro  do limite de cada eixo 
        //limitando o eixo x usando ifs, abs e sin

    
        if (Mathf.Abs(Velocity.x) > maxvelocity) Velocity.x = Mathf.Sign(Velocity.x) * maxvelocity;
    
        //-maxVelocity < velocity .z < maxVelocity 
        Velocity.z = Mathf.Clamp(value: Velocity.z, min: -maxvelocity, maxvelocity);
    
        //alterar a velocidade do player para ficar dentro dos limites 
        _rigidbody.velocity = Velocity;
    } 
    //* como  fazer do jogador pular 
    // *1 checar se o jogador esta no chao
    // *-- a - checar a colisao apartir da fisica (usando os eventos da colisao)
    // *-- a - vantagem: facil de implementar (adicionar uma funcao que ja existe no unity - OnCollision)
    // *-- a - desvantagem: nao sabemos a hora exata que o unity vqai chamar essa funcao (pode ser que o jogador
    // *toque no chao e demore alguns fraes pro jogo saber que ele esta no  chao )
    // *-- b - atraves do raycast: o ---/ bolinha vai vai atirar um raio, o raio vai bater em algum objeto e 
    // recebe o resultado dessa colisao:
    // *-- b- podemos usar layers pra definir quais objetos que o raycast deve checar a colisao 
    // *-- b - vantagens: resposta da colisao e imediata 
    // *2 - jogador precisa apartar o botao de pular 

    private void CheckGround()
    { _isGrounded = Physics.Raycast(origin: transform.position, direction: Vector3.down, rayDistance, 
        isgroundLayer);
    }

    private void Jump()
    {
        if (_isGrounded) _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
     
    private void Update()
    {
        CheckGround();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(start:transform.position, dir:Vector3.down * rayDistance, Color.red);
    }



}
   