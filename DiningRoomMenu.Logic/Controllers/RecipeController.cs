using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Recipe;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.Controllers
{
    class RecipeController : ControllerBase, IRecipeController
    {
        public RecipeController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public ControllerMessage Add(RecipeAddDTO recipeAddDTO)
        {
            string message = String.Empty;
            bool success = Validate(recipeAddDTO, ref message);

            if (success)
            {
                try
                {
                    DishEntity dish = unitOfWork.Dishes.Get(recipeAddDTO.DishName);
                    if (dish != null)
                    {
                        RecipeEntity recipeEntity = new RecipeEntity
                        {
                            Name = recipeAddDTO.Name,
                            Description = recipeAddDTO.Description,
                            Dish = dish
                        };

                        unitOfWork.Recipes.Add(recipeEntity);
                        unitOfWork.Commit();

                        message = "Recipe added";
                    }
                    else
                    {
                        success = false;
                        message = "Dish not found";
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

        private bool Validate(RecipeAddDTO recipeAddDTO, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(recipeAddDTO.Name))
            {
                isValid = false;
                message = "Recipe's name cannot be empty";
            }
            if (recipeAddDTO.Name.Length > 40)
            {
                isValid = false;
                message = "Recipe's name cannot be more then 40 symbols";
            }
            else if (String.IsNullOrEmpty(recipeAddDTO.DishName))
            {
                isValid = false;
                message = "Recipe's dish's name cannot be empty";
            }
            else if (unitOfWork.Recipes.GetAll().Any(recipe => recipe.Name == recipeAddDTO.Name))
            {
                isValid = false;
                message = "Recipe with such name already exists";
            }

            return isValid;
        }
    }
}
