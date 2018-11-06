using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBubble : MonoBehaviour
{
    public Collider collider;

    public void Start()
    {
        GetComponent<Collider>();
    }
    public void SpawnCollider()
    {
        {

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 50f);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].tag == "Enemy")
                {
                    hitColliders[i].SendMessage("Seek");
                }
                i++;
            }
        }
    }

}
