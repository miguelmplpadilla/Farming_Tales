using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class DialogeController {
    public List<string> getTextoDialogos(TextAsset dialogos, string ablante, string frase, string idioma)
    {
        List<string> textoDialogo = new List<string>();
        string fs = dialogos.text;
        string[] fLines = Regex.Split ( fs, "\n|\r|\r\n" );

        for (int i = 0; i < fLines.Length; i++)
        {

            string valueLine = fLines[i];
            string[] values = Regex.Split(valueLine, ",");
            if (values[0].Equals(ablante) && values[1].Equals(frase))
            {
                if (idioma.Equals("Español")) {
                    textoDialogo.Add(values[2].Replace(".",","));
                } else if (idioma.Equals("English")) {
                    textoDialogo.Add(values[3].Replace(".",","));
                }
            }
        }
        
        return textoDialogo;
    }
}