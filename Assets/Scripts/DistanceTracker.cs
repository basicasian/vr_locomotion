using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    private Vector3 lastPosition;
    public float totalDistance;
    private float distanceThisFrame;

    public bool local;

    void Start()
    {
        // Initialize the last position to the starting position of the GameObject
        lastPosition = transform.position;
        totalDistance = 0f;
    }

    void Update()
    {
        // Calculate the distance moved this frame
       if(local) distanceThisFrame = Vector3.Distance(lastPosition, transform.localPosition);
        else distanceThisFrame = Vector3.Distance(lastPosition, transform.position);

        // Add the distance moved this frame to the total distance
        totalDistance += distanceThisFrame;

        // Update the last position to the current position
        lastPosition = transform.position;

        // Log the distance moved this frame and the total distance
       // Debug.Log("Distance this frame: " + distanceThisFrame + " units");
      //  Debug.Log("Total distance: " + totalDistance + " units");
    }
}
