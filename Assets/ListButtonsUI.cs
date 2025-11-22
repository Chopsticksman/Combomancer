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
	float xOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
		createdElements = new List<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void DrawLeft() 
	{
		Image newImage = Instantiate(LeftIndicator, Target.transform.parent);
		newImage.transform.position += new Vector3(xOffset, 0, 0);
		xOffset += LeftIndicator.sprite.rect.width + offsetIncrement;
		createdElements.Add(newImage);
	}

	public void DrawRight() 
	{
		Image newImage = Instantiate(RightIndicator, Target.transform.parent);
		newImage.transform.position += new Vector3(xOffset, 0, 0);
		xOffset += RightIndicator.sprite.rect.width + offsetIncrement;
		createdElements.Add(newImage);
	}

	public void DeleteAll() 
	{
		for(int i = 0; i < createdElements.Count; i++) 
		{
			Image e = createdElements[i];
			Object.Destroy(e);
		}
	}
}
