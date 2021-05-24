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
            ElevatorControler.isMoving = true;
        }
        if ((Vector3.Distance(ElevatorControler.target.transform.position, elevator.transform.position) < error_permissible) && (moveToFloor == true))
        {
            moveToFloor = false;
            activeDoor = true;
        }

        if (activeDoor == true)
        {
            activeDoor = false;
            closing = false;
            stopSound.Play();
            DoOpenDoor();
            
        }
        if (ElevatorControler.sensorIsActive == true && closing == true)
        {
            doorAnim.speed = 0;
            //Niestety nie wiem jak wprowadzic wartość ujemną dla animator.speed, 
            //Drzwi zatem beda sie zatrzymowac natomiast odejscie od fotokomorki zapewni oich zamkniecie
        }
        else if (ElevatorControler.sensorIsActive == false && closing == true)
        {
            doorAnim.speed = 1;
        }

    }
    private void OnMouseDown()
    {
        if (DistanceFromObj.DistanceFromTarget < 5)
        {
            clickSound.Play();
            moveToFloor = true;
            ElevatorControler.target = floorPointer;
            clickAnim.SetTrigger("Click");
        }
    }

    void DoOpenDoor()
    {
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        readyToMove = false;
        yield return new WaitForSeconds(1);
        doorAnim.SetTrigger("OpenDoor");
        yield return new WaitForSeconds(10);
        closing = true;
        doorAnim.SetTrigger("CloseDoor");
        yield return new WaitForSeconds(6);
        readyToMove = true;
    }
}
