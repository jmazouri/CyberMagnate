using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using EnumerableExtensions;
using UnityEngine.Events;

[Serializable]
public class ListView : MonoBehaviour
{
    public List<String> Items = new List<string>();
    private List<String> OldItems = new List<string>();
    public GameObject TextPrefab;

	// Use this for initialization
	void Start()
    {
	    
    }

    public void OnSelectionChanged(Button b)
    {
        print("You Selected: " + b.GetComponentInChildren<Text>().text);
    }

    private void UpdateDrawn()
    {
        if (Items.IsIdenticalTo(OldItems)) return;

        foreach (Transform child in transform)
        {
            if (child.tag == "ListItem")
            {
                Destroy(child.gameObject);
            }
        }

        foreach (string s in Items)
        {
            GameObject newButtonObj = (GameObject)Instantiate(TextPrefab);
            newButtonObj.name = "Item: " + s;

            Button newButton = newButtonObj.GetComponent<Button>();
            Text newText = newButtonObj.GetComponentInChildren<Text>();

            newButtonObj.transform.SetParent(transform, false);

            newText.text = s;
            newButton.onClick.AddListener(delegate
            {
                Button curButton = newButton;
                OnSelectionChanged(curButton);
            });
        }

        OldItems.Clear();
        OldItems.AddRange(Items);
    }

	void Update()
    {
        UpdateDrawn();
	}
}
