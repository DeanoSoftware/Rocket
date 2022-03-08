using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustButton : MonoBehaviour
{
    void Update()
    {
        /*
        if (Input.GetMouseButton(0))
        {
            GameController.Instance.ClickButton();
        } else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Release");
            GameController.Instance.Release();
        }
        

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("thrust");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("mousePos:"+mousePos);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            Debug.Log("mousePos2D:" + mousePos2D);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
        */
    }

    private void OnMouseDown()
    {
        GameController.Instance.Thrust();
    }

    private void OnMouseUp()
    {
        GameController.Instance.Release();
    }
}
