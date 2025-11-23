using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] TMP_Text spellText;
    [SerializeField] SpriteRenderer fireballSprite;
    [SerializeField] SpriteRenderer windSprite;
    [SerializeField] SpriteRenderer killSprite;
    [SerializeField] SpriteRenderer arrowSprite;
    public List<Tuple<SpriteRenderer, int>> activeEffects;

    private void Awake()
    {
        activeEffects = new List<Tuple<SpriteRenderer, int>>();
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
            case "FJFJ":
                spellText.text = "Fireball";
                spellText.color = fireballSprite.color;
                CastFireball(EnemyManager.instance.ClosestEnemy());
                break;
            case "JFJJFJ":
                spellText.text = "Gust of Wind";
                spellText.color = windSprite.color;
                spellText.alpha = 1;
                CastGustOfWind(EnemyManager.instance.ClosestEnemy());
                break;
            case "FJJJ":
                spellText.text = "Power Word: Kill";
                spellText.color = killSprite.color;
                CastPWKill(EnemyManager.instance.ClosestEnemy());
                break;
            case "FFJJ":
                spellText.text = "Magic Arrow";
                spellText.color = arrowSprite.color;
                CastMagicArrow(EnemyManager.instance.ClosestEnemy());
                break;
            default:
                spellText.text = "No Spell Cast";
                spellText.color = Color.black;
                break;
        }
    }

    void CastFireball(Enemy e)
    {
        if (e == null)
        {
            return;
        }
        SpriteRenderer fireball = Instantiate(fireballSprite, transform);
        fireball.transform.position = e.transform.position;
        activeEffects.Add(new Tuple<SpriteRenderer, int>(fireball, 50));
    }

    void CastGustOfWind(Enemy e)
    {
        if (e == null)
        {
            return;
        }
        SpriteRenderer wind = Instantiate(windSprite, transform);
        wind.transform.position = e.transform.position;
        activeEffects.Add(new Tuple<SpriteRenderer, int>(wind, 50));
    }
    void CastPWKill(Enemy e)
    {
        if (e == null)
        {
            return;
        }
        SpriteRenderer kill = Instantiate(killSprite, transform);
        kill.transform.position = e.transform.position;
        activeEffects.Add(new Tuple<SpriteRenderer, int>(kill, 5));
    }

    void CastMagicArrow(Enemy e)
    {
        if (e == null)
        {
            return;
        }
        SpriteRenderer arrow = Instantiate(arrowSprite, transform);
        arrow.transform.position = e.transform.position;
        activeEffects.Add(new Tuple<SpriteRenderer, int>(arrow, 30));
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
                    // if (activeEffects[i].Item1.Equals(fireballSprite))
                    // {
                        
                    // }
                    activeEffects[i] = new Tuple<SpriteRenderer, int>(activeEffects[i].Item1, activeEffects[i].Item2 - 1);
                }
            }
        }
    }
}
