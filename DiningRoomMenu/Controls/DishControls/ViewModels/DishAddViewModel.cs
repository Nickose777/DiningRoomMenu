using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.DTO.Category;
using DiningRoomMenu.Logic.DTO.Dish;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.DishControls.ViewModels
{
    public class DishAddViewModel : ObservableObject
    {
        public event GenericEventHandler<DishAddDTO> DishAdded;

        private DishAddDTO dish;

        private CategoryDisplayDTO category;

        public DishAddViewModel(IEnumerable<CategoryDisplayDTO> categories)
        {
            this.dish = new DishAddDTO();

            this.SaveCommand = new DelegateCommand(Save, CanSave);

            this.Categories = new ObservableCollection<CategoryDisplayDTO>(categories);
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
