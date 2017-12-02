using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 5f;
    public GameObject slashGraphic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Horizontal"))
        {
            transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetButton("Vertical"))
        {
            transform.Translate(Vector3.up * Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, Space.World);
        }

        Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg);

        if (Input.GetMouseButtonDown(0))
        {
            slashGraphic.SetActive(true);
            Invoke("DeactivateSlash", .2f);
        }
    }

    void DeactivateSlash()
    {
        slashGraphic.SetActive(false);
    }
}
