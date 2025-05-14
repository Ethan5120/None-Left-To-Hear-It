using UnityEngine;

public class TV_AnimationScript : MonoBehaviour
{
    public Material OffMat;
    public Material OnMat;


    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = OffMat;
    }



    public void ChangeOn()
    {
        gameObject.GetComponent<MeshRenderer>().material = OnMat;
    }

    public void ChangeOff()
    {
        gameObject.GetComponent<MeshRenderer>().material = OffMat;
    }
}
