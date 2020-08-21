using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Baby
{
    interface INeed
    {
        bool isMet();

        void removeTime(float timeToRemove);

        void refreshTime();

        float getMaxTime();

        float getTimeLeft();
    }
}
