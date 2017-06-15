using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.Contracts.ViewControllers;
using DiningRoomMenu.Controls.IngredientControls.ViewModels;
using DiningRoomMenu.Controls.RecipeControls.ViewModels;
using DiningRoomMenu.Controls.RecipeControls.Views;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Recipe;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiningRoomMenu.ViewControllers
{
    class RecipeViewController : ViewControllerBase, IRecipeViewController
    {
        public RecipeViewController(IControllerFactory factory)
            : base(factory) { }

        public UIElement GetAddView(IDishSubject dishSubject, IIngredientSubject ingredientSubject)
        {
            IngredientListViewModel ingredientsViewModel = new IngredientListViewModel(factory, ingredientSubject);
            RecipeAddViewModel viewModel = new RecipeAddViewModel(factory, dishSubject, ingredientsViewModel);
            RecipeAddView view = new RecipeAddView(viewModel);

            viewModel.RecipeAdded += (s, ea) => OnAdd(ea.Data, viewModel);

            return view;
        }

        public UIElement GetListView()
        {
            RecipeListViewModel viewModel = new RecipeListViewModel(factory, this);
            RecipeListView view = new RecipeListView(viewModel);

            return view;
        }

        private void OnAdd(RecipeAddDTO recipe, RecipeAddViewModel viewModel)
        {
            using (IRecipeController controller = factory.CreateRecipeController())
            {
                ControllerMessage controllerMessage = controller.Add(recipe);
                if (controllerMessage.IsSuccess)
                {
                    viewModel.Name = String.Empty;
                    viewModel.Description = String.Empty;
                    viewModel.Ingredients.Clear();

                    Notify();
                }
                else
                {
                    MessageBox.Show(controllerMessage.Message);
                }
            }
        }
    }
}
