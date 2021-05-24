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
}
