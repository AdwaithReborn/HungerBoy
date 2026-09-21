using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public Transform player;

    public SpriteRenderer[] backgrounds;

    public Sprite[] backgroundSprites;

    public float recycleDistance = 20f;

    private int nextSpriteIndex = 0;

    void Update()
    {
        if (player == null)
            return;

        // Find the background furthest to the left
        SpriteRenderer leftmost = backgrounds[0];

        foreach (SpriteRenderer bg in backgrounds)
        {
            if (bg.transform.position.x < leftmost.transform.position.x)
            {
                leftmost = bg;
            }
        }

        // If player has moved far enough right,
        // move the leftmost background to the end
        if (player.position.x - leftmost.transform.position.x > recycleDistance)
        {
            SpriteRenderer rightmost = backgrounds[0];

            foreach (SpriteRenderer bg in backgrounds)
            {
                if (bg.transform.position.x > rightmost.transform.position.x)
                {
                    rightmost = bg;
                }
            }

            // Move leftmost image to the right
            float width = leftmost.bounds.size.x;

            leftmost.transform.position = new Vector3(
                rightmost.transform.position.x + rightmost.bounds.size.x / 2 + width / 2,
                leftmost.transform.position.y,
                leftmost.transform.position.z
            );

            // Change to next background image
            if (backgroundSprites.Length > 0)
            {
                leftmost.sprite = backgroundSprites[nextSpriteIndex];

                nextSpriteIndex++;

                if (nextSpriteIndex >= backgroundSprites.Length)
                {
                    nextSpriteIndex = 0;
                }
            }
        }
    }
}