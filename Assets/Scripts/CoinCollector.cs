using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] TMP_Text blackCoinsText;
    [SerializeField] TMP_Text whiteCoinsText;

    [SerializeField] GameObject PlayerWinScreen;
    [SerializeField] GameObject ComputerWinScreen;

    static int whiteCoins = 0;
    static int blackCoins = 0;
    static bool redForPlayer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "White")
        {
            whiteCoins++;
            Striker.instance.strikeAgain = true;
            whiteCoinsText.text = "Player :\nWhite coins :\n" + whiteCoins;
            if (whiteCoins >= 6)
            {
                Striker.instance.gameObject.SetActive(false);
                PlayerWinScreen.SetActive(true);
            }
        }
        else if (collision.tag == "Black")
        {
            blackCoins++;
            Striker.instance.strikeAgain = true;
            blackCoinsText.text = "Computer :\nBlack coins :\n" + blackCoins;
            if (blackCoins >= 6)
            {
                Striker.instance.gameObject.SetActive(false);
                ComputerWinScreen.SetActive(true);
            }
        }
        else if (collision.tag == "Red")
        {
            Striker.instance.strikeAgain = true;
            redForPlayer = true;
        }
        else
            return;
        collision.gameObject.SetActive(false);
    }
}
