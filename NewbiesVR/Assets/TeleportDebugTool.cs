using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDebugTool : MonoBehaviour
{
    LineRenderer renderer1;
    public Transform startpoint;
    public Transform endpoint;
    private void Start()
    {
        renderer1 = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        renderer1.SetPosition(0, startpoint.position);
        renderer1.SetPosition(1, endpoint.position);
    }
}
