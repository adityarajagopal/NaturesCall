using UnityEngine;
using System.Collections;

public class RegularPlatform : Platform {
	public RegularPlatform (string i_entry, string i_exit, string i_platformType, GameObject i_platform) { 
		this.details = new Details (i_entry, i_exit, i_platformType, i_platform); 
	}
}
