using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {

    // Config pengaturan gerakan
    [Header("Player Movement")]
    [SerializeField] int moveSpeed;
    [SerializeField] float padding = .5f;
    // Config pengaturan tembakan
    [Header("Projectile")]
    GameObject weapon;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.5f;
    // Visual efek player mati
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    //cek wilayah dari pergerakan dan status player
    void Start () 
	{
        SetUpMoveBoundaries();
        SetPlayerStats();
        
    }
    //cek player status senjata dan kecepatan
    private void SetPlayerStats()
    {
        SetWeapon();
        SetSpeed();
    }
    //Memperbaharui pergerakan dan tembakan
    void Update () 
	{
        Move();
        Fire();
    }
    //Mengatur tembakan dari player memencet tombol(spasi dan mouse)
    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
    //Mengatur pergerakan dari player
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal");
        var deltaY = Input.GetAxis("Vertical");
        var newXPos = Mathf.Clamp(transform.position.x + deltaX * Time.deltaTime * moveSpeed, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY * Time.deltaTime * moveSpeed, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
    //cek pergerakanwilayah
    private void SetUpMoveBoundaries()
    {
        Camera gameGamera = Camera.main;
        xMin = gameGamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameGamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameGamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameGamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
    //cek pause sesaat pada waktu menembak terus menerus
    private IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject projectile = Instantiate(weapon, transform.position, Quaternion.identity) as GameObject;

            // Mengambil arah dari tembakan
            List<float> projectileDirectionList = projectile.GetComponent<WeaponConfig>().GetProjectileDirections();

            // Mengambil posisi dari tembakan 
            List<Rigidbody2D> projectileRigidBodies = projectile.transform.GetComponentsInChildren<Rigidbody2D>().ToList();

            // Set rBodyCounter untuk iterasi melalui projectDirectionList
            int rBodyCounter = 0;

            // Untuk setiap child dari tembakan
            foreach (var rBody in projectileRigidBodies)
            {
                // Memasukkan arah tembakan berdasarkan velocity
                rBody.velocity = new Vector2(projectileDirectionList[rBodyCounter], projectileSpeed);

                // Iterasi rBodyCounter
                rBodyCounter++;
            }
            SoundManager.Instance.TriggerPlayerShotSFX();
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
    //Trigger collider2d(Damage ke player)
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }
    //Memproses dari damage yg mengenai player sampai nyawa player habis
    private void ProcessHit(DamageDealer damageDealer)
    {
        GameSession.Instance.SubtractHealth(damageDealer.GetDamage());
        damageDealer.Hit();
        if(GameSession.Instance.GetHealth() <= 0)
        {
            Die();
        }
    }
    //Mengatur pada saat player mati
    private void Die()
    {
        Destroy(gameObject);
        SoundManager.Instance.TriggerPlayerDeadSFX();
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(explosion, durationOfExplosion);
        SceneLoader.Instance.LoadGameOver();
    }
    //Mengatur senjata yg digunakan oleh player
    public void SetWeapon()
    {
        GameObject NewWeapon = GameSession.Instance.GetWeapon();
        this.weapon = NewWeapon;
        projectileFiringPeriod = weapon.GetComponent<WeaponConfig>().GetProjectileFiringPeriod();
        projectileSpeed = weapon.GetComponent<WeaponConfig>().GetProjectileSpeed();
        //Debug.Log("Player weapon set to " + weapon.gameObject.name);
    }
    //Mengatur kecepatan player
    public void SetSpeed()
    {
        moveSpeed = GameSession.Instance.GetPlayerSpeed();
        //Debug.Log("Player Speed After Update: " + moveSpeed);
    }
}
