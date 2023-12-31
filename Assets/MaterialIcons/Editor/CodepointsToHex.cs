using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Google.MaterialDesign.Icons
{
    
    public class CodepointsToHex 
    {
        [MenuItem("MaterialIcons/CodepointsToHex")]
        public static void CreateCodePointsToHexRange()
        {
            string path=string.Empty;
            foreach(string guid in UnityEditor.AssetDatabase.FindAssets("t:Font MaterialSymbolsOutlined[FILL,GRAD,opsz,wght]"))
            {
                string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);

                if(assetPath.EndsWith(".ttf", System.StringComparison.OrdinalIgnoreCase) && System.IO.File.Exists(System.IO.Path.GetDirectoryName(assetPath) + "/codepoints"))
                {
                    path = assetPath;
                    break;
                }
            }
            string fontPath = path;
            Debug.Log(fontPath);
            string codepointsPath = Path.GetDirectoryName(fontPath) + "/codepoints";
            
            Debug.Log(codepointsPath);
            // load all text from codepoints file
            string[] codepoints = File.ReadAllLines(codepointsPath);
            // each line is in format:  <name> <codepoint>
            // we need to extract the codepoint and convert it to hex
            // then we need to create a range of hex values
            // finally we need to create a string of hex values
            string hexRange = string.Empty;
            foreach (string line in codepoints)
            {
                string[] split = line.Split(' ');
                string codepoint = split[1];
                hexRange += ","+codepoint;
            }
            // create a new text file in the same directory as the codepoints file
            string hexRangePath = Path.GetDirectoryName(codepointsPath) + "/hexRange";
            File.WriteAllText(hexRangePath, hexRange);
            // copy the contents of the hexRange file to the clipboard
            TextEditor te = new TextEditor();
            te.text = hexRange;
            te.SelectAll();
        }

    }
}