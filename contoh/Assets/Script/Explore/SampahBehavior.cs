using UnityEngine;

public class SampahBehavior : MonoBehaviour
{
    public float detectRange = 1.5f;
    private Transform player;
    private SampahManager sampahManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        sampahManager = FindObjectOfType<SampahManager>();
    }

    void Update()
    {
        if (player == null || sampahManager == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectRange)
        {
            sampahManager.DespawnTrash(gameObject);
        }
    }
}
