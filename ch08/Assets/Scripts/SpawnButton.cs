using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    [SerializeField] SpawnController controller;

    public bool isActive = false;
    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) isActive = true;
    }

    void OnTriggerExit (Collider other) {
        if (other.CompareTag("Player")) {
            isActive = false;
            controller.spawned = false;
        }
    }
}
