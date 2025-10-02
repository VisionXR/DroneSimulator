using UnityEngine;

public class DroneColllision : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public DroneDataSO droneData;



    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            droneData.DroneCollided();
        }
        else
        {
            droneData.DroneTouched(collision.gameObject);
        }
                
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered with " + other.gameObject.name);
        if (other.gameObject.CompareTag("Coin"))
        {
            droneData.DroneTouched(other.gameObject);
        }

    }
}
