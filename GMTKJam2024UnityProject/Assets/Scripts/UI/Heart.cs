using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField] private Sprite FullHeart;

    [SerializeField] private Sprite EmptyHeart;

    [SerializeField] private Image CurrentImage;


    private void Awake()
    {
        CurrentImage = GetComponent<Image>();
    }

    public void ChangeToFullHeart()
    {
        CurrentImage.sprite = FullHeart;
    }

    public void ChangeToEmptyHeart()
    {
        CurrentImage.sprite = EmptyHeart;
    }

}
