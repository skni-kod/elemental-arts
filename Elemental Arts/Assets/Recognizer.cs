using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using PDollarGestureRecognizer;
using System.IO;
using System.Linq;

public class Recognizer : MonoBehaviour
{
    public InputActionReference triggerAction;
    public GameObject fireballPrefab;

    XRDeviceSimulatorControls controls;
    List<Vector3> positionsList = new List<Vector3>(); //lista pozycji dla gestu

    public Transform movementSource;
    public GameObject debugCube; 
    public bool creationMode = true;
    public string gestureName = "Line";
    private List<Gesture> trainigSet = new List<Gesture>();
    private bool isMoving = false;
    private bool trigger = false;
    public float newPositionThresholdDistance = 0.05f;

    private void Awake()
    {
        controls = new XRDeviceSimulatorControls();
    }
    
    private void Start()
    {
        string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath, "*xml");
        foreach(var item in gestureFiles)
        {
            trainigSet.Add(GestureIO.ReadGestureFromFile(item));
        }

    }

    void Update()
    {
        float triggerValue = triggerAction.action.ReadValue<float>();
        if (triggerValue > 0.5f) trigger = true;
        else trigger = false;
        
        //Zacz�cie ruchu
        if (!isMoving && trigger)
        {
            StartMovement();
        }
        //Zako�czenie ruchu
        else if(isMoving && !trigger)
        {
            EndMovement();
        }
        //Aktualizacja ruchu
        else if(isMoving && trigger)
        {
            UpdateMovement();
        }
    }

    private void OnEnable()
    {
        // Code for devicesimulator
        controls.InputControls.Trigger.performed += doTrigger;
        controls.InputControls.Trigger.canceled += dontTrigger;
        controls.InputControls.Trigger.Enable();
    }

    private void dontTrigger(InputAction.CallbackContext obj)
    {
        trigger = false;
    }

    private void doTrigger(InputAction.CallbackContext obj)
    {
        trigger = true;
    }

    private void OnDisable()
    {
        controls.InputControls.Trigger.Disable();
    }

    void StartMovement()
    {
        Debug.Log("Start");
        isMoving = true;
        //usuwanie starej listy gestu i dodawanie nowej
        positionsList.Clear();
        positionsList.Add(movementSource.position);
        //cube dla wizualizacji
        if(debugCube)
            Destroy(Instantiate(debugCube, movementSource.position, Quaternion.identity), 3);
    }

    void EndMovement()
    {
        isMoving = false;
        Point[] pointArray = new Point[positionsList.Count];
        //konwersja gestu na 2D
        for(int i = 0; i < positionsList.Count; i++)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }
        Gesture gesture = new Gesture(pointArray);
        
        //Dodawanie gestu
        if(creationMode)
        {
            gesture.Name = gestureName;
            trainigSet.Add(gesture);
            string fileName = Application.persistentDataPath + "/" + gestureName + ".xml";
            GestureIO.WriteGesture(pointArray, gestureName, fileName);
        }
        //Wykrywanie gestu
        else
        {
            foreach(var item in gesture.Points)
            {
                //Debug.Log(item);
            }
            if(!gesture.Points.Contains(null))
            {
                Result result = PointCloudRecognizer.Classify(gesture, trainigSet.ToArray());
                Debug.Log(result.GestureClass + " " + result.Score);
                if (result.Score > 0.75f)
                {
                    Vector3 dir = (positionsList.First() - positionsList.Last()).normalized;
                    GameObject fireball = Instantiate(fireballPrefab, movementSource.transform.position, Quaternion.Euler(dir));
                    fireball.GetComponent<Rigidbody>().AddForce(movementSource.transform.forward * 10.0f, ForceMode.Impulse);
                }
            }
        }
    }

    void UpdateMovement()
    {
        Vector3 lastPosition = positionsList[positionsList.Count - 1];
        if(Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance)
        {
            //dodawanie �cie�ki gestu
            positionsList.Add(movementSource.position);
            //cube dla wizualizacji
            if (debugCube)
                Destroy(Instantiate(debugCube, movementSource.position, Quaternion.identity), 3);
        }
    }
}
