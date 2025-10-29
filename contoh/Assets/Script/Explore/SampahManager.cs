using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampahManager : MonoBehaviour
{
    [Header("Referensi Sungai & Sampah")]
    public GameObject[] sampahPrefabs;     // Daftar jenis sampah
    public int jumlahSampah = 10;          // Total sampah di dunia
    public float respawnDelay = 5f;        // Waktu respawn setelah diambil

    private List<GameObject> sampahPool = new List<GameObject>();
    private List<Collider2D> areaSungai = new List<Collider2D>();

    void Start()
    {
        // Cari semua area dengan tag "Sungai"
        GameObject[] sungaiObjects = GameObject.FindGameObjectsWithTag("Sungai");
        foreach (GameObject s in sungaiObjects)
        {
            Collider2D col = s.GetComponent<Collider2D>();
            if (col != null)
                areaSungai.Add(col);
        }

        if (areaSungai.Count == 0)
        {
            Debug.LogWarning("❗ Tidak ada objek bertag 'Sungai' yang punya Collider2D!");
            return;
        }

        // Buat pool sampah
        for (int i = 0; i < jumlahSampah; i++)
        {
            GameObject prefab = sampahPrefabs[Random.Range(0, sampahPrefabs.Length)];
            GameObject sampah = Instantiate(prefab);
            sampah.SetActive(false);
            sampahPool.Add(sampah);
        }

        // Spawn awal
        SpawnAllTrash();
    }

    public void SpawnAllTrash()
    {
        foreach (var trash in sampahPool)
        {
            if (!trash.activeSelf)
            {
                Vector3 spawnPos = GetRandomRiverPosition();
                trash.transform.position = spawnPos;
                trash.SetActive(true);
            }
        }
    }

    public void DespawnTrash(GameObject trash)
    {
        trash.SetActive(false);
        StartCoroutine(RespawnTrash(trash));
    }

    private IEnumerator RespawnTrash(GameObject trash)
    {
        yield return new WaitForSeconds(respawnDelay);
        Vector3 spawnPos = GetRandomRiverPosition();
        trash.transform.position = spawnPos;
        trash.SetActive(true);
    }

    // Dapatkan posisi acak di dalam collider sungai mana pun
    private Vector3 GetRandomRiverPosition()
    {
        if (areaSungai.Count == 0)
        {
            Debug.LogError("Tidak ada area sungai ditemukan!");
            return Vector3.zero;
        }

        Collider2D col = areaSungai[Random.Range(0, areaSungai.Count)];
        Bounds bounds = col.bounds;

        for (int i = 0; i < 50; i++) // coba beberapa kali cari posisi valid
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            Vector2 randomPoint = new Vector2(x, y);

            if (col.OverlapPoint(randomPoint))
            {
                return randomPoint;
            }
        }

        // fallback jika gagal cari titik di dalam collider
        return col.bounds.center;
    }
}
