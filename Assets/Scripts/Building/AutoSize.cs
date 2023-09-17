using UnityEngine;

public class AutoSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CalculateSize();
    }

    private void CalculateSize()
    {
        Vector2 size = new Vector2(Mathf.Ceil(transform.parent.GetComponent<BoxCollider2D>().size.x), Mathf.Ceil(transform.parent.GetComponent<BoxCollider2D>().size.y));
        transform.localScale = size;
    }
}
