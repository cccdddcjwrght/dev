using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Android;
using UnityEngine;

public class AndroidPostBuildProcessor : IPostGenerateGradleAndroidProject
{

	const string gooleservicefile = "google-services.json";
	const string profile = "com.one.fline.fpro.jks";
	const string vfile = "values-uhi.xml";

	const string rootdir = @"/launcher/";
	const string valuedir = @"src/main/res/values/";


	public int callbackOrder
	{
		get
		{
			return 999;
		}
	}

	void IPostGenerateGradleAndroidProject.OnPostGenerateGradleAndroidProject(string path)
	{
		Console.WriteLine("android project path:" + path);
		var rp = Path.GetDirectoryName(path) + rootdir;
		CopyToRoot(gooleservicefile, rp);
		CopyToRoot(profile, rp);
		CopyToRoot(vfile, rp + valuedir);
	}

	void CopyToRoot(string file, string root)
	{
		var name = Path.GetFileNameWithoutExtension(file);
		var extend = Path.GetExtension(file);

		var files = AssetDatabase.FindAssets(name);
		if (files != null && files.Length > 0)
		{
			files = files.Select(f => AssetDatabase.GUIDToAssetPath(f)).ToArray();
			name = files.FirstOrDefault(f => f.Contains(file));
			if (!string.IsNullOrEmpty(name) && File.Exists(name))//´æÔÚ²Å¿½±´
				File.Copy(name, root + file, true);
		}
	}

}
