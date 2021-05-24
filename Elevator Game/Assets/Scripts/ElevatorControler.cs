using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControler : MonoBehaviour
{
    public static bool isMoving = false;
    public static bool sensorIsActive = false;
    public static GameObject target;
    public GameObject door;
    public GameObject openPos;
    public AudioSource[] sounds;

    [SerializeField] private Animator doorAnim;
    [SerializeField] private static Animator doorAnimSt;
    



    private void Start()
    {

        
    }
    //private void Update()
    //{
    //    if (isMoving == true)
    //    {
    //        InvokeRepeating("PlaySounds", 1, 3f);
    //    }
    //}
    //public void PlaySounds()
    //{
    //    sounds[Random.Range(0, 4)].Play();
    //}
   
}
