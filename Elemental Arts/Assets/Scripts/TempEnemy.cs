using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempEnemy : MonoBehaviour
{
    [SerializeField] private GameObject dmgPanel;
    [SerializeField] private float panelDuration = 1f;
    [SerializeField] private int damage = 2;
    [SerializeField] private int health = 100;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject dmgText = Instantiate(dmgPanel, transform.position, Quaternion.identity, transform);
        dmgText.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
        dmgText.transform.rotation = Quaternion.LookRotation(dmgText.transform.position - Camera.main.transform.position);
        IEnumerator panelMove = PanelMove(dmgText);
        IEnumerator panelLook = PanelLook(dmgText);
        StartCoroutine(panelMove);
        StartCoroutine(panelLook);
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health -= 10;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    IEnumerator PanelLook(GameObject obj)
    {
        float counter = 0;
        while (counter <= panelDuration)
        {
            obj.transform.rotation = Quaternion.LookRotation(obj.transform.position - Camera.main.transform.position);
            counter += Time.deltaTime;
            yield return null;
        }
        Destroy(obj);
    }

    IEnumerator PanelMove(GameObject obj)
    {
        float xOffset = Random.Range(-0.5f, 0.5f);
        float yOffset = Random.Range(1f, 1.5f);
        float counter = 0;
        float value;
        while (counter <= panelDuration/2)
        {
            value = counter / (panelDuration/2);
            obj.transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(xOffset, yOffset, 0f), value);
            counter += Time.deltaTime;
            yield return null;
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}

