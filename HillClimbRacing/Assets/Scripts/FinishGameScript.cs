using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameScript : MonoBehaviour
{
    [SerializeField] Canvas winCanvas;
    private void Start()
    {
        winCanvas = GameObject.Find("Win Canvas").GetComponent<Canvas>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            winCanvas.enabled = true;
        } 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
        winCanvas.enabled = false;
    }
}
