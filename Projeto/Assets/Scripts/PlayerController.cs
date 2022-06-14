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
    
    private GameControl _gameControl;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private Rigidbody _rigidbody;

    private Vector2 _moverInput;

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
            _moverInput= obj.ReadValue <Vector2> ();
            
        }
    }

    private void move()

    {
        //calcula o movimento do eixo da camera para movimento frente/tras
        Vector3 moveVertical = _mainCamera.transform.forward * _moverInput.y;
        
        
        //calcula o movimento no eixo da camera para movimento esquerdo/direita
        Vector3 moveHorizontal = _mainCamera.transform.right* _moverInput.x;
        
        //adiciona a forca no objeto pelo rigidbody
        _rigidbody.AddForce ((moveVertical + moveHorizontal ) *moveSpeed*Time.fixedDeltaTime);

        
    }

    private void FixedUpdate()
    {
        move ();

    }
    }


