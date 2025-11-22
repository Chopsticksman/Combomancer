using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ListButtonsUI : MonoBehaviour
{
	[SerializeField] Image LeftIndicator;
	[SerializeField] Image RightIndicator;
	[SerializeField] Canvas Target;
	[SerializeField] float offsetIncrement;
	List<Image> createdElements;
	public static ListButtonsUI instance;
	float xOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
		createdElements = new List<Image>();
		xOffset = 0;
		instance = this;
    }
		
	public void DrawLeft() 
	{
		Rect r = LeftIndicator.GetComponent<RectTransform>().rect;
        Image newImage = Instantiate(LeftIndicator, Target.transform);
        newImage.transform.position += new Vector3(xOffset + r.width / 2, -r.height / 2, 0);
        xOffset += r.width + offsetIncrement;
		createdElements.Add(newImage);
	}       

	public void DrawRight() 
	{
        Rect r = RightIndicator.GetComponent<RectTransform>().rect;
        Image newImage = Instantiate(RightIndicator, Target.transform);
        newImage.transform.position += new Vector3(xOffset + r.width / 2, -r.height / 2, 0);
        xOffset += r.width + offsetIncrement;
        createdElements.Add(newImage);
	}

	public void DeleteAll() 
	{
		for(int i = 0; i < createdElements.Count; i++) 
		{
            if (createdElements[i] != null)
            {
                Destroy(createdElements[i].gameObject);
            }
		}
		xOffset = 0;
	}
}
