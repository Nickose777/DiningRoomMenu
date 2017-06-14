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
                            Dish = dish,
                            RecipeIngredients = new List<RecipeIngredientEntity>()
                        };

                        foreach (IngredientPortion ingredientPortion in recipeAddDTO.Ingredients)
                        {
                            IngredientEntity ingredientEntity = unitOfWork.Ingredients.Get(ingredientPortion.Ingredient);
                            if (ingredientEntity != null)
                            {
                                recipeEntity.RecipeIngredients.Add(new RecipeIngredientEntity
                                {
                                    Ingredient = ingredientEntity,
                                    Recipe = recipeEntity,
                                    Portion = ingredientPortion.Portion
                                });
                            }
                        }

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

        public DataControllerMessage<IEnumerable<RecipeDisplayDTO>> GetAll()
        {
            string message = String.Empty;
            bool success = true;
            IEnumerable<RecipeDisplayDTO> data = null;

            try
            {
                data = unitOfWork.Recipes.GetAll()
                    .Select(recipe =>
                    {
                        RecipeDisplayDTO recipeDTO = new RecipeDisplayDTO
                        {
                            Name = recipe.Name,
                            DishName = recipe.Dish.Name,
                            CategoryName = recipe.Dish.Category.Name
                        };

                        foreach (RecipeIngredientEntity recipeIngredient in recipe.RecipeIngredients)
                        {
                            recipeDTO.IngredientPortion.Add(new IngredientPortion 
                            {
                                Ingredient = recipeIngredient.Ingredient.Name,
                                Portion = recipeIngredient.Portion
                            });
                        }

                        return recipeDTO;
                    })
                    .OrderBy(recipe => recipe.CategoryName)
                    .ToList();
            }
            catch (Exception ex)
            {
                success = false;
                message = ExceptionMessageBuilder.BuildMessage(ex);
            }

            return new DataControllerMessage<IEnumerable<RecipeDisplayDTO>>(success, message, data);
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
