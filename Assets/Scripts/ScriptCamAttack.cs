using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamAttack : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
}
