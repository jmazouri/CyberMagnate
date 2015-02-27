using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Reflection;
using DG.Tweening;
using UnityEditor;
using UnityEngine.UI;

[Serializable]
public class TextBinding : MonoBehaviour
{
    public string Format = "";
    public UnityEngine.Object Reference = null;
    public int PropIndex = 0;
    public bool ShowChanges = true;

    private int OldData;

    private Font popupFont;

	void Start()
	{
        popupFont = Resources.LoadAssetAtPath<Font>("Assets/Art/Fonts/NovaMono.ttf");
	    StartCoroutine(PollUpdate());
	    OldData = (int) Reference.GetType().GetFields()[PropIndex].GetValue(Reference);
	}

    IEnumerator PollUpdate()
    {
        while (Reference != null)
        {
            Text cur = GetComponent<Text>();
            FieldInfo info = Reference.GetType().GetFields()[PropIndex];

            int newValue = (int)info.GetValue(Reference);

            if (newValue != OldData)
            {
                DrawFancyAnim(newValue - OldData);
                OldData = (int)info.GetValue(Reference);
            }

            cur.text = String.Format(Format, info.GetValue(Reference));

            yield return new WaitForSeconds(0.1f);
        }
    }

    void DrawFancyAnim(int change)
    {
        var created = new GameObject(change.ToString());
        var curText = created.AddComponent<Text>();

        int randomMod = UnityEngine.Random.Range(-30, 30);

        created.transform.position = transform.position + new Vector3(randomMod, -15, 0);
        print(transform.name+" is animating");
        created.transform.rotation = Quaternion.identity;

        curText.font = popupFont;
        curText.color = new Color(0, 1, 0, 0);
        curText.fontSize = 32;
        curText.fontStyle = FontStyle.Bold;;

        created.transform.SetParent(transform);

        string changeString;

        if (change > 0)
        {
            changeString = "+" + change;
        }
        else
        {
            changeString = change.ToString();
            curText.color = new Color(1, 0, 0, 0);
        }
        curText.text = changeString;

        created.transform.DOMoveY(transform.position.y + 125, 2f)
            .SetEase(Ease.OutQuad);

        created.transform.DOMoveX(transform.position.x + (-randomMod * 1.5f), 2f)
            .SetEase(Ease.OutBack);

        curText.DOColor(curText.color + new Color(0, 0, 0, 1), 0.2f).OnComplete(delegate
        {
            curText.DOColor(curText.color - new Color(0, 0, 0, 1), 1).OnComplete(delegate
            {
                Destroy(created);
            });
        });
    }

}
