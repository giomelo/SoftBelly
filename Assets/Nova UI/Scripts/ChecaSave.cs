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
       
       if (Savesystem.CheckIfSaveExists())
       {
           Debug.Log("SaveExit");
       } else
       {
           Savesystem.ClearSave();
            fade.StartFade();
            scene.ChangeWithDelay("Intro");
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
