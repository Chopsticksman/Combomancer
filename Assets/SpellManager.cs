using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] TMP_Text spellText;
    public void Cast(List<ButtonType> list) {
        string spellString = "";
        foreach (ButtonType bt in list) {
            if (bt == ButtonType.Left) {
                spellString += "F";
            } else {
                spellString += "J";
            }
        }
        switch (spellString) {
            case "FJF":
                spellText.text = "Cast Fireball!";
                break;
            case "JFJ":
                spellText.text = "Cast Ice Spike!";
                break;
            case "FFJ":
                spellText.text = "Cast Heal!";
                break;
            case "JJF":
                spellText.text = "Cast Shield!";
                break;
            default:
                spellText.text = "No Spell Cast.";
                break;
        }
    }
}
