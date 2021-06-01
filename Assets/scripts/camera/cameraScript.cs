using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    GameObject player;
    Transform playerTransform;
    playerScript playerScript;

    float minX;
    float maxX;
    float minY;
    float maxY;
    public float width;
    public float height;

    public Vector4 tester4;




    public struct rectBounds
    {
        public float maxX;
        public float minX;
        public float maxY;
        public float minY;

        public rectBounds(float maxX, float minX, float maxY, float minY)
        {
            this.maxX = maxX;
            this.minX = minX;
            this.maxY = maxY;
            this.minY = minY;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        playerScript = player.GetComponent<playerScript>();




    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = playerTransform.position;


        float cameraHeight = Camera.main.GetComponent<Camera>().orthographicSize * 2f;
        float cameraWidth = cameraHeight * (Screen.width / Screen.height);

        maxX = transform.position.x + cameraWidth / 2;
        minX = transform.position.x - cameraWidth / 2;
        maxY = transform.position.y + cameraHeight / 2;
        minY = transform.position.y - cameraHeight / 2;

        rectBounds screenBounds = new rectBounds(maxX, minX, maxY, minY);

        float moveMaxX = transform.position.x + 0.5f * (maxX - transform.position.x);
        float moveMinX = transform.position.x + 0.5f * (minX - transform.position.x);
        float moveMaxY = transform.position.y + 0.5f * (maxY - transform.position.y);
        float moveMinY = transform.position.y + 0.2f * (minY - transform.position.y);

        rectBounds movementBounds = new rectBounds(moveMaxX, moveMinX, moveMaxY, moveMinY);


        Vector3 positionOffset = new Vector3(0, 0, 0);

        tester4 = new Vector4(moveMaxX, moveMinX, moveMaxY, moveMinY);

        if (playerPosition.x > movementBounds.maxX)
        {
            positionOffset.x += (playerPosition.x - movementBounds.maxX);
        }

        if (playerPosition.x < movementBounds.minX)
        {
            positionOffset.x += -(movementBounds.minX - playerPosition.x);
        }

        
        if (playerPosition.y > movementBounds.maxY)
        {
            positionOffset.y = (playerPosition.y - movementBounds.maxY);
        }

        if (playerPosition.y < movementBounds.minY)
        {
            positionOffset.y += -(movementBounds.minY - playerPosition.y);
        }






        transform.position = new Vector3(transform.position.x + positionOffset.x, transform.position.y + positionOffset.y, transform.position.z);
    }
}
