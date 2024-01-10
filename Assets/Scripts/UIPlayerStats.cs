using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private Text goldText;
    [SerializeField] private Text levelText;

    private void Update()
    {
        goldText.text = playerController.PlayerStats.Gold.ToString();
        levelText.text = playerController.PlayerStats.Level.ToString();
    }
}
