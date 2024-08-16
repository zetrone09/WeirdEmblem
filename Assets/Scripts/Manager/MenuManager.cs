using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject MenuCanvas;
    public GameObject EquipmenCanvas;
    public GameObject WeirdCanvas;
    public GameObject InventoryCanvas;
    public GameObject OptionCanvas;

    public bool isOpenMenu = false;

    CharacterControls characterControls;
    private void OnEnable()
    {
        if (characterControls == null)
        {
            characterControls = new CharacterControls();
            characterControls.PlayerMovement.OpenEquipemntMenu.performed += ctx => OpenEquipemntMenu();
            characterControls.PlayerMovement.OpenWeirdMenu.performed += ctx => OpenWeirdMenu();
            characterControls.PlayerMovement.OpenInventoryMenu.performed += ctx => OpenInventoryMenu();
            characterControls.PlayerMovement.OpenOptionMenu.performed += ctx => OpenOptionMenu();     
        }
        characterControls.Enable();
    }
    private void OnDisable()
    {
        characterControls.Disable();
    }
    void OpenEquipemntMenu()
    {
        if (!isOpenMenu) 
        {
            MenuCanvas.SetActive(true);
            EquipmenCanvas.SetActive(true);
            WeirdCanvas.SetActive(false);
            InventoryCanvas.SetActive(false);
            OptionCanvas.SetActive(false);
            isOpenMenu = true;
            return;
        }
        else 
        {
            CloseMenu();
            isOpenMenu = false;
            return;
        }
        
    }
    void OpenWeirdMenu()
    {
        if (!isOpenMenu)
        {
            MenuCanvas.SetActive(true);
            EquipmenCanvas.SetActive(false);
            WeirdCanvas.SetActive(true);
            InventoryCanvas.SetActive(false);
            OptionCanvas.SetActive(false);
            isOpenMenu = true;
            return;
        }
        else
        {
            CloseMenu();
            isOpenMenu = false;
            return;
        }
    }
    void OpenInventoryMenu()
    {
        if (!isOpenMenu)
        {
            MenuCanvas.SetActive(true);
            EquipmenCanvas.SetActive(false);
            WeirdCanvas.SetActive(false);
            InventoryCanvas.SetActive(true);
            OptionCanvas.SetActive(false);
            isOpenMenu = true;
            return;
        }
        else
        {
            CloseMenu();
            isOpenMenu = false;
            return;
        }
    }
    void OpenOptionMenu()
    {
        if (!isOpenMenu)
        {
            MenuCanvas.SetActive(true);
            EquipmenCanvas.SetActive(false);
            WeirdCanvas.SetActive(false);
            InventoryCanvas.SetActive(false);
            OptionCanvas.SetActive(true);
            isOpenMenu = true;
            return;
        }
        else
        {
            CloseMenu();
            isOpenMenu = false;
            return;
        }
    }
    void CloseMenu()
    {
        MenuCanvas.SetActive(false); 
    }

}
