using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


namespace HierarchyColor
{

    [System.Serializable]
    public class List
    {
        public static List<Colorizer_Settings> colorDesigns = new List<Colorizer_Settings>();
    }

    


    [System.Serializable]
    public class Colorizer_Settings
    {
        public string keyChar;

        public Color bgColor;

        public Color txtColor;

        public TextAnchor textAligment;

        public FontStyle fontStyle;
    }


    public class guiWindow : EditorWindow
    {
        



        private string keychar;
        private Color bgcolor = Color.white;
        private Color txtcolor = Color.white;

       
    


        [MenuItem("Organizer/Organizer setup")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(guiWindow));
        }


        private void OnGUI()
        {

            
            

            GUILayout.Label("Organizer", EditorStyles.boldLabel);

            keychar = EditorGUILayout.TextField("Key char", keychar);

            


            
            bgcolor = EditorGUILayout.ColorField("Background color", bgcolor);
            txtcolor = EditorGUILayout.ColorField("Text color", txtcolor);

            
            // text anchor i font style do dodania póŸniej

            if (GUILayout.Button("Colorize"))
            {
                Colorizer_Settings colorizer = new Colorizer_Settings();
                
                colorizer.keyChar = keychar;
                colorizer.bgColor = bgcolor;
                colorizer.txtColor = txtcolor;
                colorizer.textAligment = TextAnchor.MiddleCenter;
                colorizer.fontStyle = FontStyle.Bold; 

                List.colorDesigns.Add(colorizer);
                Save();
            }


            if(GUILayout.Button("View data", GUILayout.Width(200)))
            {
                Data.ShowWindow();
            }


            Repaint();
            
        }





        public static void Save()
        {
            
            string fileName = "ColorDesigns.txt";
            string directoryPath = "Assets/Editor/Hierarchy";
            string filePath = Path.Combine(directoryPath, fileName);
            
            
            File.WriteAllText(filePath, string.Empty);


            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }



            
            for(int i = 0; i < List.colorDesigns.Count; i++)
            {



                string[] keyChar = {   List.colorDesigns[i].keyChar };
                File.AppendAllLines(filePath, keyChar);
                string[] bg = { List.colorDesigns[i].bgColor.ToString().Replace("RGBA(","").Replace(")", "") };
                File.AppendAllLines(filePath, bg);
                string[] txt = {  List.colorDesigns[i].txtColor.ToString().Replace("RGBA(", "").Replace(")", "") };
                File.AppendAllLines(filePath, txt);
                
            }

            



            AssetDatabase.Refresh();
           
        }

    }










    class Data : EditorWindow
    {
        [MenuItem("Organizer/Organizer data")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(Data));
        }


        private void OnGUI()
        {
            

            if (List.colorDesigns.Count == 0)
            {
                GUILayout.Label("List of designs is empty", EditorStyles.boldLabel);
            }


            for (int i = 0; i < List.colorDesigns.Count; i++)
            {

                string key = List.colorDesigns[i].keyChar;
                Color bg = List.colorDesigns[i].bgColor;
                Color txt = List.colorDesigns[i].txtColor;

                List.colorDesigns[i].keyChar = EditorGUILayout.TextField("Key char", List.colorDesigns[i].keyChar);

                List.colorDesigns[i].bgColor = EditorGUILayout.ColorField("Background color", List.colorDesigns[i].bgColor);
                List.colorDesigns[i].txtColor = EditorGUILayout.ColorField("Text color", List.colorDesigns[i].txtColor);


                if(key != List.colorDesigns[i].keyChar || bg != List.colorDesigns[i].bgColor || txt != List.colorDesigns[i].txtColor)
                {
                    guiWindow.Save();
                }

                if(GUILayout.Button("Remove", GUILayout.Width(200)))
                {
                    List.colorDesigns.RemoveAt(i);
                    guiWindow.Save();
                }

                EditorGUILayout.Space();

            }


        }
}
}


