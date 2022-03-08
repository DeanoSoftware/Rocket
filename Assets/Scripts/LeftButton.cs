using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameController.Instance.RotateLeft();
    }

    private void OnMouseUp()
    {
        GameController.Instance.ReleaseRotateLeft();
    }
}
