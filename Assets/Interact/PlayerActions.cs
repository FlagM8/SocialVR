using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro UseText;
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float MaxUseDistance = 5f;
    [SerializeField]
    private LayerMask UseLayers;
    [SerializeField]
   public Inspect inspectController;


    public void OnUse()
    {
        inspectController = GetComponent<Inspect>();
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
        {
            if (hit.collider.TryGetComponent<Door>(out Door door))
            {
                if (door.IsOpen)
                {
                    door.Close();
                }
                else
                {
                    door.Open(transform.position);
                }
            }
            else if (hit.transform.gameObject.tag == "pickable")
            {
                inspectController?.PickUpObject(hit.transform.gameObject);   // "Nevinný balíček, co se v něm asi skrývá?")
            }
        }
    }
 
    private void Update()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
        {


           if (hit.collider.TryGetComponent<Door>(out Door door))
            {
                if (door.IsOpen)
                {
                    UseText.SetText("Close \"E\"");
                }
                else
                {
                    UseText.SetText("Open \"E\"");
                }
            }else if (hit.transform.gameObject.tag == "pickable"){
                UseText.SetText("Pick-up \"E\"");
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.01f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
        }else
        {
            UseText.gameObject.SetActive(false);
        }
    }
}
