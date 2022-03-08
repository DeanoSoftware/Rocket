using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameController.Instance.RotateRight();
    }

    private void OnMouseUp()
    {
        GameController.Instance.ReleaseRotateRight();
    }
}
