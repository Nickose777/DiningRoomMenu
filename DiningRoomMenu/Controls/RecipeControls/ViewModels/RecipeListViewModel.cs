using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Recipe;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Controls.RecipeControls.ViewModels
{
    public class RecipeListViewModel : ObservableObject, IObserver
    {
        private readonly IControllerFactory factory;

        public RecipeListViewModel(IControllerFactory factory, IRecipeSubject subject)
        {
            this.factory = factory;

            this.Recipes = new ObservableCollection<RecipeDisplayDTO>();

            subject.Subscribe(this);
            Update();
        }

        public void Update()
        {
            Recipes.Clear();

            using (IRecipeController controller = factory.CreateRecipeController())
            {
                DataControllerMessage<IEnumerable<RecipeDisplayDTO>> controllerMessage = controller.GetAll();
                if (controllerMessage.IsSuccess)
                {
                    foreach (RecipeDisplayDTO recipe in controllerMessage.Data)
                    {
                        Recipes.Add(recipe);
                    }
                }
            }
        }

        public ObservableCollection<RecipeDisplayDTO> Recipes { get; set; }
    }
}
