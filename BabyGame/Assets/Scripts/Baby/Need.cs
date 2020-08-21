using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Baby.Need
{
    public class Need
    {
        private ENeedType type;

        private float maxTime;
        private float timeLeft;


        public Need(ENeedType type , float maxTime)
        {
            this.type = type;
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

        public ENeedType getType()
        {
            return type;
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
