using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static Enums;

public class TrophyContainer : MonoBehaviour
{
    private UserData userData;
    private GameObject badgeHolder;

    private void Start()
    {
        userData = SaveController.Load();

        if (userData == null)
        {
            //Debug.Log("UserData is null");
            userData = new UserData();
            SaveController.Save(userData);
        }

        var badges = userData.Badges;

        // loop through badges enum
        var values = Enum.GetValues(typeof(Badges));

        foreach (var value in values)
        {
            badgeHolder = GameObject.Find(value.ToString());
            badgeHolder.transform.GetComponentInChildren<TMP_Text>().text = BadgeController.GetBadgeText((Badges)value);

            if (badges != null)
            {
                if (badges.ContainsKey((Badges)value))
                {
                    badgeHolder.transform.GetComponentInChildren<TMP_Text>().color = Color.white;
                    badgeHolder.transform.GetComponentInChildren<Graphic>().color = new Color32(255, 195, 76, 255);
                }
            }
        }
    }
}
