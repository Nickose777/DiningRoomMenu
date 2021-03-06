﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Contracts.Subjects
{
    public interface ICategorySubject
    {
        void Subscribe(IObserver observer);

        void Unsubscribe(IObserver observer);

        void Notify();
    }
}
