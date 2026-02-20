using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ceiling;
    [SerializeField] private GameObject door;
    [SerializeField] private Stage1 stage1;

    private bool isHintPlayed = false;

    private void Start()
    {
        ceiling.SetActive(true);
        door.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!stage1.isKeyGet)
            {
                if (!isHintPlayed)
                {
                    AlertManager.Show("这里似乎是个秘密房间，不知道里面有什么……\n但是需要门禁卡才能打开。");
                    isHintPlayed = true;
                }
                return;
            }
            door.SetActive(false);
            Debug.Log("[Building] Player entered building, hiding ceiling. Collision name: " + collision.name);
            ceiling.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!stage1.isKeyGet) return;
            Debug.Log("[Building] Player exited building, showing ceiling. Collision name: " + collision.name);
            ceiling.SetActive(true);
        }
    }
}
