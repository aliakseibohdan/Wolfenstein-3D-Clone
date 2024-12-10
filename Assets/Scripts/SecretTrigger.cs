using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            Application.Quit();
    }
}
