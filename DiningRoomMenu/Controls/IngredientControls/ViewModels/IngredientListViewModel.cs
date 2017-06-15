using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Ingredient;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.IngredientControls.ViewModels
{
    public class IngredientListViewModel : ObservableObject, IObserver
    {
        public event GenericEventHandler<IngredientDisplayDTO> IngredientSelected;

        private readonly IControllerFactory factory;
        private IngredientDisplayDTO ingredient;

        public IngredientListViewModel(IControllerFactory factory, IIngredientSubject subject)
        {
            this.factory = factory;

            this.SelectCommand = new DelegateCommand(
                () => RaiseIngredientSelectedEvent(ingredient),
                obj => Ingredient != null);

            this.Ingredients = new ObservableCollection<IngredientDisplayDTO>();

            subject.Subscribe(this);
            Update();
        }

        public void Update()
        {
            Ingredients.Clear();

            using (IIngredientController controller = factory.CreateIngredientController())
            {
                DataControllerMessage<IEnumerable<IngredientDisplayDTO>> controllerMessage = controller.GetAll();

                if (controllerMessage.IsSuccess)
                {
                    foreach (IngredientDisplayDTO ingredient in controllerMessage.Data)
                    {
                        Ingredients.Add(ingredient);
                    }
                }
            }
        }

        public ICommand SelectCommand { get; private set; }

        public IngredientDisplayDTO Ingredient
        {
            get { return ingredient; }
            set
            {
                ingredient = value;
                RaisePropertyChangedEvent("Ingredient");
            }
        }

        public ObservableCollection<IngredientDisplayDTO> Ingredients { get; set; }

        private void RaiseIngredientSelectedEvent(IngredientDisplayDTO ingredient)
        {
            var handler = IngredientSelected;
            if (handler != null)
            {
                GenericEventArgs<IngredientDisplayDTO> e = new GenericEventArgs<IngredientDisplayDTO>(ingredient);
                handler(this, e);
            }
        }
    }
}
