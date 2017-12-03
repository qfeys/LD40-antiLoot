using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform playerTransform;
    Camera me;
    public Vector2 xLimit;
    public Vector2 yLimit;

    // Use this for initialization
    void Start () {
        me = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 viewPos = me.WorldToViewportPoint(playerTransform.position);
        float xSet = transform.position.x;
        float ySet = transform.position.y;
        if (viewPos.x > xLimit.y)
        {
            xSet = me.ViewportToWorldPoint(viewPos - new Vector3(xLimit.y, viewPos.y) + new Vector3(0.5f, 0.5f)).x;
        }
        else if (viewPos.x < xLimit.x)
        {
            xSet = me.ViewportToWorldPoint(viewPos - new Vector3(xLimit.x, viewPos.y) + new Vector3(0.5f, 0.5f)).x;
        }
        if (viewPos.y > yLimit.y)
        {
            ySet = me.ViewportToWorldPoint((viewPos - new Vector3(viewPos.x, yLimit.y)) + new Vector3(0.5f, 0.5f)).y;
        }
        else if (viewPos.y < yLimit.x)
        {
            ySet = me.ViewportToWorldPoint(viewPos - new Vector3(viewPos.x, yLimit.x) + new Vector3(0.5f, 0.5f)).y;
        }
        transform.position = new Vector3(xSet, ySet, -10);
    }
}
