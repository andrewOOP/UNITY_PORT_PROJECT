using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

	public TextAsset initScriptFile;
	private Script currScript;

	public GameObject[] charInScene;
	[SerializeField] Text textbox;
    [SerializeField] Text namebox;
    [SerializeField] GameObject choiceObject;

    private bool sceneRunning = false;

    private string textToWrite = "";

    void Awake()
    {
        Messenger.AddListener(GameEvent.START_SCENE, startLoadedScene);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.START_SCENE, startLoadedScene);
    }

    // Use this for initialization
    void Start () {
        if (initScriptFile != null)
        {
            currScript = new Script(initScriptFile);
            startLoadedScene();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (sceneRunning && Input.GetMouseButtonDown (0)) {
			next ();
		}
	}

    public void loadScene(TextAsset script)
    {
        currScript = new Script(script);
    }

    void startLoadedScene()
    {
        sceneRunning = true;
        next();
    }

	void next() {

		string text = "";

		DataLine line = currScript.getNewLine();

        if (line != null)
        {
            if (line.type == DataLine.Type._choice)
            {
                if (line.data.Length > 1)
                {
                    for (int i = 1; i < line.data.Length; i++)
                    {
                        print("add choice");
                    }
                }
            }
            else
            {
                while (line.type != DataLine.Type._break)
                {

                    switch (line.type)
                    {

                        case DataLine.Type._sprite:

                            int index = int.Parse(line.data[0]);
                            GameObject charToSet = charInScene[index];

                            SpriteRenderer sr = charToSet.GetComponent<SpriteRenderer>();

                            Sprite[] charSprites = Resources.LoadAll<Sprite>("Characters/" + line.data[1]);

                            sr.sprite = GetSpriteByName(charSprites, line.data[2]);

                            Resources.UnloadUnusedAssets();

                            break;

                        case DataLine.Type._talk:

                            if (currScript.names.ContainsKey(line.data[0]))
                            {
                                namebox.text = currScript.names[line.data[0]];
                            }
                            else
                            {
                                namebox.text = "Myself";
                            }

                            break;
                        case DataLine.Type.text:

                            text += line.data[0] + "\n";

                            break;
                        default:
                            break;
                    }

                    line = currScript.getNewLine();
                }

                textToWrite = text;
                StopAllCoroutines();
                StartCoroutine(writeText());
            }

        } else
        {
            //If we are at the end of the script
            Messenger.Invoke(GameEvent.END_SCENE);
        }
	}

	private Sprite GetSpriteByName(Sprite[] sprites, string name)
	{

		for (int i = 0; i < sprites.Length; i++)
		{
			if (sprites[i].name == name)
				return sprites[i];
		}

		return sprites[0];
	}

    IEnumerator writeText()
    {
        int index = 0;
        textbox.text = "";
        char[] charArray = textToWrite.ToCharArray();

        while (index < charArray.Length-1) {

            char currChar = charArray[index];

            textbox.text += currChar;

            float timeToNext = 0.02f;

            switch (currChar)
            {
                case '.':
                    timeToNext = 0.2f;
                    break;
                case '-':
                    timeToNext = 0.3f;
                    break;

                case ',':
                    timeToNext = 0.15f;
                    break;

                default:
                    break;
            }

            yield return new WaitForSeconds(timeToNext);

            index++;
        }
    }
}
