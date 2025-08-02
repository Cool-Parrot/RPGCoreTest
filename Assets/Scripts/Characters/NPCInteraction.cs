using UnityEngine;
using UnityEngine.UI;            // 若之後要操作 Image 可保留
using TMPro;                     // 若要改文字內容
using UnityEngine.SceneManagement;

public class NPCInteraction : MonoBehaviour
{
    [Header("UI 物件")]
    public GameObject talkHint;      // 按 F 提示
    public GameObject dialoguePanel; // 底部對話框

    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            talkHint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            talkHint.SetActive(false);
            dialoguePanel.SetActive(false);   // 離開時自動關閉對話框
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            // 若已開啟則關閉，反之打開
            bool nowOpen = !dialoguePanel.activeSelf;
            dialoguePanel.SetActive(nowOpen);

            // 同步提示文字顯示邏輯（開框時隱藏提示）
            talkHint.SetActive(!nowOpen);
        }
    }
}
