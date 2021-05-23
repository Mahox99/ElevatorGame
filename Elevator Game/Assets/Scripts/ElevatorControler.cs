using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControler : MonoBehaviour
{
    public static bool isMoving = false;
    public static GameObject target;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private static Animator doorAnimSt;

    public static bool sensorIsActive = false, closing, activeDoor = false;

   // public GameObject PlayerDetector;

    private void Start()
    {
        doorAnimSt = doorAnim;
        
    }
    private void Update()
    {
        if(activeDoor == true)
        {
            Debug.Log("Wykonuje funkcje drzwi");
            activeDoor = false;
            closing = false;
            DoOpenDoor();
        }
        if (sensorIsActive == true && closing == true)
        {
            Debug.Log("Cofam animacje");
            doorAnim.speed = 0;
            //Niestety nie wiem jak wprowadzic wartość ujemną dla animator.speed, zastosuję więc transform
            //Wybrałem tą metodę ponieważ odtworzenie animacji otwierania drzwi jest po prostu mniej estetyczna.
        }
        else if (sensorIsActive == false && closing == true)
        {
            Debug.Log("Ruch normalny animacje");
            doorAnim.speed = 1;
        }
    }
   

    void DoOpenDoor()
    {
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        Debug.Log("Zaczalem czekac");
        yield return new WaitForSeconds(3);
        Debug.Log("Drzwi zaczynaja sie otwierac");
        doorAnim.SetTrigger("OpenDoor");
        Debug.Log("Drzwi otwarte");
        yield return new WaitForSeconds(10);
        closing = true;
        Debug.Log("Drzwi zamykaja sie");
        doorAnim.SetTrigger("CloseDoor");

        Debug.Log("Drzwi zamkniete");
        activeDoor = false;

    }
    
}
