using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    
    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
