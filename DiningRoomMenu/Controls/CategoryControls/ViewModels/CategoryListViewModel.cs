using DiningRoomMenu.EventHandlers;
using DiningRoomMenu.Logic.DTO.Category;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiningRoomMenu.Controls.CategoryControls.ViewModels
{
    public class CategoryListViewModel : ObservableObject
    {
        public event GenericEventHandler<CategoryDisplayDTO> CategorySelected;

        private CategoryDisplayDTO category;

        public CategoryListViewModel(IEnumerable<CategoryDisplayDTO> categories)
        {
            this.SelectCommand = new DelegateCommand(
                () => RaiseCategorySelectedEvent(Category),
                obj => Category != null);

            this.Categories = new ObservableCollection<CategoryDisplayDTO>(categories);
        }

        public ICommand SelectCommand { get; private set; }

        public CategoryDisplayDTO Category
        {
            get { return category; }
            set
            {
                category = value;
                RaisePropertyChangedEvent("Category");
            }
        }

        public ObservableCollection<CategoryDisplayDTO> Categories { get; set; }

        private void RaiseCategorySelectedEvent(CategoryDisplayDTO category)
        {
            var handler = CategorySelected;
            if (handler != null)
            {
                GenericEventArgs<CategoryDisplayDTO> e = new GenericEventArgs<CategoryDisplayDTO>(category);
                handler(this, e);
            }
        }
    }
}
