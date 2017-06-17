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
    public class IngredientEditViewModel : ObservableObject
    {
        public event GenericEventHandler<IngredientEditDTO> IngredientSaveRequest;
        public event GenericEventHandler<IngredientEditDTO> IngredientDeleteRequest;

        private readonly IControllerFactory factory;
        private IngredientEditDTO ingredient;

        public IngredientEditViewModel(IControllerFactory factory, IngredientListViewModel viewModel)
        {
            this.factory = factory;

            this.SaveCommand = new DelegateCommand(
                () => RaiseIngredientSaveRequestEvent(ingredient),
                obj => CanSave());
            this.DeleteCommand = new DelegateCommand(
                () => RaiseIngredientDeleteRequestEvent(ingredient),
                obj => ingredient != null);

            this.Stocks = new ObservableCollection<int>();

            viewModel.IngredientSelected += (s, e) => ChangeIngredient(e.Data.Name);
        }

        public void ChangeIngredient(string ingredientName)
        {
            using (IIngredientController controller = factory.CreateIngredientController())
            {
                DataControllerMessage<IngredientEditDTO> controllerMessage = controller.Get(ingredientName);
                if (controllerMessage.IsSuccess)
                {
                    this.ingredient = controllerMessage.Data;

                    this.Stocks.Clear();
                    foreach (int stockNo in ingredient.StocksAvailable)
                    {
                        this.Stocks.Add(stockNo);
                    }

                    RaisePropertyChangedEvent("Name");
                }
            }
        }

        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public string Name
        {
            get { return ingredient.NewName; }
            set
            {
                ingredient.NewName = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        public ObservableCollection<int> Stocks { get; set; }

        private bool CanSave()
        {
            return
                ingredient != null &&
                ingredient.OldName != ingredient.NewName &&
                !String.IsNullOrEmpty(Name);
        }

        private void RaiseIngredientSaveRequestEvent(IngredientEditDTO ingredient)
        {
            var handler = IngredientSaveRequest;
            if (handler != null)
            {
                GenericEventArgs<IngredientEditDTO> e = new GenericEventArgs<IngredientEditDTO>(ingredient);
                handler(this, e);
            }
        }

        private void RaiseIngredientDeleteRequestEvent(IngredientEditDTO ingredient)
        {
            var handler = IngredientDeleteRequest;
            if (handler != null)
            {
                GenericEventArgs<IngredientEditDTO> e = new GenericEventArgs<IngredientEditDTO>(ingredient);
                handler(this, e);
            }
        }
    }
}
