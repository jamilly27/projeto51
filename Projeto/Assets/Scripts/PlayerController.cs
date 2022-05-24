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
    private Camera mainCamera;
    private Rigidbody rigidbody;

    private Vector2 _moveInput;

    private void OnEnable()
    {
        //inicializacao de variavel 

        _gameControl = new GameControl();
        
        //referencias dos componentes no mesmo projeto da unity

        _playerInput = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
        
        //referencias para a camera main guardar na classe camera
        mainCamera = Camera.main;
        
        //atribuindo o delegate do action triggerd no player input 

        _playerInput.onActionTriggered -= OnActionTriggered;
        

    }

    private void OnDisable()
    {
        _playerInput.onActionTriggered -= OnActionTiggered;
        
    }

    private void OnActionTiggered(InputAction.CallbackContext obj)
    
        //comecando o nome do action que esta chegando com o nome do action moviment 

    {
        if (obj.action.name.CompareTo(_gameControl.Gameplay.Moviment.name)== 0)
            

        {
            
            //atribuir ao moveinput o valor proveniente ao input do jogador com um vector2

            _moveInput = obj.ReadValue<Vector2>();
            
            

        }


    {
        //calcule  do movimento no eixo da camera para movimento frente/tras
        Vector3 moveVertical = mainCamera.transform.forward * _moveInput.y;
        //calcule o movimento no eixo da camera para o movimento esquerda/direita
        Vector3 MoverHorizontal =
            
       
            

        
    }
   
    
}


