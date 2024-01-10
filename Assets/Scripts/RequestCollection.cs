using System.Threading.Tasks;
using UnityEngine;

public class RequestCollection : MonoBehaviour
{
    [SerializeField] private ServerConnection _connection;

    public async Task<PlayerStats> UpdatePlayerStatsAsync(PlayerStats playerStats)
    {
        string responseMessage = await _connection.RequestAsync("/playerStats", "GET");

        return UpdateStats(playerStats, responseMessage);
    }

    public async Task<PlayerStats> UpgrateLevelAsync(PlayerStats playerStats)
    {
        string responseMessage = await _connection.RequestAsync("/playerStats", "POST", "UpgrateLevel");

        return UpdateStats(playerStats, responseMessage);
    }

    private PlayerStats UpdateStats(PlayerStats playerStats, string responseMessage)
    {
        PlayerStats stats = playerStats;
        if (responseMessage != "")
        {
            stats = JsonUtility.FromJson<PlayerStats>(responseMessage);
        }

        return stats;
    }
}
