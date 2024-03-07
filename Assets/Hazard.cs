using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{

    [SerializeField] GameObject mainHazard;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Damage!");
    }

}
