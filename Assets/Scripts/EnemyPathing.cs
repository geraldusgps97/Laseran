using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Mengatur arah dan pergerakan dari musuh
public class EnemyPathing : MonoBehaviour {

    // Config waypoint / arah dari wave
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

	// Use this for initialization
	void Start () 
	{
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }
    //Mengatur wave
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    //Mengatur jalan dari wave per posisi waypoint
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
            if(gameObject.name.StartsWith("Enemy"))
            {
                Destroy(gameObject);
            }
            else
            {
                waypointIndex = 1;
            }
        }
    }
}
