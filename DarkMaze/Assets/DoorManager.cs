using UnityEngine;
using TMPro;

public class DoorManager : MonoBehaviour
{
    public string correctDoorName = "Door2"; // Doğru kapının ismi
    public TextMeshProUGUI messageText;

    private bool hasKey = false;

    void Start()
    {
        messageText.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
            ShowMessage("Anahtar alındı!");
        }

        if (other.name.Contains("Door"))
        {
            if (!hasKey)
            {
                ShowMessage("Anahtarın yok!");
                return;
            }

            if (other.name == correctDoorName)
            {
                ShowMessage("🎉 Tebrikler, kazandınız!");
                // İstersen burada sahne geçişi vs. yapılabilir
            }
            else
            {
                ShowMessage("❌ Bu yanlış kapı!");
            }
        }
    }

    void ShowMessage(string msg)
    {
        messageText.text = msg;
        CancelInvoke();
        Invoke("ClearMessage", 2f);
    }

    void ClearMessage()
    {
        messageText.text = "";
    }
}