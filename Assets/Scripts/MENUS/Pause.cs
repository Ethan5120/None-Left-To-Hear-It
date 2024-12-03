using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseVHS;
    public GameObject Inventory;

    public GameObject PControls, PSettings, PQuit;

    public PlayerController player; 

    public bool paused;
    public bool inventaryOpen;

    public string menuSceneName;

    public PlayerSO keyData;

    public GameObject k0, k1, k2, k3;

    private Animator animatorCanvas;

    [Header("PickUpUI")]
    [SerializeField] GameObject PickUpPanel;
    [SerializeField] TextMeshProUGUI pickedUpText;
    [SerializeField] float timeTurnedOn;
    [SerializeField] float timer;


    private void Start()
    {
       paused = false;
       inventaryOpen = false;
       animatorCanvas = GetComponent<Animator>();

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
        if(timer > 0)
        {
            PickUpPanel.SetActive(true);
            timer -= Time.deltaTime;
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
                PauseMenu.SetActive(true);             
                Time.timeScale = 0;
                Inventory.SetActive(false);
                //Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;
                AudioListener.pause = true;
                PauseVHS.SetActive(true);
            }
            if(paused == false)
            {
                player.enabled=true;
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                Inventory.SetActive(false);
                PControls.SetActive(false);
                PSettings.SetActive(false);
                PQuit.SetActive(false);
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
                AudioListener.pause = false;
                PauseVHS.SetActive(false);
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
    public void quitGamePause()
    {
        Application.Quit();
        Debug.Log("Pause Quitting...");
    }

    public void UpdateInventory()
    {/*
        k0.SetActive(keyData.PlayerKeys[0]);
        k1.SetActive(keyData.PlayerKeys[1]);
        k2.SetActive(keyData.PlayerKeys[2]);
        k3.SetActive(keyData.PlayerKeys[3]);*/
    }

    public void SetTextAndTime(Component sender, object data)
    {
        Debug.Log("Trigered");
        if(data is string)
        {
            pickedUpText.text = (string) data;
        }
        timer = timeTurnedOn;
    }
}
