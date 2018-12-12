using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class Script {

	private int _counter = 0;
	private List<DataLine> _script;

    public Dictionary<string,string> names;

	public Script(TextAsset file) {

		parse (file.text);
	}

	void parse(string data) {
		
		List<DataLine> contents = new List<DataLine> ();
        names = new Dictionary<string, string>();


        string[] dataArray = data.Split ('\n');

		for (int i = 0; i < dataArray.Length; i++) {

			string currLine = dataArray [i];
			char[] charArray = dataArray [i].ToCharArray ();

            string passedData;


            if (charArray [0] == '_') {
				//this is code, not text
				switch (charArray [1]) {
				    case 's':
					    //someone will begin to talk
					    passedData = Regex.Match (currLine, @"\(([^)]*)\)").Groups [1].Value;

					    contents.Add(new DataLine(DataLine.Type._sprite, passedData.Split(',')));

					    break;

                    case 'c':
                        //someone will begin to talk
                        passedData = Regex.Match(currLine, @"\(([^)]*)\)").Groups[1].Value;

                        contents.Add(new DataLine(DataLine.Type._choice, passedData.Split(',')));

                        break;

                    case 't':

                        passedData = Regex.Match(currLine, @"\(([^)]*)\)").Groups[1].Value;

                        string talkingCharID = "-1";

                        if (passedData != "self")
                        {
                            talkingCharID = passedData;
                        }

                        contents.Add (new DataLine (DataLine.Type._talk, talkingCharID));
					    break;

                    case 'n':

                        passedData = Regex.Match(currLine, @"\(([^)]*)\)").Groups[1].Value;

                        string[] parsedPData = passedData.Split(',');

                        names.Add(parsedPData[0], parsedPData[1]);
                        break;

                    case 'b':
					    contents.Add (new DataLine (DataLine.Type._break, ""));
					    break;
				    default:
					    break;
				}
			} else {
                //this is not code
                if (currLine != "\r")
                {
                    contents.Add(new DataLine(DataLine.Type.text, currLine));
                }
			}
		}
			

		_script = contents;

	}

	public DataLine getNewLine() {

        if (_counter < _script.Count)
        {
            DataLine returnLine = _script[_counter];
            _counter++;

            return returnLine;
        }

        return null;
	}
}
