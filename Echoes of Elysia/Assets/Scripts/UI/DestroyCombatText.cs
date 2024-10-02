using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCombatText : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.75f);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
    }
}
