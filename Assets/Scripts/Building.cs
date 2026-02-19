using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ceiling;

    private void Start()
    {
        ceiling.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("[Building] Player entered building, hiding ceiling. Collision name: " + collision.name);
            ceiling.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("[Building] Player exited building, showing ceiling. Collision name: " + collision.name);
            ceiling.SetActive(true);
        }
    }
}
