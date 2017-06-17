using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.DTO.Dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiningRoomMenu.Contracts.ViewControllers
{
    public interface IDishViewController : IDishSubject
    {
        event GenericEventHandler<string> DishDeleted;

        UIElement GetAddView(ICategorySubject subject);

        UIElement GetEditView(IIngredientSubject subject, DishEditDTO dishEditDTO);

        UIElement GetListView(IIngredientSubject subject);
    }
}
