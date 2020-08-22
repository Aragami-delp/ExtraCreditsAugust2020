using Assets.Scripts.Baby;
using Assets.Scripts.Baby.Need;


class DefaultBaby : AbstractBaby
{
    void Start()
    {
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

