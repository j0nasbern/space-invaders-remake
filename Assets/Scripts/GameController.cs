using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject meteorGroup;
    public float meteorSpeed;
    private float lastBorderTime;
    private int direction = 1;

    public bool gameOver;
    public GameObject endScreen;
    public Text endGameText;
    public Image endGameBackground;

    // Start is called before the first frame update
    void Start()
    {
        lastBorderTime = -1.0f;
        gameOver = false;
        endScreen.SetActive(false);
        Time.timeScale = 1;
        direction = 1;
        meteorSpeed = 40;

        foreach (GameObject meteorBullet in GameObject.FindGameObjectsWithTag("MeteorBullet"))
        {
            Destroy(meteorBullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Meteor").Length == 0)
        {
            GameOver(true);
        }

        if (Input.GetButtonDown("Escape"))
        {
            SceneManager.LoadScene("Home", LoadSceneMode.Single);
        }

        Vector3 meteorPosition = meteorGroup.transform.position;
        
        if (Time.time - lastBorderTime > 0.5)
        {
            meteorPosition.x += direction * meteorSpeed * Time.deltaTime;

            if (meteorPosition.x < -35.0f || meteorPosition.x > 370.0f) MeteorSwitch();

            meteorPosition.x = Mathf.Clamp(meteorPosition.x, -35.0f, 370.0f);
        } else
        {
            meteorPosition.y -= meteorSpeed * Time.deltaTime;
        }

        meteorGroup.transform.position = meteorPosition;

        if (GameObject.FindGameObjectsWithTag("Meteor").Length == 3) meteorSpeed = 90; 
    }

    public void MeteorSwitch()
    {
        direction *= -1;
        lastBorderTime = Time.time;
    }

    public void GameOver(bool win)
    {
        Time.timeScale = 0;
        endScreen.SetActive(true);
        if (win)
        {
            endGameBackground.GetComponent<Image>().color = new Color32(0, 217, 40, 150);
            endGameText.text = "YOU WIN!";
        } else
        {
            endGameBackground.GetComponent<Image>().color = new Color32(255, 0, 0, 150);
            endGameText.text = "YOU LOSE!";
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }
}
