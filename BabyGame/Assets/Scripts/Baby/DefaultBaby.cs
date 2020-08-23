using UnityEngine;


class DefaultBaby : AbstractBaby
{
    public override void Start()
    {
        base.Start();
        addNeed(new Need(ENeedType.Poop, 10));
        addNeed(new Need(ENeedType.Hunger, 10));
        addNeed(new Need(ENeedType.Sleep, 20));
    }


    public override Item PickItem()
    {
        m_collider.enabled = false;
        return this;
    }

    public override void DropItem()
    {
        m_collider.enabled = true;
    }
}

