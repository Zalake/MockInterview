using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    
    public Transform player;
    public Transform character;
    public float playerPosition;
    public float characterPosition;
    [SerializeField]
    private Transform playerRoot,lookRoot;

    [SerializeField]
    private bool invert;
    [SerializeField]
    private bool can_invert = true;

    [SerializeField]
    private float sensitivity = 5f;
    [SerializeField]
    private int smoothSteps = 10;
    [SerializeField]
    private float smoothWeight = 0.4f;
    [SerializeField]
    private float rollSpeed = 3f;
    [SerializeField]
    private float rollAngle = 10f;
    [SerializeField]
    private Vector2 default_Look_limitsx = new Vector2(-70f, 80f);
    [SerializeField]
    private Vector2 default_Look_limitsy = new Vector2(-10f, 10f);
    [SerializeField]
    private Vector2 lookAngles;
    [SerializeField]
    private Vector2 current_Mouse_Look;
    [SerializeField]
    private Vector2 smoothMove;
    [SerializeField]
    private float current_Roll_Angle;
    [SerializeField]
    private int last_look_Frame;


    
    void lock_Unlock_cursor()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            

        }
        /*else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }*/
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        
            lock_Unlock_cursor();

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.None;
        }*/
        if(Cursor.lockState ==CursorLockMode.Locked)
        {
            lookAround();
        }

    }
    //lookAround
    void lookAround()
    {
        playerPosition = player.position.z;
        characterPosition = character.position.z;
        
        current_Mouse_Look = new Vector2(Input.GetAxis(lookAxis.MOUSE_Y), Input.GetAxis(lookAxis.MOUSE_X));
        lookAngles.x += current_Mouse_Look.x * sensitivity * (invert ? 1f : -1f);
        lookAngles.y += current_Mouse_Look.y * sensitivity;
        lookAngles.x = Mathf.Clamp(lookAngles.x, default_Look_limitsx.x, default_Look_limitsx.y);
        if ((characterPosition - playerPosition < 1.4))
        {
            default_Look_limitsx.x = -10f;
            default_Look_limitsx.y = +10f;

            lookAngles.x = Mathf.Clamp(lookAngles.x, default_Look_limitsx.x, default_Look_limitsx.y);
            //lookAngles.y = Mathf.Clamp(lookAngles.x, default_Look_limitsy.x, default_Look_limitsy.y);
        }
        current_Roll_Angle = Mathf.Lerp(current_Roll_Angle, Input.GetAxisRaw(lookAxis.MOUSE_X) * rollAngle, Time.deltaTime * rollSpeed);
        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, current_Roll_Angle);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);
    }
}
