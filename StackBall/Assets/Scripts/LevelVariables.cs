using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVariables
{
	public static int level { get; set; }

	public static int getLevel()
	{
		return level;
	}

	public static int getTotalLayers()
	{
		return (level + 1) * 5;
	}

	public static void levelUp()
	{
		level = level + 1;
	}

	public static void reset()
	{
		level = 0;
	}
}