using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipButton : MonoBehaviour
{
    public bool isSelected;
    public Sprite UnselectedButton;
    public Sprite selectedButton;
    public Text nameText;
    public Image Ship;
    public Text coinText;
    public GameObject AddRemoveButton;
    void Start()
    {
        if (isSelected)
        {
            this.GetComponent<Image>().sprite = selectedButton;
            AddRemoveButton.SetActive(true);
            nameText.color = Color.white;
        }
        else
        {
            this.GetComponent<Image>().sprite = UnselectedButton;
            AddRemoveButton.SetActive(false);
            nameText.color = Color.grey;
        }
        coinText.text = "x 0";
    }

    public void Active()
    {
        isSelected = true;
        this.GetComponent<Image>().sprite = selectedButton;
        AddRemoveButton.SetActive(true);
        nameText.color = Color.white;
    }

    public void Inactive()
    {
        isSelected = false;
        this.GetComponent<Image>().sprite = UnselectedButton;
        AddRemoveButton.SetActive(false);
        nameText.color = Color.grey;
    }
}
