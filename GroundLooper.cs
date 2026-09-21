using UnityEngine;

public class GroundLooper : MonoBehaviour
{
    public Transform player;
    public Transform[] groundPieces;

    public float recycleDistance = 20f;

    void Update()
    {
        if (player == null)
            return;

        // Find the ground piece furthest behind the player
        Transform leftmost = groundPieces[0];

        foreach (Transform ground in groundPieces)
        {
            if (ground.position.x < leftmost.position.x)
            {
                leftmost = ground;
            }
        }

        // If this piece is far enough behind the player,
        // move it to the end
        if (player.position.x - leftmost.position.x > recycleDistance)
        {
            Transform rightmost = groundPieces[0];

            foreach (Transform ground in groundPieces)
            {
                if (ground.position.x > rightmost.position.x)
                {
                    rightmost = ground;
                }
            }

            float width = GetWidth(leftmost);

            leftmost.position = new Vector3(
                rightmost.position.x + GetWidth(rightmost) / 2f + width / 2f,
                leftmost.position.y,
                leftmost.position.z
            );
        }
    }

    float GetWidth(Transform ground)
    {
        BoxCollider2D collider = ground.GetComponent<BoxCollider2D>();

        return collider.bounds.size.x;
    }
}