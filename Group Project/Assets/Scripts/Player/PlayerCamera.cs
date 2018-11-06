using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    #region Variable
    #region Publics
    public float aimPos;
    public float zoomSpeed = .25f;
    public Transform target;//watchu lookin at?
    public bool hideCursor = true;//is the cursor hidden?
    [Header("Orbit")]
    public Vector3 offset = Vector3.zero;//where is the camera in relation to the target?
    public float
        xSpeed = 120,//how fast on the x?
        ySpeed = 120,//how fast on the y?
        yMinLimit = -20,//how far can I look up?
        yMaxLimit = 80,//how far can I look down?
        distanceMin = 0.5f,//how close can I get?
        distanceMax = 15;//how far away can I get?
    [Header("Collision")]
    public bool cameraCollision = true;//is the cameras collision turned on?
    public float camRadius = 0.3f;//what is the radius the camera can see?
    public LayerMask ignoreLayers;//what layers does the camera not see?
    #endregion
    #region Privates
    private Vector3 originalOffset;//what is the offset at the start of the game?
    private float distance;//whats the current distance?
    private float rayDistance = 1000;//how far can the ray check for collisions? 
    private float x = 0;//whats the x degrees rotation?
    private float y = 0;//whats the y degrees rotation?
    #endregion
    #endregion

    void Start()// Use this for initialization
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();//Find player
        transform.SetParent(null);//give it no parents
        if (hideCursor)//hides cursor
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        originalOffset = transform.position - target.position;//finds original offset from target
        rayDistance = originalOffset.magnitude;//set raydist to current distance magnitude of the camera
        Vector3 angles = transform.eulerAngles;//camera rotation
                                               //sets x and y degrees to current rotation
        x = angles.y;
        y = angles.x;
    }
    void Update()// Update is called once per frame
    {
        if (target)
        {
            //rotates the camera based on the mouse inputs
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            y = ClampAngle(y, yMinLimit, yMaxLimit);//clamp the angle using the "ClampAngle" function

            transform.rotation = Quaternion.Euler(y, x, 0);//rotate the transform using euler angle (y for x / x for y)
        }
        if (Input.GetMouseButton(1) && Time.timeScale == 1)
        {

            offset.z = Mathf.Lerp(offset.z, aimPos, zoomSpeed);
        }
        else
        {
            offset.z = Mathf.Lerp(offset.z, originalOffset.z, zoomSpeed);
        }
    }
    private void FixedUpdate()
    {
        if (target)//if a target has been set
        {
            if (cameraCollision)//is camera collision enabled?
            {
                //shoot ray towards camera
                Ray camRay = new Ray(target.position, -transform.forward);
                RaycastHit hit;

                if (Physics.SphereCast(camRay, camRadius, out hit, rayDistance, ~ignoreLayers, QueryTriggerInteraction.Ignore))//shoot a sphere in rays direction 
                {
                    distance = hit.distance;//set current camera distance to hit objects distance
                    return;
                }
            }
            distance = originalOffset.magnitude;//sets distance to original distance
        }

    }
    private void LateUpdate()
    {
        if (target)//if a target has been set
        {
            Vector3 localOffset = transform.TransformDirection(offset);//find our local offset for offset
            transform.position = (target.position + localOffset) + -transform.forward * distance;//repostion camera to new position based off distance and offset
        }
    }
    public static float ClampAngle(float angle, float min, float max)//givem teh CLAMPS
    {
        if (angle < -360f)
        {
            angle += 360;
        }
        if (angle > 360f)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}