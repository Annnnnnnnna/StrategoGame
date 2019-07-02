using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public InputField mainInputField;
    public static string elem;
    public static string mode;
    public void Start()
    {
        mainInputField.onEndEdit.AddListener(delegate {  elem = mainInputField.text; });
    }
    public void LoadOnClick(int nrScene)
    {
        switch (nrScene)
        {
            case 1:
                mode = "PlayerVsPlayer";
                break;
            case 2:
                mode = "PlayerVsComputer";
                break;
            case 3:
                mode = "ComputerVsComputer";
                break;
            default: Debug.Log("Error"); break;
        }
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}