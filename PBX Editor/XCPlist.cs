using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
 
namespace UnityEditor.XCodeEditor
{
    public class XCPlistFile : System.IDisposable
    {
 
		private string filePath;
		List<string> contents = new List<string>();
		public XCPlistFile(string fPath)
		{
            filePath = fPath;
            if( !System.IO.File.Exists( filePath ) ) {
                Debug.LogError( filePath +" not found" );
			    return;
			}
 
            FileInfo projectFileInfo = new FileInfo( filePath );
			StreamReader sr = projectFileInfo.OpenText();
			while (sr.Peek() >= 0) 
			{
				contents.Add(sr.ReadLine());
			}
			sr.Close();
 
		}
		public void AddKey(string key)
		{
				if(contents.Count < 2)
						return;
				contents.Insert(contents.Count - 2,key);
 
		}
 
		public void ReplaceKey(string key,string replace){
			for(int i = 0;i < contents.Count;i++){
					if(contents[i].IndexOf(key) != -1){
							contents[i] = contents[i].Replace(key,replace);
					}
			}
		}
 
		public void Save()
		{
            StreamWriter saveFile = File.CreateText(filePath);
			foreach(string line in contents)
					saveFile.WriteLine(line);
			saveFile.Close();
    	}
 
		public void Dispose()
		{
 
		}
    }
}