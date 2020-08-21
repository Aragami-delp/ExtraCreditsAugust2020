using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Baby.Need
{
    class NeedHunger : INeed
    {
        private float maxTime;
        private float timeLeft;


        public NeedHunger(float maxTime)
        {
            this.maxTime = maxTime;
            this.timeLeft = maxTime;

        }

        public float getMaxTime()
        {
            return maxTime;
        }

        public float getTimeLeft()
        {
            return timeLeft;
        }

        public bool isMet()
        {
            if (timeLeft > 0)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public void refreshTime()
        {
            timeLeft = maxTime;
        }

        public void removeTime(float timeToRemove)
        {
            timeLeft -= timeToRemove;
        }
    }
}
