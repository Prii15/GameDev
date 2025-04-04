using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float finishLinePos;

    // Update is called once per frame
    void Update () {
        if(0f < player.transform.position.x && player.transform.position.x < finishLinePos){
            transform.position = new Vector3(player.transform.position.x, 0, -10);
        }
        else if(player.transform.position.x < 0){
            transform.position = new Vector3(0, 0, -10);
        }
        else{
            transform.position = new Vector3(finishLinePos, 0, -10);
        }
    }
}
