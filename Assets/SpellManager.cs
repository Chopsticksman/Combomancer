using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] TMP_Text spellText;
    [SerializeField] GameObject fireballSprite;
    [SerializeField] int fireballDamage;
    public List<Tuple<GameObject, int>> activeEffects;

    private void Awake()
    {
        activeEffects = new List<Tuple<GameObject, int>>();
        GameTick.OnTick += DecreaseQueue;
    }
    public void Cast(List<ButtonType> list)
    {
        string spellString = "";
        foreach (ButtonType bt in list)
        {
            if (bt == ButtonType.Left)
            {
                spellString += "F";
            }
            else
            {
                spellString += "J";
            }
        }
        switch (spellString)
        {
            case "FJF":
                spellText.text = "Cast Fireball!";
                //CastFireball(EnemyManager.instance.ClosestEnemy());
                CastFireball(new Vector3(0, 0, 0));
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

    void CastFireball(Vector3 location)
    {
        GameObject fireball = Instantiate(fireballSprite, transform);
        fireball.transform.position = location;
        activeEffects.Add(new Tuple<GameObject, int>(fireball, 50));
    }

    void DecreaseQueue()
    {
        if (activeEffects.Count == 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < activeEffects.Count; i++)
            {
                if (activeEffects[i].Item2 <= 0)
                {
                    Destroy(activeEffects[i].Item1.gameObject);
                    activeEffects.RemoveAt(i);
                }
                else
                {
                    activeEffects[i] = new Tuple<GameObject, int>(activeEffects[i].Item1, activeEffects[i].Item2 - 1);
                }
            }
        }
    }
}
