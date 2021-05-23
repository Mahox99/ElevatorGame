using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovingMenager : MonoBehaviour
{
    public GameObject elevator;
    //public GameObject elevtorSensor;
    public GameObject floorPointer;
    float moveSpeed = 1.5f;
    public float error_permissible = 0.1f;
    bool moveToFloor = false;

 
    void Update()
    {
        if(moveToFloor == true)
        {
            elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, ElevatorControler.target.transform.position, Time.deltaTime * moveSpeed);
            Debug.Log("Winda powinna ruszyc");
            ElevatorControler.isMoving = true;
        }


        if (Vector3.Distance(ElevatorControler.target.transform.position, elevator.transform.position) < error_permissible)
        {
            Debug.Log("Winda powinna sie zatrzymac");
            moveToFloor = false;
        }
    }
    private void OnMouseDown()
    {
        Debug.Log("kliknieto w guziczek");
        moveToFloor = true;
        ElevatorControler.target = floorPointer;
    }


}
