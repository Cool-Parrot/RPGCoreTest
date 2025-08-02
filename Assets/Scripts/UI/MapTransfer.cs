using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTransfer : MonoBehaviour
{
    public string targetSceneName = "Map2"; // 要切換到的場景名稱

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 確保是玩家
        {
            Debug.Log("進入過圖區域，切換場景");
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(targetSceneName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 找到 SpawnPoint
        GameObject spawn = GameObject.FindWithTag("Respawn");
        GameObject player = GameObject.FindWithTag("Player");

        if (spawn != null && player != null)
        {
            player.transform.position = spawn.transform.position;
        }

        // 取消訂閱，避免多次觸發
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
