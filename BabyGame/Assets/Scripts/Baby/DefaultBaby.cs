using Assets.Scripts.Baby;
using Assets.Scripts.Baby.Need;
using UnityEngine;


class DefaultBaby : AbstractBaby
{
    public override void Start()
    {
        base.Start();
        addNeed(new Need(ENeedType.Hunger, 10));
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

