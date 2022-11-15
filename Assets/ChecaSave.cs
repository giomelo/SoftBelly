using _Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChecaSave : MonoBehaviour
{
    [SerializeField]
    private FadeIO fade;
    [SerializeField]
    private MenuSceneManager scene;

    public void ConfirmaNovo()
    {
        Savesystem.ClearSave();
        if (Savesystem.CheckIfSaveExists())
        {
        
        } else
        {
            fade.StartFade();
            scene.ChangeWithDelay("SC_Garden");
        }
    }

    private void Start()
    {
        if (gameObject.name == "Carregar" && !Savesystem.CheckIfSaveExists()) 
        {
            Color transp = Color.black;
            transp.a = 0.92f;
            gameObject.GetComponent<Button>().interactable = false;
            gameObject.GetComponentInChildren<Text>().color = transp;
            gameObject.GetComponent<EventTrigger>().enabled = false;
        }
    }
}
