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
                    foreach (StockEntity stockEntity in unitOfWork.Stocks.GetAll())
                    {
                        stockEntity.StockIngredients.Add(new StockIngredientEntity
                        {
                            Stock = stockEntity,
                            Ingredient = ingredientEntity
                        });
                    }

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

        public ControllerMessage Update(IngredientEditDTO ingredientEditDTO)
        {
            string message = String.Empty;
            bool success = Validate(ingredientEditDTO.NewName, ref message);

            if (success)
            {
                try
                {
                    IngredientEntity ingredientEntity = unitOfWork.Ingredients.Get(ingredientEditDTO.OldName);
                    if (ingredientEntity != null)
                    {
                        ingredientEntity.Name = ingredientEditDTO.NewName;
                        unitOfWork.Commit();

                        message = "Ingredient changed";
                    }
                    else
                    {
                        success = false;
                        message = "Ingredient not found";
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                }
            }

            return new ControllerMessage(success, message);
        }

        public ControllerMessage Delete(string ingredientName)
        {
            string message = String.Empty;
            bool success = true;

            try
            {
                IngredientEntity ingredientEntity = unitOfWork.Ingredients.Get(ingredientName);
                if (ingredientEntity != null)
                {
                    unitOfWork.Ingredients.Remove(ingredientEntity);
                    unitOfWork.Commit();

                    message = "Ingredient deleted";
                }
                else
                {
                    success = false;
                    message = "Ingredient not found";
                }
            }
            catch (Exception ex)
            {
                success = false;
                message = ExceptionMessageBuilder.BuildMessage(ex);
            }

            return new ControllerMessage(success, message);
        }

        public DataControllerMessage<IngredientEditDTO> Get(string ingredientName)
        {
            string message = String.Empty;
            bool success = true;
            IngredientEditDTO data = null;

            try
            {
                IngredientEntity ingredientEntity = unitOfWork.Ingredients.Get(ingredientName);
                if (ingredientEntity != null)
                {
                    data = new IngredientEditDTO
                    {
                        NewName = ingredientEntity.Name,
                        OldName = ingredientEntity.Name,
                        StocksAvailable = unitOfWork.Stocks.GetAllWithIngredient(ingredientName)
                            .Select(stock => stock.StockNo)
                            .OrderBy(stockNo => stockNo)
                            .ToList()
                    };
                }
                else
                {
                    success = false;
                    message = "Ingredient not found";
                }
            }
            catch (Exception ex)
            {
                success = false;
                message = ExceptionMessageBuilder.BuildMessage(ex);
            }

            return new DataControllerMessage<IngredientEditDTO>(success, message, data);
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
