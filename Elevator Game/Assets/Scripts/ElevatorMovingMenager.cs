using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovingMenager : MonoBehaviour
{
    public GameObject elevator;
    public GameObject floorPointer;
    public GameObject door;
    float moveElevoatorSpeed = 1.5f;
    public float error_permissible = 0.1f;
    bool moveToFloor = false;
    public static bool closing, activeDoor = false, readyToMove = true;
    public AudioSource clickSound, stopSound;


    [SerializeField] private Animator clickAnim;
    [SerializeField] private Animator doorAnim;

   
   
    

    private void Start()
    {
        ElevatorControler.target = elevator;
    }


    void Update()
    {
        if(moveToFloor == true && readyToMove == true && ElevatorControler.sensorIsActive == false)
        {
            elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, ElevatorControler.target.transform.position, Time.deltaTime * moveElevoatorSpeed);
            Debug.Log("Winda powinna ruszyc");
            ElevatorControler.isMoving = true;
        }


        if ((Vector3.Distance(ElevatorControler.target.transform.position, elevator.transform.position) < error_permissible) && (moveToFloor == true))
        {
            Debug.Log("Winda powinna sie zatrzymac");
            moveToFloor = false;
            activeDoor = true;
        }

        if (activeDoor == true)
        {
            Debug.Log("Wykonuje funkcje drzwi");
            activeDoor = false;
            closing = false;
            stopSound.Play();
            DoOpenDoor();
            
        }
        if (ElevatorControler.sensorIsActive == true && closing == true)
        {
            Debug.Log("Cofam animacje");
            doorAnim.speed = 0;
            

            //Niestety nie wiem jak wprowadzic wartość ujemną dla animator.speed, zastosuję więc transform
            //Wybrałem tą metodę ponieważ odtworzenie animacji otwierania drzwi jest po prostu mniej estetyczna.
            //door.transform
            //Debug.Log("Awaryjne otwieranie drzwi!");
            //door.transform.position = Vector3.MoveTowards(door.transform.position, openPos.transform.position, Time.deltaTime * 2.0f);
            //closing = false;
            //
        }
        else if (ElevatorControler.sensorIsActive == false && closing == true)
        {
            Debug.Log("Ruch normalny animacje");
            doorAnim.speed = 1;
           
        }

    }
    private void OnMouseDown()
    {
        clickAnim.SetTrigger("Click");
        clickSound.Play();
        Debug.Log("kliknieto w guziczek");
        moveToFloor = true;
        ElevatorControler.target = floorPointer;
        
    }

    void DoOpenDoor()
    {
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        readyToMove = false;
        Debug.Log("Zaczalem czekac");
        yield return new WaitForSeconds(1);
        Debug.Log("Drzwi zaczynaja sie otwierac");
        doorAnim.SetTrigger("OpenDoor");
        Debug.Log("Drzwi otwarte");
        yield return new WaitForSeconds(10);
        closing = true;
        Debug.Log("Drzwi zamykaja sie");
        doorAnim.SetTrigger("CloseDoor");
        Debug.Log("Drzwi zamkniete");
        yield return new WaitForSeconds(6);
        Debug.Log("Winda gotowa do jazdy");
        readyToMove = true;

    }
}
