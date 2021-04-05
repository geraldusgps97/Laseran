using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Mengatur pergerakan dari boss
public class BossPathing : MonoBehaviour {

    // Config dari wave khusus boss
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    // Cek pada saat start untuk pergerakan dari boss
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    //Mengatur wave musuh
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    //Mengatur pergerakan wave
    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            waypointIndex = 0;
        }
    }
}
