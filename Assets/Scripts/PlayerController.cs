using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private RequestCollection requestCollection;
    [SerializeField] private PlayerStats _playerStats;
    public PlayerStats PlayerStats => _playerStats;

    private void Start()
    {
        StartCoroutine(UpdateStats());
    }

    public async void UpdatePlayerStats()
    {
        _playerStats = await requestCollection.UpdatePlayerStatsAsync(_playerStats);
    }

    public async void UpgrateLevel()
    {
        _playerStats = await requestCollection.UpgrateLevelAsync(_playerStats);
    }

    private IEnumerator UpdateStats()
    {

        UpdatePlayerStats();

        while (true) 
        {
            yield return new WaitForSeconds(1);
            UpdatePlayerStats();
        }
    }
}
