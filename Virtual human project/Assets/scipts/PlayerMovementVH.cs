using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementVH : MonoBehaviour
{
    
    private CharacterController character_Controller;
    private Vector3 move_Direction;
    public float speed = 5f;
    public float gravity = 9.8f;
    public Transform player;
    public Transform character;
    public float playerPosition;
    public float characterPosition;
    public float xPositionPlayer;
    public float yPositionPlayer;
    public float zPositionPlayer;
    public float xPositionCharacter;
    public float yPositionCharacter;
    public float zPositionCharacter;




    private void Awake()
    {
        
        character_Controller = GetComponent<CharacterController>();
    
    }
    void moveThePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));
        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;
        character_Controller.Move(move_Direction);

    }
    // Update is called once per frame
    void Update()
    {
        
        playerPosition = player.position.z;
        characterPosition = character.position.z;
        
        if ((characterPosition - playerPosition>1.4))
        {
            moveThePlayer();
        }
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Sitting down");
            makePlayerSit();
            

        }*/



    }
    /*public void makePlayerSit()
    {
        Debug.Log("Inside makePlayerSit method");

        zPositionCharacter = character.transform.position.z;
        zPositionPlayer = zPositionCharacter - 2f;
        //player.transform.position += new Vector3(0f, 0f, 0.5f);
        player.transform.position = new Vector3(character.transform.position.x, player.transform.position.y, zPositionPlayer);
        character.localRotation = Quaternion.Euler(0f, -180, 0f);


    }*/

}
