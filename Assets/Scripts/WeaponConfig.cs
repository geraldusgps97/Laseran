using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConfig : MonoBehaviour {

    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.5f;
    [SerializeField] List<float> projectileDirectionList;
    private float checkForChildrenPeriod =  1f;
    private float timePassed;

    //Mengambil kecepatan tembakan
    public float GetProjectileSpeed()
    {
        return projectileSpeed;
    }

    //Mengambil waktu/periode setiap tembakan yang dilakukan
    public float GetProjectileFiringPeriod()
    {
        return projectileFiringPeriod;
    }

    //Mengambil arah tembakan
    public List<float> GetProjectileDirections()
    {
        return projectileDirectionList;
    }

    //Menghapus tembakan jika arah melebihi batas
    public void DestroyWhenProjectilesExpended()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    //Untuk mengecek waktulewat tembakan melebihi/tidak lalu didestroy
    private void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed >= checkForChildrenPeriod)
        {
            DestroyWhenProjectilesExpended();
        }
    }
}

