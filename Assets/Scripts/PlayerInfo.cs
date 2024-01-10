using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private new string _name;
    [SerializeField] private string _password;

    public string Name => _name;
    public string Password => _password;

    public string GetPasswordHash()
    { 
        StringBuilder sb = new StringBuilder();

        using (SHA256 sHA256 = SHA256.Create())
        {
            byte[] hashValue = sHA256.ComputeHash(Encoding.UTF8.GetBytes(_password));

            foreach (byte b in hashValue)
            {
                sb.Append($"{b:X2}");
            }

           // UnityEngine.Debug.Log(sb.ToString());

            return sb.ToString();
        }
    }
}
