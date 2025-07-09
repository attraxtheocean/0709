using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < -10f)
        {
            transform.position += new Vector3(0,20f,0);
        }
    }
}
