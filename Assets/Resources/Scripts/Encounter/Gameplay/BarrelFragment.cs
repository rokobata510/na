using UnityEngine;

public class BarrelFragment: DissapearingProjectile
{

    public Sprite[] sprites;
    public bool rotatingRight;
    public float rotationSpeed = 1;
    public float minRotationMultiplier = 0.25f;
    public float maxRotationMultiplier = 4f;
    public void OnEnable()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[UnseededRandomStream.Range(0, sprites.Length)];
        rotatingRight = UnseededRandomStream.Range(0, 2) == 0;
        rotationSpeed *= UnseededRandomStream.Range(minRotationMultiplier, maxRotationMultiplier);

    }
    public void FixedUpdate()
    {
        transform.Rotate(0, 0, rotatingRight ? rotationSpeed : -rotationSpeed);
    }
}

