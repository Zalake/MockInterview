using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animatorScript : MonoBehaviour
{
    public GameObject continueButton;
    public GameObject instructionPanel;
    public GameObject nextQuestion;
    public bool introduction = false;
    public int questionNumber = 0;
    [SerializeField]
    private GameObject beginButton;
    [SerializeField]
    private AudioClip[] questions;
    public GameObject recordStartButton;
    public PlayerMovementVH playerMovement;
    [SerializeField]
    public GameObject shakeHandsInstruction;
    [SerializeField]
    public GameObject sitDownInstruction;
    [SerializeField]
    private AudioClip[] audioClips;
    public AudioClip audioClip;
    public  float Volume;
    public AudioSource audio;
    public Animator anim;
    public Transform player;
    public Transform character;
    public float playerPosition;
    public float characterPosition;
    public bool standUp = false;
    public bool handShake = false;
    public bool sitDown = false;
    public bool sitDownRequest = false;
    public float characterAngle;
    public float xPositionPlayer;
    public float yPositionPlayer;
    public float zPositionPlayer;
    public float xPositionCharacter;
    public float yPositionCharacter;
    public float zPositionCharacter;
    // Start is called before the first frame update
    void Start()
    {
        
        
        nextQuestion.SetActive(false);
        beginButton.SetActive(false);
        recordStartButton.SetActive(false);
        audio = GetComponent<AudioSource>();
        audio.enabled = false;
        audio.clip = audioClips[0];
        shakeHandsInstruction.SetActive(false);
        sitDownInstruction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (questions.Length == questionNumber)
        {
            nextQuestion.SetActive(false);

        }
        anim = GetComponent<Animator>();
        playerPosition = player.position.z;
        characterPosition = character.position.z;
        if ((characterPosition - playerPosition < 2) && standUp == false)
        {

            
            anim.SetTrigger("standUp");
            shakeHandsInstruction.SetActive(true);

            //audio.enabled = true;

            //audio.PlayOneShot(audio.clip, Volume);
            xPositionPlayer = player.position.x;
            yPositionPlayer = player.position.z;
            
            characterAngle = Mathf.Rad2Deg * Mathf.Atan(xPositionPlayer / yPositionPlayer);
            Debug.Log("Angle" + characterAngle);
            character.localRotation = Quaternion.Euler(0f, -180+characterAngle , 0f);
            standUp = true;
        }

        if (standUp == true)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                shakeHandsMethod();
            }
        }

        if(handShake == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Sitting down");
            makePlayerSit();
            sitDownInstruction.SetActive(false);
            sitDown = true;
            



        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stand To Sit") == true && sitDown==false)
        {
            sitDownInstruction.SetActive(true);

        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Happy Hand Gesture (1)") == true && !audio.isPlaying && sitDownRequest == false)
        {
            audio.clip = audioClips[1];
            audio.Play();
            sitDownRequest = true;

        }
        if(audio.clip.name =="introduction"&& audio.isPlaying == false)
        {
            beginButton.SetActive(true);
        }
        
    }
    public void shakeHandsMethod()
    {
        
        anim.SetTrigger("handShake");
        audio.enabled = true;
        audio.Play();
        
        shakeHandsInstruction.SetActive(false);
        handShake = true;
    }
    

    public void makePlayerSit()
    {
        Debug.Log("Inside makePlayerSit method");
        
        zPositionCharacter = character.transform.position.z;
        zPositionPlayer = zPositionCharacter;
        Debug.Log("zPositionPalyervalur" + zPositionPlayer);
        zPositionPlayer = zPositionCharacter - 1.5f;
        Debug.Log("zPositionPalyervalur" + zPositionPlayer);
        //player.transform.position += new Vector3(0f, 0f, 0.5f);
        player.transform.position = new Vector3(character.transform.position.x,player.transform.position.y-0.3f, zPositionPlayer);
        character.localRotation = Quaternion.Euler(0f, -180, 0f);
        player.localRotation = Quaternion.Euler(0f, 0, 0f);
        if (!audio.isPlaying && sitDownRequest == true && introduction == false)
        {
            audio.clip = audioClips[2];
            audio.Play();
            
            
        }
        



    }
    public void startInterview()
    {

        askQuestions(questionNumber);
        beginButton.SetActive(false);
        recordStartButton.SetActive(true);
    }
    public void askQuestions(int number)
    {
        if (!audio.isPlaying)
        {
            audio.clip = questions[number];
            audio.Play();
            questionNumber++;
            
        }
        
        
    }
    public void activatePlayer()
    {
        
        instructionPanel.SetActive(false);
        

    }
  
}
