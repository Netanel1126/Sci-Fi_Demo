using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager SharedInstance { get; private set; }

    [SerializeField] private Text ammoText;
    [SerializeField] private Text indectorText;
    [SerializeField] private Image coinImage;

    private void Awake()
    {
        SharedInstance = this;
    }

    public void UpdateAmmoText(int ammo)
    {
        ammoText.text = "Ammo: " + ammo;   
    }

    public void ShouldShowCoin(bool show)
    {
        coinImage.gameObject.SetActive(show);
    }

    public void ShowIndectorWithKey(string keyString)
    {
        indectorText.gameObject.SetActive(true);
        indectorText.text = "Press: " + keyString;
    }

    public void HideIndector()
    {
        indectorText.gameObject.SetActive(false);
    }

}
