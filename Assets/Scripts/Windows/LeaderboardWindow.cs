using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.Linq;

public class LeaderboardWindow : Window
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private RectTransform content;
    [SerializeField] private Button saveButton;
    private Dictionary<string, int> leaders = new Dictionary<string, int>();

    private void Awake()
    {
        saveButton.onClick.AddListener(Save);
        windowType = WindowType.leaderboard;
        base.Awake();
    }

    private void Save()
    {
        if (inputField.text == "")
        {
            return;
        }
        if (leaders.ContainsKey(inputField.text))
        {
            if (leaders[inputField.text] < PlayerController.currentPlace)
            {
                leaders[inputField.text] = PlayerController.currentPlace;
            }
        }
        else
        {
            leaders.Add(inputField.text, PlayerController.currentPlace);
        }
        leaders = leaders.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        var json = JsonConvert.SerializeObject(leaders);
        PlayerPrefs.SetString("leaders", json);
        MessagesManager.SendMessage(new RestartMessage());
    }

    public override void Restart()
    {
        var json = PlayerPrefs.GetString("leaders");
        if (json.Length == 0)
        {
            return;
        }
        leaders = (Dictionary<string, int>)JsonConvert.DeserializeObject(json, typeof(Dictionary<string, int>));

        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }

        foreach (var lead in leaders)
        {
            Instantiate(DataManager.DataSO.leaderPrefab, content).GetComponent<Leader>().SetValues(lead.Key, lead.Value.ToString());
        }
    }
}
