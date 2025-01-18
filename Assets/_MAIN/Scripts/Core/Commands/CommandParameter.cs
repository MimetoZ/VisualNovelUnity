using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandParameter
{
    private const char PARAMETER_IDENTIFIER = '-';
    private Dictionary<string, string> parameters = new Dictionary<string, string>();

    public CommandParameter(string[] parameterArray)
    {

        for (int i = 0; i < parameterArray.Length; i++)
        {
            if (parameterArray[i].StartsWith(PARAMETER_IDENTIFIER))
            {
                string pName = parameterArray[i];
                string pValue = "";
                if (i + 1 < parameterArray.Length && !parameterArray[i + 1].StartsWith(PARAMETER_IDENTIFIER))
                {
                    pValue = parameterArray[i + 1];
                    i++;
                }

                parameters.Add(pName, pValue);
            }
        }
    }
}
