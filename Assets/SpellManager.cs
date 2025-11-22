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
                spellString += "L";
            } else {
                spellString += "R";
            }
        }
        switch (spellString) {
            case "LRL":
                spellText.text = "Cast Fireball!";
                break;
            case "RLR":
                spellText.text = "Cast Ice Spike!";
                break;
            case "LLR":
                spellText.text = "Cast Heal!";
                break;
            case "RRL":
                spellText.text = "Cast Shield!";
                break;
            default:
                spellText.text = "No Spell Cast.";
                break;
        }
    }
}
