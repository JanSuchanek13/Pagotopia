using UnityEngine;
using TMPro;

public class CastCowNameToUi : MonoBehaviour
{
    // THIS SCRIPT sits on all the cows/developers of the CREDITS Scene only

    // the text(TMP) windows are filled from the "Buttons"-Script on the "SceneManager" gameObject
    private TextMeshProUGUI developerName_TextField;
    private TextMeshProUGUI developerSkills_TextField;

    // find and assign neccessary variables (TMP)
    private void Start()
    {
        GameObject SceneManager = GameObject.Find("SceneManager");
        developerName_TextField = SceneManager.GetComponent<Buttons>().developerName_TextField;
        developerSkills_TextField = SceneManager.GetComponent<Buttons>().developerSkills_TextField;
        // resets textfields at game-start
        developerName_TextField.text = "";
        developerSkills_TextField.text = "";
    }

    // fills text-spaces by hovering over cows using their names
    private void OnMouseOver()
    {
        if (name == "Jan")
        {
            developerName_TextField.text = name;
            developerSkills_TextField.text = "Game Design and Code";
        }
        if (name == "Felix")
        {
            developerName_TextField.text = name;
            developerSkills_TextField.text = "Game Design, UI's and Code";
        }
        if (name == "Sophie")
        {
            developerName_TextField.text = name;
            developerSkills_TextField.text = "Art, 3D and Music";
        }
        if (name == "Paula")
        {
            developerName_TextField.text = name;
            developerSkills_TextField.text = "Art, 3D and VFX";
        }
        if (tag == "Ball")
        {
            developerName_TextField.text = "Ball";
            developerSkills_TextField.text = "very round...";
        }
        if (tag == "Ground")
        {
            developerName_TextField.text = "Gras";
            developerSkills_TextField.text = "very toothsome...";
        }
        if (tag == "Fence")
        {
            developerName_TextField.text = "Fence";
            developerSkills_TextField.text = "not very toothsome...";
        }
        // these don't work, no clue why (all in UI)
        /*
        if (tag == "Button")
        {
            developerName_TextField.text = "Button";
            developerSkills_TextField.text = "very pressable...";
        }
        if (tag == "Title")
        {
            developerName_TextField.text = "Title";
            developerSkills_TextField.text = "learn about the devs...";
        }
        if (tag == "BackGround_UI")
        {
            developerName_TextField.text = "UI";
            developerSkills_TextField.text = "very pleasant backdrop...";
        }
        if (tag == "Skill_UI")
        {
            developerName_TextField.text = "Infoboard";
            developerSkills_TextField.text = "very informative...";
        }*/
    }

    // resets text-spaces
    private void OnMouseExit()
        {
            developerName_TextField.text = "";
            developerSkills_TextField.text = "";
        }
}
