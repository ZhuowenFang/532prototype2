using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    public List<Button> buttons;
    public PurchaseDiamond purchaseDiamond;

    public int amount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckButton();
    }

    void CheckButton()
    {
        amount = purchaseDiamond.amount;
        foreach(Button b in buttons)
        {
            if (int.Parse(b.gameObject.GetComponentInChildren<TextMeshProUGUI>().text) > amount)
            {
                Debug.Log(b.gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
                b.interactable = false;
            } else
            {
                b.interactable = true;
            }
        }
    }
}
