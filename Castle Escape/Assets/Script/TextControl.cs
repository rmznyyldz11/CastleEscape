using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class TextControl : MonoBehaviour
{
    [Header("StartGameUI")]
    //Duruma göre bu kodu baþka bir scripte taþýyabilirsin
    private Vector3 originalScale;
    private Vector3 scaleTo;
    [SerializeField] public GameObject DoorOrKeyTextOrBook;
    [SerializeField] public GameObject Arrow;
  //[SerializeField] Image LevelImage;
    public Transform targetOfArrowImage;
    


    void Start()
    {
        
          ArrangeOfStartGameUI();
        
    }

   
    void Update()
    {
        DeleteAllTexts();
    }

    private void DeleteAllTexts()
    {
        if (GameManager.instance.deleteText)
        {
            Arrow.gameObject.SetActive(false);
            DoorOrKeyTextOrBook.gameObject.SetActive(false);
        }
      
    }

    private void ArrangeOfStartGameUI()
    {

        Arrow.transform.DOMoveY(targetOfArrowImage.position.y, 0.9f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutSine);

        originalScale = DoorOrKeyTextOrBook.transform.localScale;
        scaleTo = originalScale * 1.2f;

        DoorOrKeyTextOrBook.transform.DOScale(scaleTo, 1.0f).SetEase(Ease.InOutSine).
        SetLoops(-1, LoopType.Yoyo);


    }
}
