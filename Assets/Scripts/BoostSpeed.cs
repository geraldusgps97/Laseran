using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Mengatur power up kecepatan penjelasan hampir sama seperti power up yang lain hanya berbeda pada konfigurasinya
public class BoostSpeed : MonoBehaviour {

    private Rigidbody2D rb2d;
    private bool active = true;
    [SerializeField] private int dropSpeed = 3;
    [SerializeField] int speedValue = 1;
    [SerializeField] int scoreValue = 1000;
    [SerializeField] private GameObject collectedVFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && active == true)
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        active = false;

        Animator animator = GetComponent<Animator>();
        animator.Play("Boost Collected");
        Instantiate(collectedVFX, transform.position, Quaternion.identity);

        GameSession.Instance.AddPlayerSpeed(speedValue);
        GameSession.Instance.AddToScore(scoreValue);

        Destroy(gameObject, .5f);
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (active == true)
        {
            rb2d.velocity = Vector2.down * dropSpeed;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }
}
