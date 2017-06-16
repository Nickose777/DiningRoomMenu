﻿using DiningRoomMenu.Contracts.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiningRoomMenu.Contracts.ViewControllers
{
    public interface IIngredientViewController : IIngredientSubject
    {
        UIElement GetAddView();

        UIElement GetEditView();

        UIElement GetListView();
    }
}
