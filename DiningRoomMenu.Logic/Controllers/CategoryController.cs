﻿using System;
using System.Linq;
using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.Infrastructure;
using System.Collections.Generic;
using DiningRoomMenu.Logic.DTO.Category;
using DiningRoomMenu.Logic.DTO.Dish;

namespace DiningRoomMenu.Logic.Controllers
{
    class CategoryController : ControllerBase, ICategoryController
    {
        public CategoryController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public ControllerMessage Add(string categoryName)
        {
            string message = String.Empty;
            bool success = Validate(categoryName, ref message);

            if (success)
            {
                try
                {
                    CategoryEntity categoryEntity = new CategoryEntity
                    {
                        Name = categoryName
                    };

                    unitOfWork.Categories.Add(categoryEntity);
                    unitOfWork.Commit();

                    message = "Category added";
                }
                catch (Exception ex)
                {
                    success = false;
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                }
            }

            return new ControllerMessage(success, message);
        }

        public ControllerMessage Update(CategoryEditDTO categoryEditDTO)
        {
            string message = String.Empty;
            bool success = Validate(categoryEditDTO.NewName, ref message);

            if (success)
            {
                try
                {
                    CategoryEntity categoryEntity = unitOfWork.Categories.Get(categoryEditDTO.OldName);
                    categoryEntity.Name = categoryEditDTO.NewName;

                    unitOfWork.Commit();

                    message = "Category changed";
                }
                catch (Exception ex)
                {
                    success = false;
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                }
            }

            return new ControllerMessage(success, message);
        }

        public DataControllerMessage<CategoryEditDTO> Get(CategoryDisplayDTO categoryDisplayDTO)
        {
            string message = String.Empty;
            bool success = true;
            CategoryEditDTO data = null;

            try
            {
                CategoryEntity categoryEntity = unitOfWork.Categories.Get(categoryDisplayDTO.Name);
                data = new CategoryEditDTO
                {
                    NewName = categoryEntity.Name,
                    OldName = categoryEntity.Name
                };

                foreach (DishEntity dishEntity in categoryEntity.Dishes)
                {
                    data.Dishes.Add(new DishDisplayDTO
                    {
                        CategoryName = categoryEntity.Name,
                        Name = dishEntity.Name,
                        Price = dishEntity.Price
                    });
                }
            }
            catch (Exception ex)
            {
                success = false;
                message = ExceptionMessageBuilder.BuildMessage(ex);
            }

            return new DataControllerMessage<CategoryEditDTO>(success, message, data);
        }

        public DataControllerMessage<IEnumerable<CategoryDisplayDTO>> GetAll()
        {
            string message = String.Empty;
            bool success = true;
            IEnumerable<CategoryDisplayDTO> data = null;

            try
            {
                data = unitOfWork.Categories.GetAll()
                    .Select(category => new CategoryDisplayDTO { Name = category.Name })
                    .OrderBy(category => category.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                success = false;
                message = ExceptionMessageBuilder.BuildMessage(ex);
            }

            return new DataControllerMessage<IEnumerable<CategoryDisplayDTO>>(success, message, data);
        }

        private bool Validate(string categoryName, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(categoryName))
            {
                isValid = false;
                message = "Category's name cannot be empty";
            }
            else if (categoryName.Length > 40)
            {
                isValid = false;
                message = "Category's name cannot be longer then 40 symbols";
            }
            else if (unitOfWork.Categories.GetAll().Any(category => category.Name == categoryName))
            {
                isValid = false;
                message = "Category with such name already exists";
            }

            return isValid;
        }
    }
}
