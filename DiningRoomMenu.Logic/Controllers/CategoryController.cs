using System;
using System.Linq;
using DiningRoomMenu.Core.Entities;
using DiningRoomMenu.Data.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.Infrastructure;

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

        private bool Validate(string categoryName, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(categoryName))
            {
                isValid = false;
                message = "Category's name cannot be empty";
            }
            else if (categoryName.Length > 20)
            {
                isValid = false;
                message = "Category's name cannot be longer then 20 symbols";
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
