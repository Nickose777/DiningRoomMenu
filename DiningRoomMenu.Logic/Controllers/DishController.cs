using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Dish;
using DiningRoomMenu.Logic.DTO.Recipe;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Logic.Controllers
{
    class DishController : ControllerBase, IDishController
    {
        public DishController(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public ControllerMessage Add(DishAddDTO dishAddDTO)
        {
            string message = String.Empty;
            bool success = Validate(dishAddDTO, ref message);

            if (success)
            {
                try
                {
                    CategoryEntity category = unitOfWork.Categories.Get(dishAddDTO.CategoryName);
                    if (category != null)
                    {
                        DishEntity dishEntity = new DishEntity
                        {
                            Name = dishAddDTO.Name,
                            Price = dishAddDTO.Price,
                            Category = category
                        };

                        unitOfWork.Dishes.Add(dishEntity);
                        unitOfWork.Commit();

                        message = "Dish added";
                    }
                    else
                    {
                        success = false;
                        message = "Category not found";
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

        public ControllerMessage Update(DishEditDTO dishEditDTO)
        {
            string message = String.Empty;
            bool success = Validate(dishEditDTO, ref message);

            if (success)
            {
                try
                {
                    DishEntity dishEntity = unitOfWork.Dishes.Get(dishEditDTO.OldName);
                    dishEntity.Name = dishEditDTO.NewName;
                    dishEntity.Price = dishEditDTO.Price;

                    unitOfWork.Commit();

                    message = "Dish changed";
                }
                catch (Exception ex)
                {
                    success = false;
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                }
            }

            return new ControllerMessage(success, message);
        }

        public ControllerMessage UpdateRecipes(DishEditDTO dishEditDTO)
        {
            string message = String.Empty;
            bool success = Validate(dishEditDTO, ref message);

            if (success)
            {
                try
                {
                    DishEntity dishEntity = unitOfWork.Dishes.Get(dishEditDTO.OldName);

                    IEnumerable<string> added = dishEditDTO.Recipes
                        .Select(recipe => recipe.Name)
                        .Where(recipeName => !dishEntity.Recipes.Select(recipe => recipe.Name).Contains(recipeName));
                    IEnumerable<string> deleted = dishEntity.Recipes
                        .Select(recipe => recipe.Name)
                        .Where(recipeName => !dishEditDTO.Recipes.Select(recipe => recipe.Name).Contains(recipeName))
                        .ToList();

                    foreach (DishRecipeEditDTO recipe in dishEditDTO.Recipes.Where(recipe => added.Contains(recipe.Name)))
                    {
                        if (!unitOfWork.Recipes.Exists(recipe.Name))
                        {
                            RecipeEntity recipeEntity = new RecipeEntity
                            {
                                Name = recipe.Name,
                                Description = recipe.Description,
                                Dish = dishEntity,
                                RecipeIngredients = recipe.Ingredients.Select(ingredient => new RecipeIngredientEntity
                                {
                                    Portion = ingredient.Portion,
                                    Ingredient = unitOfWork.Ingredients.Get(ingredient.Ingredient)
                                })
                                .ToList()
                            };
                            unitOfWork.Recipes.Add(recipeEntity);
                        }
                    }
                    foreach (string recipeName in deleted)
                    {
                        RecipeEntity recipeEntity = dishEntity.Recipes.Single(recipe => recipe.Name == recipeName);
                        unitOfWork.Recipes.Remove(recipeEntity);
                    }

                    unitOfWork.Commit();

                    message = "Dish's recipes changed";
                }
                catch (Exception ex)
                {
                    success = false;
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                }
            }

            return new ControllerMessage(success, message);
        }

        public ControllerMessage Delete(string dishName)
        {
            string message = String.Empty;
            bool success = true;

            try
            {
                DishEntity dishEntity = unitOfWork.Dishes.Get(dishName);
                if (dishEntity != null)
                {
                    unitOfWork.Dishes.Remove(dishEntity);
                    unitOfWork.Commit();

                    message = "Dish deleted";
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

            return new ControllerMessage(success, message);
        }

        public DataControllerMessage<DishEditDTO> Get(string dishName)
        {
            string message = String.Empty;
            bool success = true;
            DishEditDTO data = null;

            try
            {
                DishEntity dishEntity = unitOfWork.Dishes.Get(dishName);
                if (dishEntity != null)
                {
                    data = new DishEditDTO
                    {
                        NewName = dishEntity.Name,
                        OldName = dishEntity.Name,
                        Price = dishEntity.Price,
                        CategoryName = dishEntity.Category.Name
                    };

                    foreach (RecipeEntity recipeEntity in dishEntity.Recipes)
                    {
                        DishRecipeEditDTO dishRecipe = new DishRecipeEditDTO
                        {
                            Name = recipeEntity.Name,
                            Description = recipeEntity.Description
                        };

                        var ingredients = recipeEntity.RecipeIngredients.Select(ri => new IngredientPortion
                            {
                                Ingredient = ri.Ingredient.Name,
                                Portion = ri.Portion
                            })
                            .OrderBy(ingredient => ingredient.Ingredient);
                        dishRecipe.Ingredients.AddRange(ingredients);

                        data.Recipes.Add(dishRecipe);

                    }
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

            return new DataControllerMessage<DishEditDTO>(success, message, data);
        }

        public DataControllerMessage<IEnumerable<DishDisplayDTO>> GetAll()
        {
            string message = String.Empty;
            bool success = true;
            IEnumerable<DishDisplayDTO> data = null;

            try
            {
                data = unitOfWork.Dishes.GetAll()
                    .Select(dish => new DishDisplayDTO 
                    { 
                        Name = dish.Name,
                        Price = dish.Price,
                        CategoryName = dish.Category.Name
                    })
                    .OrderBy(dish => dish.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                success = false;
                message = ExceptionMessageBuilder.BuildMessage(ex);
            }

            return new DataControllerMessage<IEnumerable<DishDisplayDTO>>(success, message, data);
        }

        private bool Validate(DishAddDTO dishAddDTO, ref string message)
        {
            bool isValid = true;
            string name = dishAddDTO.Name;
            string categoryName = dishAddDTO.CategoryName;
            decimal price = dishAddDTO.Price;

            if (String.IsNullOrEmpty(name))
            {
                isValid = false;
                message = "Dish's name cannot be empty";
            }
            if (name.Length > 40)
            {
                isValid = false;
                message = "Dish's name cannot be more then 40 symbols";
            }
            else if (String.IsNullOrEmpty(categoryName))
            {
                isValid = false;
                message = "Dish's category's name cannot be empty";
            }
            else if (price < 0)
            {
                isValid = false;
                message = "Dish's price cannot be less then 0";
            }
            else if (unitOfWork.Dishes.GetAll().Any(dish => dish.Name == name))
            {
                isValid = false;
                message = "Dish with such name already exists";
            }

            return isValid;
        }

        private bool Validate(DishEditDTO dishEditDTO, ref string message)
        {
            bool isValid = true;
            string oldName = dishEditDTO.OldName;
            string newName = dishEditDTO.NewName;
            string categoryName = dishEditDTO.CategoryName;
            decimal price = dishEditDTO.Price;

            if (String.IsNullOrEmpty(newName))
            {
                isValid = false;
                message = "Dish's name cannot be empty";
            }
            if (newName.Length > 40)
            {
                isValid = false;
                message = "Dish's name cannot be more then 40 symbols";
            }
            else if (String.IsNullOrEmpty(categoryName))
            {
                isValid = false;
                message = "Dish's category's name cannot be empty";
            }
            else if (price < 0)
            {
                isValid = false;
                message = "Dish's price cannot be less then 0";
            }
            else if (oldName != newName && unitOfWork.Dishes.GetAll().Any(dish => dish.Name == newName))
            {
                isValid = false;
                message = "Dish with such name already exists";
            }

            return isValid;
        }
    }
}
