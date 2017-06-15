using DiningRoomMenu.Contracts;
using DiningRoomMenu.Contracts.Subjects;
using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.Contracts;
using DiningRoomMenu.Logic.Contracts.Controllers;
using DiningRoomMenu.Logic.DTO.Category;
using DiningRoomMenu.Logic.DTO.Dish;
using DiningRoomMenu.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.DishControls.ViewModels
{
    public class DishAddViewModel : ObservableObject, IObserver
    {
        public event GenericEventHandler<DishAddDTO> DishAdded;

        private readonly IControllerFactory factory;
        private DishAddDTO dish;
        private CategoryDisplayDTO category;

        public DishAddViewModel(IControllerFactory factory, ICategorySubject subject)
        {
            this.factory = factory;
            this.dish = new DishAddDTO();

            this.SaveCommand = new DelegateCommand(Save, CanSave);

            this.Categories = new ObservableCollection<CategoryDisplayDTO>();

            subject.Subscribe(this);
            Update();
        }

        public void Update()
        {
            Categories.Clear();

            using (ICategoryController controller = factory.CreateCategoryController())
            {
                DataControllerMessage<IEnumerable<CategoryDisplayDTO>> controllerMessage = controller.GetAll();
                if (controllerMessage.IsSuccess)
                {
                    foreach (CategoryDisplayDTO category in controllerMessage.Data)
                    {
                        Categories.Add(category);
                    }
                }
            }
        }

        public ICommand SaveCommand { get; private set; }

        public string Name
        {
            get { return dish.Name; }
            set 
            {
                dish.Name = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        public decimal Price
        {
            get { return dish.Price; }
            set
            {
                dish.Price = value;
                RaisePropertyChangedEvent("Price");
            }
        }

        public CategoryDisplayDTO Category
        {
            get { return category; }
            set
            {
                category = value;
                RaisePropertyChangedEvent("Category");
            }
        }

        public ObservableCollection<CategoryDisplayDTO> Categories { get; private set; }

        private void Save()
        {
            dish.CategoryName = Category.Name;
            RaiseDishAddedEvent(dish);
        }

        private bool CanSave(object obj)
        {
            return
                Category != null &&
                !String.IsNullOrEmpty(Name);
        }

        private void RaiseDishAddedEvent(DishAddDTO dish)
        {
            var handler = DishAdded;
            if (handler != null)
            {
                GenericEventArgs<DishAddDTO> e = new GenericEventArgs<DishAddDTO>(dish);
                handler(this, e);
            }
        }
    }
}
