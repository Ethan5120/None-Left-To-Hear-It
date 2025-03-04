using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Pause : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject PausaMenu, PausaControls, PausaSettings, PausaQuit, PausaVHS;
=======
    public GameObject PauseMenu;
    public GameObject PauseVHS;
    public GameObject Inventory;

    public GameObject PControls, PSettings, PQuit;

>>>>>>> develop
    public PlayerController player; 

    public bool paused;
    public bool inventaryOpen;

    public string menuSceneName;
    public string endingSceneName;

    public PlayerSO keyData;

    public GameObject k0, k1, k2, k3;
    [SerializeField] TextMeshProUGUI AmmoText;
    [SerializeField] TextMeshProUGUI PillsText;


    private Animator animatorCanvas;

    [SerializeField] Button startPauseButton;
    [SerializeField] Button startInventoryButton;

    [Header("PickUpUI")]
    [SerializeField] GameObject PickUpPanel;
    [SerializeField] TextMeshProUGUI pickedUpText;
    [SerializeField] float timeTurnedOn;
    [SerializeField] float pick_timer;

    [Header("HealthUI")]
    [SerializeField] Image hpDisplay;



    private void Start()
    {
<<<<<<< HEAD
       PausaMenu.SetActive(false);
       PausaControls.SetActive(false);
       PausaSettings.SetActive(false);
=======
        paused = false;
        inventaryOpen = false;
        animatorCanvas = GetComponent<Animator>();
        Time.timeScale = 1;
>>>>>>> develop
    }

    private void OnEnable()
    {
        player.Pause.action.performed += ButtonPause;
        player.Inventory.action.performed += ButtonInventory;

    }

    private void OnDisable()
    {
        player.Pause.action.performed -= ButtonPause;
        player.Inventory.action.performed -= ButtonInventory;
    }   

    // Update is called once per frame
    void Update()
    {
        if(pick_timer > 0)
        {
            PickUpPanel.SetActive(true);
            pick_timer -= Time.deltaTime;
        }
        else
        {
            PickUpPanel.SetActive(false);
        }

        UpdateInventory();
        if(Time.timeScale == 0) //ESTO PERMITE LAS ANIMACIONES UNICAMENTE DEL CANVAS REPRODUCIRSE
        {
            float deltaTime = Time.unscaledDeltaTime;
            animatorCanvas.Update(deltaTime);
        }
    }

    public void ButtonPause(InputAction.CallbackContext context)
    {
        paused = !paused;
        if(inventaryOpen == true)
        {
            paused  = false;
        }
        else
        {
            if(paused == true) 
            {
                player.enabled = false;  
<<<<<<< HEAD
                PausaMenu.SetActive(true);
                PausaControls.SetActive(false);
                PausaSettings.SetActive(false);
                PausaQuit.SetActive(false);
=======
                PauseMenu.SetActive(true);             
>>>>>>> develop
                Time.timeScale = 0;
                Inventory.SetActive(false);
                //Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;
                AudioListener.pause = true;
                PauseVHS.SetActive(true);
                startPauseButton.Select();
            }
            if(paused == false)
            {
                player.enabled=true;
<<<<<<< HEAD
                PausaMenu.SetActive(false);
                PausaControls.SetActive(false);
                PausaSettings.SetActive(false);
                PausaQuit.SetActive(false);
=======
                PauseMenu.SetActive(false);
>>>>>>> develop
                Time.timeScale = 1;
                Inventory.SetActive(false);
                PControls.SetActive(false);
                PSettings.SetActive(false);
                PQuit.SetActive(false);
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
<<<<<<< HEAD
                //AudioListener.pause = false;
                PausaVHS.SetActive(false);
=======
                AudioListener.pause = false;
                PauseVHS.SetActive(false);
>>>>>>> develop
            }
        }
    }

    public void ButtonInventory(InputAction.CallbackContext context)
    {
        inventaryOpen = !inventaryOpen;
        if(paused == true)
        {
            inventaryOpen  = false;
        }
        else
        {
            if(inventaryOpen == true) 
            {
                player.enabled = false;  
                PauseMenu.SetActive(false);             
                Time.timeScale = 0;
                Inventory.SetActive(true);
                //Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;
                AudioListener.pause = false;
                startInventoryButton.Select();
            }
            if(inventaryOpen == false)
            {
                player.enabled=true;
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                Inventory.SetActive(false);
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
                AudioListener.pause = false;
            }
        }
    }


    public void resumeGame()
    {
        player.enabled = true;
        PauseMenu.SetActive(false);
        Inventory.SetActive(false);
        paused = false;
        inventaryOpen = false;
        Time.timeScale=1;
        //Cursor.visible = false;
        //Cursor.lockState= CursorLockMode.Locked;
        AudioListener.pause= false;
        PauseVHS.SetActive(false);
    }
    public void backToMenu()
    {
        Time.timeScale = 1;
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
        AudioListener.pause = false;
        SceneManager.LoadScene(menuSceneName);
        Debug.Log("Yendo A Menu");

    }

    public void EndGame()
    {
        Time.timeScale = 1;
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
        AudioListener.pause = false;
        SceneManager.LoadScene(endingSceneName);
        Debug.Log("Yendo Al Final");
    }

    public void quitGamePause()
    {
        Application.Quit();
        Debug.Log("Pause Quitting...");
    }

    public void UpdateInventory()
    {
        k0.SetActive(keyData.PlayerKeys[0]);
        k1.SetActive(keyData.PlayerKeys[1]);
        k2.SetActive(keyData.PlayerKeys[2]);
        k3.SetActive(keyData.PlayerKeys[3]);
        
        AmmoText.text = $"x{keyData.playerAmmo}";
        PillsText.text = $"x{keyData.playerPills}";

        switch (keyData.playerHP)
        {
            case 1:
            {
                hpDisplay.color = Color.red;
                break;
            }
            case 2:
            {
                hpDisplay.color = Color.yellow;
                break;
            }
            case 3:
            {
                hpDisplay.color = Color.green;
                break;
            }
            default:
            {
                hpDisplay.color = Color.red;
                break;
            }
        }
    }

    public void SetTextAndTime(Component sender, object data)
    {
        Debug.Log("Trigered");
        if(data is string)
        {
            pickedUpText.text = (string) data;
        }
        pick_timer = timeTurnedOn;
    }
}
