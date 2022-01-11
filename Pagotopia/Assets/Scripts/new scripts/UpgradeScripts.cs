using UnityEngine;

public class UpgradeScripts : MonoBehaviour
{
    #region
    [Header("Scripts to turn off:")]
    //private List<MonoBehaviour> ScriptsToTurnOff;
    //[SerializeField] MonoBehaviour ScriptsToTurnOff;
    [SerializeField] MonoBehaviour script_01_ToTurnOff;
    [SerializeField] MonoBehaviour script_02_ToTurnOff;
    [SerializeField] MonoBehaviour script_03_ToTurnOff;
    [SerializeField] GameObject zahnradMesh;
    [SerializeField] Transform carryPosition;
    private Vector3 currentPosition;
    //[SerializeField] bool gotPickedUp = false;
    #endregion

    /*private void OnMouseDown()
    {
        if(gotPickedUp)
        {
            TurnScriptsOn();
            Debug.Log("scripts turnt on");
            gotPickedUp = false;
        }else
        {
            TurnScriptsOff();
            Debug.Log("scripts turnt off");
            gotPickedUp = true;
        }

    }*/
    public void PickUpgradeUp()
    {
        script_01_ToTurnOff.enabled = false;
        script_02_ToTurnOff.enabled = false;
        script_03_ToTurnOff.enabled = false;
        currentPosition = zahnradMesh.transform.position;
        zahnradMesh.transform.position = carryPosition.position;
    }
    public void PutUpgradeBack()
    {
        zahnradMesh.transform.position = currentPosition;
        script_01_ToTurnOff.enabled = true;
        script_02_ToTurnOff.enabled = true;
        script_03_ToTurnOff.enabled = true;
    }
}
