using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Character Isabelle = CharacterManager.instance.CreateCharacter("Isabelle");
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        Character stella = CharacterManager.instance.CreateCharacter("Isabelle");

        stella.SetPosition(new Vector2(0.5f, 0.5f));

        yield return stella.Hide();
        
        yield return stella.Show();

        yield return new WaitForSeconds(5);

        yield return stella.Hide();

        yield return stella.Say("\"Hello\"");

        yield return new WaitForSeconds(1);

        yield return stella.Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
