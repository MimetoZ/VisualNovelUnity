using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TETING
{
    public class Testin_Architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        string[] line = new string[4]
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
            "Nulla maximus vitae nulla non volutpat.",
            "In scelerisque ipsum sem, at hendrerit massa rhoncus quis.",
            "Pellentesque ac libero pharetra augue ornare semper non eu purus."
        };

        //string lineLong = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed nisi sit amet lacus mollis bibendum. " +
        //    "Vivamus suscipit tempus dolor, quis gravida turpis. Sed ut sem eget mi facilisis cursus sit amet et risus.";

        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.fade;
            architect.speed = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            if (bm != architect.buildMethod)
            {
                architect.buildMethod = bm;
                architect.Stop();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                architect.Stop();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (architect.isBuilding)
                {
                    if (!architect.hurryUp)
                    {
                        architect.hurryUp = true;
                    }
                    else
                        architect.ForceComplete();
                }
                else
                    architect.Build((line[Random.Range(1, line.Length)]));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Append((line[Random.Range(1, line.Length)]));
            }
        }
    }
}