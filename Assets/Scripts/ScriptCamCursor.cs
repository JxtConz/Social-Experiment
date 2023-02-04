using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamCursor : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePos;

    public SpriteRenderer movePos;

    public Transform atkTransform;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (movePos)
        {

        }
    }
}
