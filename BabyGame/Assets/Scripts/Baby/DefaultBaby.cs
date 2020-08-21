using Assets.Scripts.Baby;
using Assets.Scripts.Baby.Need;


class DefaultBaby : AbstractBaby
{
    void Start()
    {
        addNeed(new Need(ENeedType.Hunger, 10));
    }
}

