using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    public float parallaxEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * parallaxEffect * Input.GetAxis("Horizontal");

        if (transform.position.x < -length)
        {
            if (gameObject.CompareTag("Background")) 
            {
                transform.position = new Vector3(length, transform.position.y, transform.position.z);
            }
        }

    }
}
