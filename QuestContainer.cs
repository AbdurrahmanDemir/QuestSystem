using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Button claimButton;

    private int key;

    [Header(" Actions ")]
    public static Action<int> onRewardClaimed;

    public void Configure(string title, string rewardString, float progress, int key)
    {
        this.key = key;
        titleText.text = title;
        coinText.text = rewardString;
        progressBar.value = progress;

        CheckIfCanClaim(progress);
    }
    public void UpdateProgress(float value)
    {
        progressBar.value = value;

        CheckIfCanClaim(value); //Görev tamamlanýnca claim butonunu alýyoruz
    }

    private void CheckIfCanClaim(float progress)
    {
        if (progress >= 1)
        {
            claimButton.gameObject.SetActive(true);
            progressBar.gameObject.SetActive(false);
        }
    }

    public void Claim()
    {
        onRewardClaimed?.Invoke(key);
    }
    public int GetKey()
    {
        return key;
    }
}
