using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UnitParametersData : MonoBehaviour
{
    public UnitParameters UnitParameters = new UnitParameters();

    private string path;

    private void Start()
    {

    }

//    public void RefreshShop()
//    {
//#if UNITY_ANDROID && !UNITY_EDITOR
//        path = Path.Combine(Application.persistentDataPath, "Weapons.json");
//#else
//        path = Path.Combine(Application.dataPath, "UnitParameters.json");
//#endif

//        if (!File.Exists(path))
//        {
//            for (int i = 0; i < gameData.TotalWeaponsInGame1; i++)
//            {
//                WeaponsList.Weapons.Add(new Weapons());

//                WeaponsList.Weapons[i].ID = i;
//                WeaponsList.Weapons[i].Sprite = "weapon_" + i.ToString();
//                WeaponsList.Weapons[i].Name = "Weapon " + i.ToString();
//                WeaponsList.Weapons[i].Price = i * 100;
//                WeaponsList.Weapons[i].State = WeaponButton.WeaponState.DontSelected;
//                WeaponsList.Weapons[i].Status = WeaponButton.WeaponStatus.DontPurchased;
//            }

//            Debug.Log("SIMON: IM CREATE DATA");

//            WeaponsList.Weapons[0].Status = WeaponButton.WeaponStatus.Purchased;
//            WeaponsList.Weapons[0].State = WeaponButton.WeaponState.Selected;

//            SaveWeaponsData();
//            Debug.Log("SIMON: IM SAVE DATA TO JSON");
//        }
//        else
//        {
//            WeaponsList = JsonUtility.FromJson<WeaponsList>(File.ReadAllText(path));
//            gameData.TotalWeaponsInGame1 = WeaponsList.Weapons.Count;
//            Debug.Log("SIMON: IM LOAD DATA FROM JSON");
//        }

//        for (int i = 0; i < gameData.TotalWeaponsInGame1; i++)
//        {
//            WeaponButtons.Add(Instantiate(WeaponButtonPrefab, shopContent.transform));
//            Debug.Log("SIMON: IM CREATE WEAPON BUTTONS");
//        }

//        for (int i = 0; i < WeaponButtons.Count; i++) // Сбрасываем выделения если есть
//        {
//            WeaponButtons[i].GetComponent<WeaponButton>().State = WeaponButton.WeaponState.DontSelected;
//        }

//        for (int i = 0; i < WeaponButtons.Count; i++) // Сбрасываем статусы
//        {
//            WeaponButtons[i].GetComponent<WeaponButton>().Status = WeaponButton.WeaponStatus.DontPurchased;
//        }

//        for (int i = 0; i < WeaponButtons.Count; i++)
//        {
//            WeaponButtons[i].GetComponent<WeaponButton>().State = WeaponsList.Weapons[i].State; // Назначаем значения на сцену с базы
//            WeaponButtons[i].GetComponent<WeaponButton>().Status = WeaponsList.Weapons[i].Status;  // Назначаем значения на сцену с базы
//        }

//        SetWeaponButtonPerameters();
//        CheckWeaponStateAndStatus();

//    }

//    public void SetWeaponButtonPerameters()
//    {
//        for (int i = 0; i < WeaponButtons.Count; i++)
//        {
//            WeaponButtons[i].GetComponent<WeaponButton>().name = WeaponsList.Weapons[i].Name;
//            Debug.Log("SIMON: IM ADDED NAMES!");

//            WeaponButtons[i].GetComponent<WeaponButton>().weaponImage.sprite = Resources.Load<Sprite>(WeaponsList.Weapons[i].Sprite);
//            Debug.Log("SIMON: IM ADDED SPRITE - " + Resources.Load<Sprite>(WeaponsList.Weapons[i].Sprite));

//            WeaponButtons[i].GetComponent<WeaponButton>().ID = WeaponsList.Weapons[i].ID;
//            Debug.Log("SIMON: IM ADDED ID!");

//            WeaponButtons[i].GetComponent<WeaponButton>().price = WeaponsList.Weapons[i].Price;
//            Debug.Log("SIMON: IM ADDED PRICES!");
//        }
//    }

//    public void CheckWeaponStateAndStatus()
//    {
//        for (int i = 0; i < WeaponButtons.Count; i++)
//        {
//            if (WeaponButtons[i].GetComponent<WeaponButton>().State == WeaponButton.WeaponState.Selected &&
//                WeaponButtons[i].GetComponent<WeaponButton>().Status == WeaponButton.WeaponStatus.Purchased)
//            {
//                WeaponButtons[i].GetComponent<Image>().color = weaponSelectedColor;
//                WeaponButtons[i].GetComponent<WeaponButton>().weaponPricePanel.gameObject.SetActive(false);
//            }
//            else
//            if (WeaponButtons[i].GetComponent<WeaponButton>().State == WeaponButton.WeaponState.DontSelected &&
//                WeaponButtons[i].GetComponent<WeaponButton>().Status == WeaponButton.WeaponStatus.Purchased)
//            {
//                WeaponButtons[i].GetComponent<Image>().color = weaponDontSelectedColor;
//                WeaponButtons[i].GetComponent<WeaponButton>().weaponPricePanel.gameObject.SetActive(false);
//            }
//            else
//            if (WeaponButtons[i].GetComponent<WeaponButton>().State == WeaponButton.WeaponState.DontSelected &&
//                WeaponButtons[i].GetComponent<WeaponButton>().Status == WeaponButton.WeaponStatus.DontPurchased && PlayerPrefs.GetInt(gameData.GetCollectCoins()) >= WeaponButtons[i].GetComponent<WeaponButton>().price)
//            {
//                WeaponButtons[i].GetComponent<Image>().color = weaponReadtyToPurchase;
//                WeaponButtons[i].GetComponent<WeaponButton>().weaponPricePanel.gameObject.SetActive(true);
//            }
//            else
//            {
//                WeaponButtons[i].GetComponent<Image>().color = weaponDontPurchasedColor;
//                WeaponButtons[i].GetComponent<WeaponButton>().weaponPricePanel.gameObject.SetActive(true);
//            }
//        }

//        SaveWeaponsData();
//        Debug.Log("SIMON: IM SAVE DATA TO JSON");
//    }

//    public void WeaponSelected(int weaponID)
//    {
//        sound_Manager.PlayButtonSound();

//        if (WeaponButtons[weaponID].GetComponent<WeaponButton>().Status == WeaponButton.WeaponStatus.Purchased)
//        {
//            for (int i = 0; i < WeaponButtons.Count; i++)
//            {
//                WeaponButtons[i].GetComponent<WeaponButton>().State = WeaponButton.WeaponState.DontSelected;
//                WeaponsList.Weapons[i].State = WeaponButton.WeaponState.DontSelected; // SAVE
//            }

//            WeaponButtons[weaponID].GetComponent<WeaponButton>().State = WeaponButton.WeaponState.Selected;
//            WeaponsList.Weapons[weaponID].State = WeaponButton.WeaponState.Selected; // SAVE

//            PlayerPrefs.SetInt(gameData.SelectedWeaponID, weaponID);
//        }
//        else
//        {
//            if (PlayerPrefs.GetInt(gameData.GetCollectCoins()) > WeaponButtons[weaponID].GetComponent<WeaponButton>().price)
//            {
//                PlayerPrefs.SetInt(gameData.GetCollectCoins(), PlayerPrefs.GetInt(gameData.GetCollectCoins()) -
//                    WeaponButtons[weaponID].GetComponent<WeaponButton>().price);

//                sound_Manager.PurchaseAccept();

//                menuController.RefreshMainMenu();

//                for (int i = 0; i < WeaponButtons.Count; i++)
//                {
//                    WeaponButtons[i].GetComponent<WeaponButton>().State = WeaponButton.WeaponState.DontSelected;
//                    WeaponsList.Weapons[i].State = WeaponButton.WeaponState.DontSelected; // SAVE
//                }

//                WeaponButtons[weaponID].GetComponent<WeaponButton>().Status = WeaponButton.WeaponStatus.Purchased;
//                WeaponsList.Weapons[weaponID].Status = WeaponButton.WeaponStatus.Purchased; // SAVE

//                WeaponsList.Weapons[weaponID].State = WeaponButton.WeaponState.Selected; // SAVE
//            }
//            else
//            {
//                sound_Manager.PurchaseDenied();
//            }
//        }

//        CheckWeaponStateAndStatus();

//    }

    public void SaveWeaponsData()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Weapons.json");
#else
        path = Path.Combine(Application.dataPath, "Weapons.json");
#endif
        File.WriteAllText(path, JsonUtility.ToJson(UnitParameters));

    }
    public void DeleteWeaponData()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Weapons.json");
#else
        path = Path.Combine(Application.dataPath, "Weapons.json");
#endif
        File.Delete(path);
    }
}
