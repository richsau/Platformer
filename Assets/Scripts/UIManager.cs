﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _coinText, _livesText;

    public void UpdateCoinDisplay(int coins)
    {
        _coinText.text = "Coins: " + coins.ToString();
    }

    public void UpdateLivesDisplay(int lives)
    {
        _livesText.text = "Lives: " + lives.ToString();
    }
   
}
