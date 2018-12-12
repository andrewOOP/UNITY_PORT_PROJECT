using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLine {

	public enum Type {
		_sprite,
		_talk,
        _name,
        _choice,
		_break,
		text
	}

	public Type type;
	public string[] data;

	public DataLine(Type _type, string[] _data) {
		type = _type;
		data = _data;
	}

	public DataLine(Type _type, string _data) {
		type = _type;

		data = new string[] {_data};
	}

}
