using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Dish;
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

            if (String.IsNullOrEmpty(dishAddDTO.Name))
            {
                isValid = false;
                message = "Dish's name cannot be empty";
            }
            if (dishAddDTO.Name.Length > 40)
            {
                isValid = false;
                message = "Dish's name cannot be more then 40 symbols";
            }
            else if (String.IsNullOrEmpty(dishAddDTO.CategoryName))
            {
                isValid = false;
                message = "Dish's category's name cannot be empty";
            }
            else if (dishAddDTO.Price < 0)
            {
                isValid = false;
                message = "Dish's price cannot be less then 0";
            }
            else if (unitOfWork.Dishes.GetAll().Any(dish => dish.Name == dishAddDTO.Name))
            {
                isValid = false;
                message = "Dish with such name already exists";
            }

            return isValid;
        }
    }
}
