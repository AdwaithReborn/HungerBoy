using UnityEngine;

public class BlastEffect : MonoBehaviour
{
    public float destroyTime = 0.5f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}