using Common;
using UnityEngine;

public class Asteroid : Destructable
{
    [SerializeField] private GameObject onStoneCrush;
    public enum Size
    {
        Small,
        Normal,
        Big,
        Huge
    }
    [SerializeField] public Size size;
    [SerializeField] private float spawnUpForce;
    [SerializeField] public Transform stoneSprite;



    private Color stoneColor;

    private void Awake()
    {
        //onDie.AddListener(OnStoneDestroyed);
        SetSize(size);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        //onDie.RemoveListener(OnStoneDestroyed);        
    }
    private void OnStoneDestroyed()
    {
        if (size != Size.Small)
        {
            SpawnStones(this);
        }

        Destroy(gameObject);
    }
    public void SpawnStones(Asteroid asteroidPrefab)
    {
        stoneColor = new Color(Random.value, Random.value, Random.value);
        for (int i = 0; i < 2; i++)
        {
            Asteroid stone = Instantiate(asteroidPrefab, asteroidPrefab.transform.position, Quaternion.identity);
            stone.SetSize(size - 1);
            stone._hitPoints = Mathf.Clamp(_hitPoints / 2, 1, _hitPoints);
            stone.transform.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(0,2), Random.Range(0, 2), 0) ;

            SpriteRenderer sr = stone.stoneSprite.GetComponent<SpriteRenderer>();
            sr.color = stoneColor;
        }
    }

    public void SetSize(Size size)
    {
        if (size < 0)
        {
            return;
        }

        transform.localScale = GetVectorFromSize(size);
        this.size = size;
    }

    private Vector3 GetVectorFromSize(Size size)
    {
        if (size == Size.Huge)
        {
            return new Vector3(1, 1, 1);
        }
        if (size == Size.Big)
        {
            return new Vector3(0.75f, 0.75f, 0.75f);
        }
        if (size == Size.Normal)
        {
            return new Vector3(0.6f, 0.6f, 0.6f);
        }
        if (size == Size.Small)
        {
            return new Vector3(0.4f, 0.4f, 0.4f);
        }

        return Vector3.one;
    }

}
