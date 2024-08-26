using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject offersPanel;
    [SerializeField] GameObject skinsPanel;
    [SerializeField] GameObject costumesPanel;
    [SerializeField] GameObject utilityPanel;
    public void Offers()
    {
        offersPanel.SetActive(true);
        skinsPanel.SetActive(false);
        costumesPanel.SetActive(false);
        utilityPanel.SetActive(false);
    }
    public void Skins()
    {
        skinsPanel.SetActive(true);
        offersPanel.SetActive(false);
        costumesPanel.SetActive(false);
        utilityPanel.SetActive(false);
    }
    public void Costumes()
    {
        costumesPanel.SetActive(true);
        offersPanel.SetActive(false);
        skinsPanel.SetActive(false);
        utilityPanel.SetActive(false);
    }
    public void Utility()
    {
        utilityPanel.SetActive(true);
        offersPanel.SetActive(false);
        skinsPanel.SetActive(false);
        costumesPanel.SetActive(false);
    }
}
