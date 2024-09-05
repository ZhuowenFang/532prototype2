using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PurchaseDiamond : MonoBehaviour
{
    public int amount;

    public void OnEnable()
    {
        amount = int.Parse(GetComponent<TextMeshProUGUI>().text);
    }

    private void OnDisable()
    {
        
    }
    public void AddDiamond(int toAdd)
    {
        amount = amount + toAdd;
        GetComponent<TextMeshProUGUI>().text = amount.ToString();
    }

    public void DecreaseDiamond(int toDecrease)
    {
        amount = amount - toDecrease;
        GetComponent<TextMeshProUGUI>().text = amount.ToString();
    }
}
