using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    int _max = 101;
    int _oneBoxParsent = 70;
    int _threeBoxParsent = 20;
    int _fiveBoxParsent = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BoxControl _boxControl = FindObjectOfType<BoxControl>();
            var r = Random.Range(1, _max);

            if (r <= _fiveBoxParsent)
            {
                _boxControl.GetBox(5);
            }
            else if (r < _fiveBoxParsent && r <= _threeBoxParsent)
            {
                _boxControl.GetBox(3);
            }
            else
            {
                _boxControl.GetBox(1);
            }
            gameObject.SetActive(false);
        }
    }

}
