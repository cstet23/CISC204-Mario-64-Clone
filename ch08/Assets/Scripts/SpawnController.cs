using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject miniPrefab;
    [SerializeField] SpawnButton button;
    private GameObject mini;
    public bool spawned = false;

    // Update is called once per frame
    void Update()
    {
        if (button.isActive && !spawned) {
            spawned = true;
            mini = Instantiate(miniPrefab) as GameObject;
            mini.transform.position = new Vector3(0, 1, 13);
        }
    }
}
