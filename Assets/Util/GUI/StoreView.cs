using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.UI;
using EnumerableExtensions;
using UnityEngine.Events;

[Serializable]
public class StoreView : MonoBehaviour
{
    public GameObject ElementPrefab;
    public GlobalManager Manager;

	// Use this for initialization
	void Start()
    {
        UpdateDrawn();
    }

    public void OnSelectionChanged(Button b)
    {
        print("You Selected: " + b.GetComponentInChildren<Text>().text);
    }

    private void UpdateDrawn()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "ListItem")
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var field in Manager.GetType().GetFields())
        {
            if (field.GetValue(Manager).CanConvertTo<float>() && field.IsPublic)
            {
                GameObject newPropertyEditor = (GameObject)Instantiate(ElementPrefab);
                newPropertyEditor.name = "Item: " + field.Name;
                newPropertyEditor.transform.SetParent(transform, false);

                Text headerText = newPropertyEditor.GetComponentsInChildren<Text>().FirstOrDefault(d => d.name == "PropName");
                headerText.text = field.Name;

                InputField curInput = newPropertyEditor.GetComponentInChildren<InputField>();
                curInput.text = "0";

                InputField localInput = curInput;

                InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
                submitEvent.AddListener(delegate
                {
                    localInput.text = int.Parse(localInput.text).ZeroMin().ToString();
                });
                curInput.onEndEdit = submitEvent;

                Button increaseButton = newPropertyEditor.GetComponentsInChildren<Button>().FirstOrDefault(d => d.gameObject.name == "IncreaseButton");
                Button decreaseButton = newPropertyEditor.GetComponentsInChildren<Button>().FirstOrDefault(d => d.gameObject.name == "DecreaseButton");
                Button purchaseButton = newPropertyEditor.GetComponentsInChildren<Button>().FirstOrDefault(d => d.gameObject.name == "PurchaseButton");

                increaseButton.onClick.AddListener(delegate
                {
                    int numInput = int.Parse(localInput.text);
                    numInput++;
                    localInput.text = numInput.ZeroMin().ToString();
                });

                decreaseButton.onClick.AddListener(delegate
                {
                    int numInput = int.Parse(localInput.text);
                    numInput--;
                    localInput.text = numInput.ZeroMin().ToString();
                });

                var localField = field;
                purchaseButton.onClick.AddListener(delegate
                {
                    int numInput = int.Parse(localInput.text);
                    localField.SetValue(Manager, (int)localField.GetValue(Manager) + numInput);
                });
            }
        }
    }

	void Update()
    {
        
	}
}
