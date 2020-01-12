using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
[System.Serializable]
public class Question
{
    public string question;
    public string answer;
}
public class AudioInputScript : MonoBehaviour
{
    public bool updateAns=false;
    public GameObject reviewPanel;
    public GameObject reviewButton;
    public Question[] questions;
    public Text QuestionText;
    public Text AnswerText;
    List<string> questionNumber = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    public Dropdown dropDown;
    string path;
    string jsonString;
    public GameObject nextQuestion;
    public int answerNumber=0;
    public bool isRecording = false;
    public GameObject recordStartButton;
    public GameObject recordStopButton;
    public string recordButtonStatus;
    [SerializeField]
    private AudioClip[] recordedAudioClips;
    public string microphone;
    private AudioSource audioSource; 

    // Start is called before the first frame update
    void Start()
    {
        createDropDown();

        path = Application.streamingAssetsPath + "/questions.json";
        jsonString = File.ReadAllText(path);
        
        
        questions = JsonHelper.FromJson<Question>(jsonString);
        
        Debug.Log(questions[0].answer);
        reviewPanel.SetActive(false);
        reviewButton.SetActive(false);
        nextQuestion.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        foreach(string device in Microphone.devices)
        {
            Debug.Log("mics" + Microphone.devices);
            if (microphone != null)
            {
                
                microphone = device;
            }
        }
        
        recordStartButton.SetActive(false);
        recordStopButton.SetActive(false);
        UpdateMicrophone();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Microphone.IsRecording(microphone))
        {
            while (!(Microphone.GetPosition(microphone) > 0))
            {

            }
            //audioSource.Play();
        }*/

    }
    void UpdateMicrophone()

    {
        audioSource.clip = Microphone.Start(microphone, true, 3599, 44100);
        //audioSource.loop = true;
        if (Microphone.IsRecording(microphone))
        {
            while (!(Microphone.GetPosition(microphone) > 0))
            {

            }
            //audioSource.Play();
        }
        

        //audioSource.Stop();
        //audioSource.clip = Microphone.Start(microphone, true, 10, 44100);
        //audioSource.loop = true;
    }

    public void recordStartButtonFuntion()
    {
        nextQuestion.SetActive(false);
        audioSource.Stop();
        UpdateMicrophone();
        Debug.Log("recording Started");
        audioSource.clip = Microphone.Start(microphone, true, 3599, 44100);
        if (Microphone.IsRecording(microphone))
        {
            while (!(Microphone.GetPosition(microphone) > 0))
            {

            }
            Debug.Log("startSpeaking");
            //audioSource.Play();
        }
        //audioSource.loop = true;


        recordStartButton.SetActive(false);
        recordStopButton.SetActive(true);
    }
    public void recordStopButtonFunction()
    {
        
        Microphone.End(microphone);
        audioSource.clip.name = answerNumber.ToString();
       
            
                
                recordedAudioClips[answerNumber] = audioSource.clip;
            

        if (questions.Length == answerNumber+1)
        {
            recordStartButton.SetActive(false);
            recordStopButton.SetActive(false);
            nextQuestion.SetActive(false);
            reviewButton.SetActive(true);
        }
        recordStartButton.SetActive(true);
        recordStopButton.SetActive(false);
        if (updateAns == false)
        {
            nextQuestion.SetActive(true);
            answerNumber += 1;
        }
        else
        {
            nextQuestion.SetActive(false);
            reviewButton.SetActive(true);
        }
        
        //audioSource.clip = recordedAudioClips[0];

        
        


    }
    public void createDropDown()
    {
        dropDown.AddOptions(questionNumber);
    }
    public void dropDownIndex(int index)
    {
        
        QuestionText.text = questions[index].question;
        AnswerText.text= questions[index].answer;
        audioSource.clip = recordedAudioClips[index];
    }
    public void playAnswers()
    {
        audioSource.Play();
    }
    public void reviewScreen()
    {
        if(updateAns == false)
        {
            reviewPanel.SetActive(true);
            dropDownIndex(0);
        }
        else
        {
            reviewPanel.SetActive(true);
            dropDownIndex(answerNumber);
        }
        
    }

    public void updateAnswer()
    {
        for (int i= 0; i < questions.Length; i++)
        {
            if(QuestionText.text== questions[i].question)
            {
                reviewPanel.SetActive(false);
                updateAns = true;
                answerNumber = i;
                Debug.Log("dshfzdjf" + recordedAudioClips[i].name);

            }
        }
        //updateAns = true;
    }
}




