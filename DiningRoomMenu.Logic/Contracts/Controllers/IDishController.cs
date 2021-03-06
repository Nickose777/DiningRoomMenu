﻿using DiningRoomMenu.Logic.DTO.Dish;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.Contracts.Controllers
{
    public interface IDishController : IDisposable
    {
        ControllerMessage Add(DishAddDTO dishAddDTO);

        ControllerMessage Update(DishEditDTO dishEditDTO);

        ControllerMessage UpdateRecipes(DishEditDTO dishEditDTO);

        ControllerMessage Delete(string dishName);

        DataControllerMessage<DishEditDTO> Get(string dishName);

        DataControllerMessage<IEnumerable<DishDisplayDTO>> GetAll();
    }
}
