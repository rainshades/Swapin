using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class DamageText : MonoBehaviour
{
    [SerializeField]
    private GameObject DamageTextPrefab;

    public void SpawnNewText(int Damage)
    {
        var Go = Instantiate(DamageTextPrefab, transform.position + Vector3.right * Random.Range(-10, 50) + Vector3.up * Random.Range(-50,50), Quaternion.identity);
        Go.transform.SetParent(transform); 
        var text = Go.GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"{Damage}";
        Go.AddComponent<MovingText>();

        if (name.Contains("Player"))
            text.color = Color.blue;
        else
            text.color = Color.red;

        Go.transform.localScale *= 1.25f; 
    }

    private class MovingText : MonoBehaviour
    {
        private float Timer = 2.0f;
        private float H_Timer = 0.25f;
        Vector2 lr;

        private void Start()
        {
            lr = Random.Range(0, 20) % 2 == 0 ? Vector2.left : Vector2.right;
        }


        private void Update()
        {
            Timer -= Time.deltaTime;
            H_Timer -= Time.deltaTime; 
            transform.Translate((Vector2.up * 5)  * Time.deltaTime * 25);
            transform.Translate(lr * 35 * Time.deltaTime); 

            if(H_Timer <= 0)
            {
                H_Timer = 1.5f;
                lr = lr == Vector2.left ? Vector2.right : Vector2.left;
            }


            if (Timer <= 0)
            {
                Destroy(gameObject); 
            }
        }

    }
}
