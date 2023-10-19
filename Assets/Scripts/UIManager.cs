using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _coinText;

    public void UpdateCoinDisplay(int coinCount)
    {
        _coinText.text = "Coins: " + coinCount.ToString();
    }
}
