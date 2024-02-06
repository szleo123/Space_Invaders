using UnityEngine;

public class AlienH : Alien
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        point = 40.0f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
