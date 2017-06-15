﻿using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO;
using DiningRoomMenu.Logic.DTO.Category;
using DiningRoomMenu.Logic.DTO.Dish;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.Controllers
{
    class MenuController : ControllerBase, IMenuController
    {
        public MenuController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public DataControllerMessage<MenuDTO> GetMenu()
        {
            string message = String.Empty;
            bool success = true;
            MenuDTO data = new MenuDTO();

            try
            {
                var categoryEntities = unitOfWork.Categories.GetAll();
                foreach (CategoryEntity categoryEntity in categoryEntities)
                {
                    CategoryMenuDTO category = new CategoryMenuDTO
                    {
                        NewName = categoryEntity.Name,
                        OldName = categoryEntity.Name,
                        Dishes = categoryEntity.Dishes.Select(dish =>
                            new DishMenuDTO
                            {
                                NewName = dish.Name,
                                OldName = dish.Name,
                                Price = dish.Price
                            })
                            .OrderBy(dish => dish.OldName)
                            .ToList()
                    };

                    data.Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                success = false;
                message = ExceptionMessageBuilder.BuildMessage(ex);
            }

            return new DataControllerMessage<MenuDTO>(success, message, data);
        }
    }
}
