using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    AudioSource soundOfPaper; 
    /*//float buttonFeedbackTimer = .3f;
    //Transform defaultTransform;
    RectTransform defaultTransform;
    float scaleFactor = 1.1f;
    [SerializeField] AudioSource paperSound;
    */
    private void Start()
    {
        soundOfPaper = GameObject.Find("SceneManager").GetComponent<Buttons>().soundOfPaper;

        //defaultTransform = RectTransform.localScale;
        //rt = transform;
    }
    private void OnMouseOver()
    {
        //soundOfPaper.Play();
        Debug.Log("hovering over button");
        /*rt.localScale = other.localScale;
        this.rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3f);// * scaleFactor);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, other.rect.height);
        //rectTransf.ForceUpdateRectTransforms(); - needed before we adjust pivot a second time?
        //rt.pivot = myPrevPivot;

        paperSound.Play();
        RectTransform.sizeDelta = new Vector2(width, height);
        transform.localScale = transform.localScale * scaleFactor;
        RectTransform.localScale = RectTransform.localScale * scaleFactor;
        //transform.SetParent(transform.root);

        //felixButton.GetComponentInChildren<TMP_Text>().color = new Color32(255, 255, 255, 255); // redundant? Delete?
        //Invoke("ColorAndScaleReset", .3f); // redundant? Delete?

        // if we wanted to use various sounds
        /*AudioSource[] paperSoundList = //GameManager.GetComponent<Sounds>().arrayOfPaperSounds;
        AudioSource randomPaperSound = paperSoundList[Random.Range(0, paperSOundList.Length)];
        randomPaperSound.Play();*/
    }

    private void OnMouseExit()
    {
        //soundOfPaper.Stop();
        Debug.Log("no longer hovering over button");
        //transform.localScale = defaultTransform.localScale;
        //transform.SetParent(defaultTransform);// this shouldnt work?!

        //transform.localScale = transform.localScale / scaleFactor;
    }

    /*void ColorAndScaleReset()
    {
        sophieButton.transform.localScale = sophieButtonDefaultScale;
        sophieButton.GetComponentInChildren<TMP_Text>().color = new Color32(127, 255, 121, 255);

        susanneButton.transform.localScale = susanneButtonDefaultScale;
        susanneButton.GetComponentInChildren<TMP_Text>().color = new Color32(127, 255, 121, 255);

        matthiasButton.transform.localScale = matthiasButtonDefaultScale;
        matthiasButton.GetComponentInChildren<TMP_Text>().color = new Color32(127, 255, 121, 255);

        janButton.transform.localScale = janButtonDefaultScale;
        janButton.GetComponentInChildren<TMP_Text>().color = new Color32(127, 255, 121, 255);

        felixButton.transform.localScale = felixButtonDefaultScale;
        felixButton.GetComponentInChildren<TMP_Text>().color = new Color32(127, 255, 121, 255);
    }*/
}
