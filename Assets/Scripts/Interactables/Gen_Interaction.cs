using UnityEngine;

public class Gen_Interaction : MonoBehaviour, I_Interactable
{
    [SerializeField] GM_ScriptableObject GM;
    [SerializeField] PlayerSO playerData;
    [SerializeField] int RequiredKey;
    [SerializeField] GameObject particles;

    [Header("UI Data")]
    [SerializeField] GameEvent turnOnPanel;
    [SerializeField][TextArea(3, 5)] string TextOn;
    [SerializeField][TextArea(3, 5)] string TextOff;

    void Start()
    {
        if (GM.isEnergyOn)
        {
           TurnOn();
        }
    }

    public void Interact()
    {
        CheckGas();
    }

    void CheckGas()
    {
        if (playerData.PlayerKeys[RequiredKey] == true && !GM.isEnergyOn)
        {
            TurnOn();
            GM.isEnergyOn = true;
            turnOnPanel.Raise(this, TextOn);
        }
        else if (!GM.isEnergyOn)
        {
            //Sound of Closed
            turnOnPanel.Raise(this, TextOff);
        }
    }

    void TurnOn()
    {
        particles.SetActive(false);
    }
}
