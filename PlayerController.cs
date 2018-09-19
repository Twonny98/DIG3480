using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text scoreText;
    public Text winText;
    public PlayerController Player;

    private Rigidbody rb;
    private int count;
    private int score;
    private bool stagechange = false;

	// Use this for initialization
	void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        score = 0;
        SetCountText();
        winText.text = "";
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        float colorR = Mathf.Abs(Player.transform.position.x / 10);
        float colorG = Mathf.Abs(Player.transform.position.y / 10);
        float colorB = Mathf.Abs(Player.transform.position.z / 10);

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        Color colornow = new Vector4(colorR, colorG, colorB, 0.0f);

        Player.GetComponent<Renderer>().material.color = colornow;

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            score++;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            score--;
            SetCountText();
        }
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
        scoreText.text = "Score: " + score.ToString();
        if (count >= 22)
            winText.text = "You Finished with a Score of: " + score.ToString();
        return;
    }

    void Update()
    {
        if (stagechange != true)
            if (count >= 12)
            {
                transform.position = new Vector3(18f, .5f, 5f);
                stagechange = true;
            }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
