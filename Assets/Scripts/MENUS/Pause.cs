using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Pause : MonoBehaviour
{
    [Header("Canvas Screens")]
    public GameObject PauseMenu;
    public GameObject PauseVHSEffect;
    public GameObject Inventory;
    public GameObject PControls, PSettings, PQuit;
    [Space(5)]




    [Header("Status Game Canvas")]
    public bool paused;
    public bool inventaryOpen;
    [Space(5)]




    [Header("Scenes to go on Pause Menu")]
    public SceneField menuScene;
    [Space(5)]




    [Header("Key Data")]
    public PlayerSO keyData;
    public GM_ScriptableObject GM;
    [Space(5)]




    [Header("Keys")]
    public GameObject k0;
    public GameObject k1;
    public GameObject k2;
    public GameObject k3;
    [Space(5)]




    [Header("Inventory text")]
    [SerializeField] TextMeshProUGUI AmmoText;
    [SerializeField] TextMeshProUGUI PillsText;
    [Space(5)]




    [Header("Player")]
    public PlayerController player;
    [Space(5)]




    [Header("Canvas Animator")]
    private Animator animatorCanvas;
    [Space(5)]




    [Header("Buttons")]
    [SerializeField] Button startPauseButton;
    [SerializeField] Button startInventoryButton;
    [Space(5)]




    [Header("PickUpUI")]
    [SerializeField] GameObject PickUpPanel;
    [SerializeField] TextMeshProUGUI PreText;
    [SerializeField] TextMeshProUGUI pickedUpText;
    [SerializeField] TextMeshProUGUI InfoText;
    [SerializeField] float timeItem;
    [SerializeField] float timeDoor;
    [SerializeField] float pick_timer;
    [Space(5)]



    [Header("HealthUI")]
    [SerializeField] Image hpDisplay;



    private void Start()
    {
        paused = false;
        inventaryOpen = false;
        animatorCanvas = GetComponent<Animator>();
        pick_timer = 0;
        Time.timeScale = 1;
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
        if (pick_timer > 0)
        {
            PickUpPanel.SetActive(true);
            pick_timer -= Time.deltaTime;
        }
        else
        {
            PreText.gameObject.SetActive(false);
            pickedUpText.gameObject.SetActive(false);
            InfoText.gameObject.SetActive(false);
            PickUpPanel.SetActive(false);
        }

        UpdateInventory();
        if (Time.timeScale == 0) //ESTO PERMITE LAS ANIMACIONES UNICAMENTE DEL CANVAS REPRODUCIRSE
        {
            float deltaTime = Time.unscaledDeltaTime;
            animatorCanvas.Update(deltaTime);
        }
    }

    public void ButtonPause(InputAction.CallbackContext context)
    {
        paused = !paused;
        if (inventaryOpen == true)
        {
            paused = false;
        }
        else
        {
            if (paused == true)
            {
                player.enabled = false;
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
                Inventory.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                AudioListener.pause = true;
                PauseVHSEffect.SetActive(true);
                startPauseButton.Select();
            }
            if (paused == false)
            {
                player.enabled = true;
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                Inventory.SetActive(false);
                PControls.SetActive(false);
                PSettings.SetActive(false);
                PQuit.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                AudioListener.pause = false;
                PauseVHSEffect.SetActive(false);
            }
        }
    }

    public void ButtonInventory(InputAction.CallbackContext context)
    {
        inventaryOpen = !inventaryOpen;
        if (paused == true)
        {
            inventaryOpen = false;
        }
        else
        {
            if (inventaryOpen == true)
            {
                player.enabled = false;
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                Inventory.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                PauseVHSEffect.SetActive(true);
                AudioListener.pause = false;
                startInventoryButton.Select();
            }
            if (inventaryOpen == false)
            {
                player.enabled = true;
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                Inventory.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PauseVHSEffect.SetActive(false);
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
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState= CursorLockMode.Locked;
        AudioListener.pause = false;
        PauseVHSEffect.SetActive(false);
    }
    public void backToMenu()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioListener.pause = false;
        SceneManager.LoadScene(menuScene);
        Debug.Log("Yendo A Menu");

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

    public void SetItemTextAndTime(Component sender, object data)
    {
        if (sender is playerPickUps)
        {
            if (data is string)
            {
                PreText.gameObject.SetActive(true);
                pickedUpText.gameObject.SetActive(true);
                pickedUpText.text = (string)data;
            }
            pick_timer = timeItem;
        }
        else
        {
            if (data is string)
            {
                InfoText.gameObject.SetActive(true);
                InfoText.text = (string)data;
            }
            pick_timer = timeDoor;
        }
    }

}
