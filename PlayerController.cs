using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    //This lets you edit it from the Unity editor
    public float speed;
    private Rigidbody rb;
    private Vector3 spherePosition;
    private Quaternion sphereOrientation;
    private int count;
    public Text countText;
    public Text winText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement*speed);
        spherePosition = GetComponent<Rigidbody>().position;
        sphereOrientation = GetComponent<Rigidbody>().rotation;
        Debug.Log(GetComponent<Rigidbody>().rotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

    }

    void OnGUI()
    {
        GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
        fontSize.fontSize = 24;
        GUI.Label(new Rect(20, 20, 400, 50), "Position: " + spherePosition.ToString("F2"), fontSize);
        GUI.Label(new Rect(20, 60, 600, 100), "Orientation: " + sphereOrientation.ToString("F2"), fontSize);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 11)
        {
            winText.text = "You Win!";
        }
    }
}
