﻿using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Ingredient;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.Controllers
{
    class IngredientController : ControllerBase, IIngredientController
    {
        public IngredientController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public ControllerMessage Add(string ingredientName)
        {
            string message = String.Empty;
            bool success = Validate(ingredientName, ref message);

            if (success)
            {
                try
                {
                    IngredientEntity ingredientEntity = new IngredientEntity
                    {
                        Name = ingredientName
                    };

                    unitOfWork.Ingredients.Add(ingredientEntity);
                    unitOfWork.Commit();

                    message = "Ingredient added";
                }
                catch (Exception ex)
                {
                    success = false;
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                }
            }

            return new ControllerMessage(success, message);
        }

        public DataControllerMessage<IEnumerable<IngredientDisplayDTO>> GetAll()
        {
            string message = String.Empty;
            bool success = true;
            IEnumerable<IngredientDisplayDTO> data = null;

            try
            {
                data = unitOfWork.Ingredients.GetAll()
                    .Select(ingredient => new IngredientDisplayDTO { Name = ingredient.Name })
                    .OrderBy(ingredient => ingredient.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                success = false;
                message = ExceptionMessageBuilder.BuildMessage(ex);
            }

            return new DataControllerMessage<IEnumerable<IngredientDisplayDTO>>(success, message, data);
        }

        private bool Validate(string ingredientName, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(ingredientName))
            {
                isValid = false;
                message = "Ingredient's name cannot be empty";
            }
            else if (ingredientName.Length > 40)
            {
                isValid = false;
                message = "Ingredient's name cannot be longer then 40 symbols";
            }
            else if (unitOfWork.Ingredients.GetAll().Any(ingredient => ingredient.Name == ingredientName))
            {
                isValid = false;
                message = "Ingredient with such name already exists";
            }

            return isValid;
        }
    }
}
