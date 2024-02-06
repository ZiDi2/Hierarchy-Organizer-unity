using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Globalization;

namespace HierarchyColor
{

    [InitializeOnLoad]
    public class Colorizer
    {
        
        static Colorizer()
        {
            LoadData();
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindow;
        }


        private static void OnHierarchyWindow(int instanceID, Rect selectionRect)
        {

            if (List.colorDesigns.Count == 0) return;

            // Zamiana instanceID na obiekt
            UnityEngine.Object instance = EditorUtility.InstanceIDToObject(instanceID);

            if (instance != null)
            {
                for (int i = 0; i < List.colorDesigns.Count; i++)
                {
                    var design = List.colorDesigns[i];
                    

                    if (instance.name.StartsWith(design.keyChar))
                    {
                        // usuniêcie keychar z pocz¹tku nazwy
                        string newName = instance.name.Substring(design.keyChar.Length);
                        // dodanie koloru do obiektu
                        EditorGUI.DrawRect(selectionRect, design.bgColor);
                        // ustawianie pozosta³ych rzeczy ustawionych przez u¿ytkownika
                        GUIStyle newStyle = new GUIStyle
                        {
                            alignment = design.textAligment,
                            fontStyle = design.fontStyle,
                            normal = new GUIStyleState()
                            {
                                textColor = design.txtColor
                            }
                        };

                        EditorGUI.LabelField(selectionRect, newName.ToUpper(), newStyle);


                    }
                }

            }

        }


        static void LoadData()
        {
            string[] lines = File.ReadAllLines("Assets/Editor/Hierarchy/ColorDesigns.txt");

            for(int i = 0; i < lines.Length; i += 3)
            {
                Colorizer_Settings colorizer = new Colorizer_Settings();


                // Zamiana string na kolor bg
                string[] bgColor = lines[i + 1].Split(",");

                float bgRed, bgGreen, bgBlue, bgAlpha;


                bgRed = float.Parse(bgColor[0], CultureInfo.InvariantCulture);
                bgGreen = float.Parse(bgColor[1], CultureInfo.InvariantCulture);
                bgBlue = float.Parse(bgColor[2], CultureInfo.InvariantCulture);
                bgAlpha = float.Parse(bgColor[3], CultureInfo.InvariantCulture);


                // zmiana string na kolor txt
                string[] txtColor = lines[i + 2].Split(",");

                float txtRed, txtGreen, txtBlue, txtAlpha;


                txtRed = float.Parse(txtColor[0], CultureInfo.InvariantCulture);
                txtGreen = float.Parse(txtColor[1], CultureInfo.InvariantCulture);
                txtBlue = float.Parse(txtColor[2], CultureInfo.InvariantCulture);
                txtAlpha = float.Parse(txtColor[3], CultureInfo.InvariantCulture);



                Color bgcolor = new Color(bgRed, bgGreen, bgBlue, bgAlpha);
                Color txtcolor = new Color(txtRed, txtGreen, txtBlue, txtAlpha);



                colorizer.keyChar = lines[i];
                colorizer.bgColor = bgcolor;
                colorizer.txtColor = txtcolor;
                colorizer.textAligment = TextAnchor.MiddleCenter;
                colorizer.fontStyle = FontStyle.Bold;

                List.colorDesigns.Add(colorizer);
            }

          
        }

        

    }
    



}



